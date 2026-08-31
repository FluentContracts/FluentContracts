using System;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Serilog;
using static Nuke.Common.Tools.Git.GitTasks;

// ReSharper disable AllUnderscoreLocalParameterName
partial class Build
{
    AbsolutePath ChangelogFile => RootDirectory / "CHANGELOG.md";

    const string UnreleasedHeading = "## [Unreleased]";

    /// <summary>
    /// Put this in a pull request title to merge without publishing a package.
    /// </summary>
    /// <remarks>
    /// GitHub understands <c>[skip ci]</c> natively, but that skips every workflow. This one is ours,
    /// so the merge is still built and tested on <c>master</c> — only publishing is held back.
    /// </remarks>
    const string SkipReleaseToken = "[skip release]";

    /// <summary>
    /// Whether the commit being built asked not to be released. Pull requests are squash-merged, so
    /// the pull request title is what ends up in this message.
    /// </summary>
    bool SkipRelease =>
        HeadCommitMessage.IndexOf(SkipReleaseToken, StringComparison.OrdinalIgnoreCase) >= 0;

    string HeadCommitMessage =>
        Git("log -1 --pretty=%B", logOutput: false, logInvocation: false)
            .Select(x => x.Text)
            .JoinNewLine();

    /// <summary>
    /// Renames the <c>[Unreleased]</c> section to the version that was just published and leaves a
    /// fresh empty one above it, so the next release's entries do not merge into this one's.
    /// </summary>
    [UsedImplicitly]
    Target FinalizeChangelog => _ => _
        .TriggeredBy(Publish)
        .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
        .OnlyWhenDynamic(() => !SkipRelease)
        // Never fail a release that already published because the bookkeeping afterwards did not land.
        .ProceedAfterFailure()
        .Executes(() =>
        {
            if (!TryFinalizeChangelog(MajorMinorPatchVersion, out var reason))
            {
                Log.Information("Leaving {File} alone: {Reason}", ChangelogFile.Name, reason);
                return;
            }

            Git("config user.name \"github-actions[bot]\"");
            Git("config user.email \"41898282+github-actions[bot]@users.noreply.github.com\"");
            Git($"add {ChangelogFile}");
            Git($"commit -m \"Finalise the changelog for {MajorMinorPatchVersion} {SkipReleaseToken}\"");

            // The checkout is detached at the commit that triggered this run, so push explicitly.
            // A push made with GITHUB_TOKEN does not start another workflow run, and the marker in
            // the message keeps it from releasing even if that ever changes.
            Git($"push origin HEAD:{MainBranch}");

            Log.Information("Changelog finalized for {Version}", MajorMinorPatchVersion);
        });

    bool TryFinalizeChangelog(string version, out string reason)
    {
        var content = ChangelogFile.ReadAllText();

        if (!content.Contains(UnreleasedHeading))
        {
            reason = $"there is no {UnreleasedHeading} section";
            return false;
        }

        if (Regex.IsMatch(content, $@"^## \[{Regex.Escape(version)}\]", RegexOptions.Multiline))
        {
            reason = $"{version} already has a section";
            return false;
        }

        var unreleasedBody = Regex.Match(
            content,
            $@"{Regex.Escape(UnreleasedHeading)}(?<body>.*?)(?=^## \[)",
            RegexOptions.Multiline | RegexOptions.Singleline);

        if (!unreleasedBody.Success || unreleasedBody.Groups["body"].Value.Trim().Length == 0)
        {
            reason = "nothing was written under it";
            return false;
        }

        var previousVersion = Regex
            .Match(content, @"^## \[(?<version>\d+\.\d+\.\d+)\]", RegexOptions.Multiline)
            .Groups["version"].Value;

        var released = $"## [{version}] / {DateTime.UtcNow:yyyy-MM-dd}";
        content = content.Replace(UnreleasedHeading, $"{UnreleasedHeading}{Environment.NewLine}{Environment.NewLine}{released}");

        // Keep the comparison links at the bottom in step with the new section.
        var unreleasedLink = Regex.Match(
            content,
            @"^\[Unreleased\]:\s*(?<url>\S+)/compare/(?<previous>\S+)\.\.\.HEAD\s*$",
            RegexOptions.Multiline);

        if (unreleasedLink.Success)
        {
            var url = unreleasedLink.Groups["url"].Value;

            content = content.Replace(
                unreleasedLink.Value,
                $"[Unreleased]: {url}/compare/{version}...HEAD{Environment.NewLine}" +
                $"[{version}]: {url}/compare/{previousVersion}...{version}");
        }

        ChangelogFile.WriteAllText(content);

        reason = string.Empty;
        return true;
    }
}
