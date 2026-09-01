using System;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.GitHub;
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
    /// GitHub's own marker, which skips every workflow. Only the changelog commit uses it: it is
    /// generated, touches nothing but Markdown, and has already been through CI as part of the
    /// merge it documents.
    /// </summary>
    const string SkipCiToken = "[skip ci]";

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
    /// An installation token for the GitHub App that is allowed to push to <see cref="MainBranch"/>.
    /// Minted by the release workflow — see <see cref="ReleaseWorkflowAttribute"/>.
    /// </summary>
    [Parameter] [Secret] string ChangelogPushToken;

    /// <summary>The app's slug, used to name the author of the changelog commit.</summary>
    [Parameter] string ChangelogPushAppSlug;

    /// <summary>
    /// Renames the <c>[Unreleased]</c> section to the version that was just published and leaves a
    /// fresh empty one above it, so the next release's entries do not merge into this one's.
    /// </summary>
    [UsedImplicitly]
    Target FinalizeChangelog => _ => _
        .TriggeredBy(Publish)
        .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
        .OnlyWhenDynamic(() => !SkipRelease)
        .Executes(() =>
        {
            // The package is already on nuget.org by the time this runs, so nothing here may fail the
            // release. ProceedAfterFailure would still leave the workflow red, which reads as a failed
            // release; report the problem and let the run stay green instead.
            try
            {
                PushFinalizedChangelog();
            }
            catch (Exception exception)
            {
                Log.Warning(
                    exception,
                    "{Version} published, but {File} was not finalised. Until someone does it by hand, "
                    + "the next release will fold this version's entries into its own section.",
                    MajorMinorPatchVersion,
                    ChangelogFile.Name);
            }
        });

    void PushFinalizedChangelog()
    {
        if (!TryFinalizeChangelog(MajorMinorPatchVersion, out var reason))
        {
            Log.Information("Leaving {File} alone: {Reason}", ChangelogFile.Name, reason);
            return;
        }

        if (ChangelogPushToken.IsNullOrWhiteSpace())
        {
            Log.Warning(
                "{File} was updated but not pushed: no {Parameter}. The next release will fold "
                + "these entries into its own section unless someone finalises them by hand.",
                ChangelogFile.Name,
                nameof(ChangelogPushToken));
            return;
        }

        var committer = ChangelogPushAppSlug.IsNullOrWhiteSpace()
            ? "github-actions"
            : $"{ChangelogPushAppSlug}[bot]";

        // Every value below goes in as a single interpolation hole, never inside quotes of our own.
        // Nuke quotes an interpolated value that contains a space; adding quotes around it as well
        // produces a nested pair, and git then reads the tail of the message as pathspecs.
        var email = $"{committer}@users.noreply.github.com";
        var message = $"Finalise the changelog for {MajorMinorPatchVersion} {SkipCiToken} {SkipReleaseToken}";

        Git($"config user.name {committer}");
        Git($"config user.email {email}");
        Git($"add {ChangelogFile}");
        Git($"commit -m {message}");

        // The checkout is detached at the commit that triggered this run, and its credentials are the
        // workflow's GITHUB_TOKEN, which the branch protection on MainBranch rejects. Point the remote
        // at a URL carrying the app token instead. Unlike GITHUB_TOKEN, an app's push does start
        // another workflow run, which the markers in the message are there to stop.
        var remote =
            $"https://x-access-token:{ChangelogPushToken}@github.com/"
            + $"{GitRepository.GetGitHubOwner()}/{GitRepository.GetGitHubName()}.git";

        // Only the command carrying the token is hidden. The push itself is logged, so a rejected push
        // says why; Actions masks the token in whatever git echoes back.
        Git($"remote set-url origin {remote}", logOutput: false, logInvocation: false);
        Git($"push origin HEAD:{MainBranch}");

        Log.Information("Changelog finalized for {Version}", MajorMinorPatchVersion);
    }

    bool TryFinalizeChangelog(string version, out string reason)
    {
        var content = ChangelogFile.ReadAllText();

        // Anchored to the start of a line, because entries quote the heading in their prose. Matching
        // it anywhere would rewrite those mentions too and corrupt the very entry describing this step.
        var heading = Regex.Match(
            content,
            $@"^{Regex.Escape(UnreleasedHeading)}[ \t]*$",
            RegexOptions.Multiline);

        if (!heading.Success)
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
            $@"^{Regex.Escape(UnreleasedHeading)}[ \t]*$(?<body>.*?)(?=^## \[)",
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

        // Splice at the heading that was matched, rather than replacing every occurrence of its text.
        content = content[..heading.Index]
            + $"{UnreleasedHeading}{Environment.NewLine}{Environment.NewLine}{released}"
            + content[(heading.Index + heading.Length)..];

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
