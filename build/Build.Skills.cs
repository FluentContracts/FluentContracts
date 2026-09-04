using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Utilities.Collections;
using Octokit;
using Serilog;
using Utils;

// ReSharper disable AllUnderscoreLocalParameterName
// ReSharper disable InconsistentNaming

/// <summary>
/// The agent skill and the plugin that ships it.
/// </summary>
/// <remarks>
/// <para>
/// <c>skills/</c> is the source of truth, and <c>plugins/fluentcontracts/skills/</c> is a committed
/// copy of it. A copy rather than a symlink, because a Windows clone made without
/// <c>core.symlinks</c> materialises a link as a small text file holding the link target — and the
/// plugin then ships no skills at all.
/// </para>
/// <para>
/// All of it is a build target because none of it fails on its own. The folders are loaded directly
/// by three harnesses, so a frontmatter name that no longer matches its directory silently never
/// loads; the copy can drift from its source with a green build; and the version the manifests carry
/// is bumped by hand, so skills shipped without a bump never reach an agent already holding the old
/// copy. Each one is a change that looks correct in the repository and reaches nobody.
/// </para>
/// </remarks>
partial class Build
{
    /// <summary>The skills tree that every distribution surface serves.</summary>
    AbsolutePath SkillsDirectory => RootDirectory / "skills";

    /// <summary>The plugin directory the marketplaces install from.</summary>
    AbsolutePath PluginDirectory => RootDirectory / "plugins" / PluginName;

    /// <summary>The committed copy of <see cref="SkillsDirectory"/> that the plugin ships.</summary>
    AbsolutePath PluginSkillsDirectory => PluginDirectory / "skills";

    /// <summary>The plugin's own manifest, whose version the marketplace reads.</summary>
    AbsolutePath PluginManifest => PluginDirectory / ".claude-plugin" / "plugin.json";

    /// <summary>The Codex-native copy of the plugin manifest.</summary>
    AbsolutePath CodexPluginManifest => PluginDirectory / ".codex-plugin" / "plugin.json";

    /// <summary>The Claude Code marketplace manifest listing the plugin.</summary>
    AbsolutePath MarketplaceManifest => RootDirectory / ".claude-plugin" / "marketplace.json";

    /// <summary>The Codex-native marketplace catalog listing the plugin.</summary>
    AbsolutePath AgentsMarketplaceManifest => RootDirectory / ".agents" / "plugins" / "marketplace.json";

    /// <summary>The Gemini CLI extension manifest at the repository root.</summary>
    AbsolutePath GeminiExtensionManifest => RootDirectory / "gemini-extension.json";

    /// <summary>Where <see cref="PackPlugin"/> writes the archive attached to the GitHub release.</summary>
    AbsolutePath PluginPackagesDirectory => OutputDirectory / "plugins";

    /// <summary>The archive <see cref="PackPlugin"/> produced, if it ran.</summary>
    IEnumerable<AbsolutePath> PluginPackageFiles => PluginPackagesDirectory.GlobFiles("*.zip");

    /// <summary>The name every manifest must agree on; also the plugin directory's own name.</summary>
    const string PluginName = "fluentcontracts";

    /// <summary>The plugin's location, as the two marketplace manifests spell it.</summary>
    const string PluginSourcePath = "./plugins/fluentcontracts";

    /// <summary>Where the Codex manifest points for the skills it serves.</summary>
    const string CodexSkillsPath = "./skills/";

    /// <summary>
    /// The environment variable naming the ref <see cref="CheckPluginVersion"/> compares against, for
    /// a clone whose base branch is not fetched under the name the check would pick on its own.
    /// </summary>
    const string PluginBaseRefVariable = "FLUENTCONTRACTS_PLUGIN_BASE_REF";

    /// <summary>Plugin versions are plain <c>major.minor.patch</c>; nothing consumes a prerelease.</summary>
    static readonly Regex PluginVersionFormat = new(@"^\d+\.\d+\.\d+$");

    /// <summary>
    /// Every manifest carrying the plugin version. The bump is manual and has to land in all of them,
    /// which is what <see cref="CheckPluginManifests"/> is for.
    /// </summary>
    IReadOnlyList<AbsolutePath> VersionedManifests =>
    [
        PluginManifest,
        CodexPluginManifest,
        MarketplaceManifest,
        GeminiExtensionManifest
    ];

    /// <summary>The plugin version the working tree declares.</summary>
    string PluginVersion => ReadJsonString(PluginManifest, "version").NotNull();

    /// <summary>
    /// Everything about the skills and the plugin that has to hold before a merge. <c>Test</c>
    /// depends on it, so the <c>pr</c> and <c>release</c> workflows already run it and neither the
    /// workflows nor their generating attributes need to change.
    /// </summary>
    [UsedImplicitly]
    Target VerifySkills => _ => _
        .DependsOn(CheckSkillDocuments, CheckPluginSkillsSync, CheckPluginManifests, CheckPluginVersion)
        .Unlisted();

    /// <summary>
    /// Regenerates <see cref="PluginSkillsDirectory"/> from <see cref="SkillsDirectory"/>. Run it
    /// after editing a skill; <see cref="CheckPluginSkillsSync"/> fails the build until you have.
    /// </summary>
    [UsedImplicitly]
    Target SyncPluginSkills => _ => _
        .Executes(() =>
        {
            CopyDirectory(SkillsDirectory, PluginSkillsDirectory);

            var files = RelativeFiles(PluginSkillsDirectory);
            foreach (var file in files)
                Log.Information("Synced {File}", $"{Relative(PluginSkillsDirectory)}/{file}");

            ReportSummary(_ => _.AddPair("Synced", files.Count.ToString()));
        });

    /// <summary>
    /// Validates every skill document against the Agent Skills specification.
    /// </summary>
    [UsedImplicitly]
    Target CheckSkillDocuments => _ => _
        .Executes(() =>
        {
            // Reported relative to the repository, so a message can be pasted straight into an editor.
            var problems = AgentSkills.Check(SkillsDirectory)
                .Select(x => x.Replace($"{RootDirectory}{Path.DirectorySeparatorChar}", string.Empty))
                .ToList();

            Assert.True(
                problems.Count == 0,
                "The skills tree does not conform to the Agent Skills specification:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, problems.Select(x => $"  {x}")));

            Log.Information(
                "{Count} skill(s) conform to the Agent Skills specification.",
                Directory.GetDirectories(SkillsDirectory).Length);
        });

    /// <summary>
    /// Fails when the plugin's committed copy of the skills has drifted from its source.
    /// </summary>
    [UsedImplicitly]
    Target CheckPluginSkillsSync => _ => _
        .Executes(() =>
        {
            var drift = FindSkillsDrift();

            Assert.True(
                drift.Count == 0,
                $"{Relative(PluginSkillsDirectory)} has drifted from {Relative(SkillsDirectory)}:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, drift.Select(x => $"  {x}"))
                + Environment.NewLine + Environment.NewLine
                + $"Run ./build.sh {nameof(SyncPluginSkills)} and commit the result. "
                + "Never hand-edit the copy.");

            Log.Information("The plugin's skills are an exact copy of {Directory}.", Relative(SkillsDirectory));
        });

    /// <summary>
    /// Fails when the manifests disagree — about the version, the plugin's name, or where it lives.
    /// </summary>
    /// <remarks>
    /// They are five separate files because three harnesses read them, and each one is a separate
    /// opportunity to update four of the five.
    /// </remarks>
    [UsedImplicitly]
    Target CheckPluginManifests => _ => _
        .Executes(() =>
        {
            var problems = FindManifestProblems();

            Assert.True(
                problems.Count == 0,
                "The plugin manifests disagree:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, problems.Select(x => $"  {x}")));

            Log.Information(
                "All {Count} manifests agree on {Name} {Version}.",
                VersionedManifests.Count,
                PluginName,
                PluginVersion);
        });

    /// <summary>
    /// Fails when what the plugin publishes changed against the base ref without the plugin version
    /// moving up.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Clients read the declared version to decide whether an installed plugin is stale, so a skill
    /// edited without a bump simply never reaches an agent already holding the old copy. Nothing else
    /// in the build can catch it: the source and the committed copy move together, and the version
    /// sits outside both.
    /// </para>
    /// <para>
    /// Up, not merely different. Resolving a version conflict by keeping the lower number would
    /// otherwise read as a bump, and ship the skills under a version clients already have.
    /// </para>
    /// <para>
    /// This is the one check that needs history, because "the content changed and the version did
    /// not" is not a property of a single snapshot. Both workflows check out with
    /// <c>fetch-depth: 0</c>, so on CI the base ref is always present and a skip is a failure — a
    /// check that cannot run is not a check. Locally a skip is ordinary: a shallow clone, a fresh
    /// worktree, a base that was never fetched.
    /// </para>
    /// </remarks>
    [UsedImplicitly]
    Target CheckPluginVersion => _ => _
        .Executes(() =>
        {
            var baseRef = ResolvePluginBaseRef();

            if (!GitRefExists(baseRef))
            {
                var skipped =
                    $"Plugin version check skipped: the base ref \"{baseRef}\" is not in this clone. "
                    + $"Set {PluginBaseRefVariable} to a ref you do have.";

                Assert.True(
                    IsLocalBuild,
                    $"{skipped} On CI the base ref has to be fetchable: check out with fetch-depth 0.");

                Log.Information("{Message}", skipped);
                return;
            }

            var changed = PublishedSkillChanges(baseRef);
            if (changed.Count == 0)
            {
                Log.Information("No published skill changed against {BaseRef}; nothing to bump.", baseRef);
                return;
            }

            var head = PluginVersion;
            var previous = ReadJsonStringAt(baseRef, PluginManifest, "version");

            // No manifest at the base means the plugin is new on this branch: there is no previous
            // version for it to differ from, so the requirement cannot apply.
            if (previous == null)
            {
                Log.Information("The plugin is new against {BaseRef}; shipping {Version}.", baseRef, head);
                return;
            }

            Assert.True(IsNewerVersion(head, previous), PluginBumpFailure(changed, previous, head));

            Log.Information(
                "The skills changed against {BaseRef} and the plugin version moved {Previous} to {Head}.",
                baseRef,
                previous,
                head);
        });

    /// <summary>
    /// Archives the plugin, so a release carries a copy of exactly what it published. Attached to the
    /// GitHub release next to the packages.
    /// </summary>
    [UsedImplicitly]
    Target PackPlugin => _ => _
        .TriggeredBy(Pack)
        .After(Pack)
        .DependsOn(VerifySkills)
        .Executes(() =>
        {
            PluginPackagesDirectory.CreateOrCleanDirectory();

            var archive = PluginPackagesDirectory / $"{PluginName}-plugin-{PluginVersion}.zip";

            ZipFile.CreateFromDirectory(
                PluginDirectory,
                archive,
                CompressionLevel.Optimal,
                includeBaseDirectory: false);

            ReportSummary(_ => _.AddPair("Plugin", $"{PluginName} {PluginVersion}"));
        });

    /// <summary>
    /// Tags the commit that published a plugin version, so an installation can be pinned to it:
    /// <c>/plugin marketplace add FluentContracts/FluentContracts@plugin-v1.0.0</c>.
    /// </summary>
    /// <remarks>
    /// Idempotent, because every merge into the main branch runs this while the plugin version only
    /// moves when a skill changes: a version that already has its tag is left alone. The package is
    /// on nuget.org by the time this runs, so nothing here may fail the release.
    /// </remarks>
    [UsedImplicitly]
    Target TagPluginRelease => _ => _
        .TriggeredBy(Publish)
        .After(CreateGitHubRelease)
        .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
        .OnlyWhenDynamic(() => !SkipRelease)
        .Executes(async () =>
        {
            var tag = $"plugin-v{PluginVersion}";
            var token = GitHubActions?.Token;

            if (token == null)
            {
                Log.Information("No GitHub token, so {Tag} was not created.", tag);
                return;
            }

            try
            {
                GitHubTasks.GitHubClient.Credentials = new Credentials(token);

                var owner = GitRepository.GetGitHubOwner();
                var name = GitRepository.GetGitHubName();

                if (await TagExistsAsync(owner, name, tag))
                {
                    Log.Information("{Tag} already exists; the plugin version has not moved.", tag);
                    return;
                }

                await GitHubTasks.GitHubClient.Git.Reference.Create(
                    owner,
                    name,
                    new NewReference($"refs/tags/{tag}", GitVersion.Sha));

                Log.Information("Tagged {Sha} as {Tag}.", GitVersion.Sha, tag);
            }
            catch (Exception exception)
            {
                Log.Warning(
                    exception,
                    "{Version} published, but the plugin tag {Tag} was not created.",
                    MajorMinorPatchVersion,
                    tag);
            }
        });

    static async Task<bool> TagExistsAsync(string owner, string name, string tag)
    {
        try
        {
            await GitHubTasks.GitHubClient.Git.Reference.Get(owner, name, $"tags/{tag}");
            return true;
        }
        catch (NotFoundException)
        {
            return false;
        }
    }

    List<string> FindManifestProblems()
    {
        var problems = new List<string>();

        var missing = VersionedManifests
            .Concat([AgentsMarketplaceManifest])
            .Where(x => !File.Exists(x))
            .ToList();

        if (missing.Count > 0)
        {
            problems.AddRange(missing.Select(x => $"{Relative(x)}: missing."));
            return problems;
        }

        var versions = new Dictionary<string, string>();
        foreach (var manifest in VersionedManifests)
        {
            versions[Relative(manifest)] = manifest == MarketplaceManifest
                ? ReadMarketplaceEntryString(manifest, "version")
                : ReadJsonString(manifest, "version");
        }

        foreach (var declared in versions.Where(x => x.Value == null))
            problems.Add($"{declared.Key}: declares no string \"version\".");

        foreach (var declared in versions.Where(x => x.Value != null && !PluginVersionFormat.IsMatch(x.Value)))
            problems.Add($"{declared.Key}: version \"{declared.Value}\" is not major.minor.patch.");

        var distinct = versions.Values.Where(x => x != null).Distinct().ToList();
        if (distinct.Count > 1)
        {
            problems.Add(
                "the manifests declare different versions ("
                + string.Join(", ", versions.Where(x => x.Value != null).Select(x => $"{x.Key} = {x.Value}"))
                + "). They are bumped by hand and have to agree.");
        }

        var names = new Dictionary<string, string>
        {
            [Relative(PluginManifest)] = ReadJsonString(PluginManifest, "name"),
            [Relative(CodexPluginManifest)] = ReadJsonString(CodexPluginManifest, "name"),
            [Relative(GeminiExtensionManifest)] = ReadJsonString(GeminiExtensionManifest, "name"),
            [$"{Relative(MarketplaceManifest)} (entry)"] = ReadMarketplaceEntryString(MarketplaceManifest, "name"),
            [$"{Relative(AgentsMarketplaceManifest)} (entry)"] =
                ReadMarketplaceEntryString(AgentsMarketplaceManifest, "name")
        };

        foreach (var declared in names.Where(x => x.Value != PluginName))
            problems.Add($"{declared.Key}: name is \"{declared.Value}\", expected \"{PluginName}\".");

        var codexSkills = ReadJsonString(CodexPluginManifest, "skills");
        if (codexSkills != CodexSkillsPath)
        {
            problems.Add(
                $"{Relative(CodexPluginManifest)}: \"skills\" is \"{codexSkills}\", "
                + $"expected \"{CodexSkillsPath}\".");
        }

        var source = ReadMarketplaceEntryString(MarketplaceManifest, "source");
        if (source != PluginSourcePath)
        {
            problems.Add(
                $"{Relative(MarketplaceManifest)}: the entry's \"source\" is \"{source}\", "
                + $"expected \"{PluginSourcePath}\".");
        }

        var agentsSource = ReadAgentsMarketplaceSourcePath();
        if (agentsSource != PluginSourcePath)
        {
            problems.Add(
                $"{Relative(AgentsMarketplaceManifest)}: the entry's source path is \"{agentsSource}\", "
                + $"expected \"{PluginSourcePath}\".");
        }

        return problems;
    }

    List<string> FindSkillsDrift()
    {
        var drift = new List<string>();
        var source = RelativeFiles(SkillsDirectory);
        var copy = RelativeFiles(PluginSkillsDirectory);
        var copied = new HashSet<string>(copy, StringComparer.Ordinal);

        foreach (var file in source)
        {
            if (!copied.Contains(file))
            {
                drift.Add($"{Relative(PluginSkillsDirectory)}/{file} is missing.");
                continue;
            }

            if (Normalize(File.ReadAllText(SkillsDirectory / file)) !=
                Normalize(File.ReadAllText(PluginSkillsDirectory / file)))
            {
                drift.Add(
                    $"{Relative(PluginSkillsDirectory)}/{file} differs from "
                    + $"{Relative(SkillsDirectory)}/{file}.");
            }
        }

        var kept = new HashSet<string>(source, StringComparer.Ordinal);
        drift.AddRange(copy
            .Where(x => !kept.Contains(x))
            .Select(x => $"{Relative(PluginSkillsDirectory)}/{x} is not in {Relative(SkillsDirectory)}/."));

        return drift;
    }

    string PluginBumpFailure(IReadOnlyList<string> changed, string previous, string head)
    {
        var lines = new List<string>
        {
            previous == head
                ? $"The published skills changed but the plugin version did not (still {head})."
                : "The published skills changed and the plugin version moved the wrong way: "
                  + $"{previous} to {head}, which is not an increase.",
            string.Empty,
            "Changed:"
        };

        lines.AddRange(changed.Take(5).Select(x => $"  {x}"));
        if (changed.Count > 5)
            lines.Add($"  ...and {changed.Count - 5} more");

        lines.Add(string.Empty);
        lines.Add("Clients read this version to decide whether an installed plugin is stale, so skills");
        lines.Add("shipped without a bump never reach an agent that already holds the old copy.");
        lines.Add(string.Empty);
        lines.Add($"Set a version above {previous} in ALL of these manifests, which have to agree:");
        lines.AddRange(VersionedManifests.Select(x => $"  {Relative(x)}"));
        lines.Add("Additive skill content is a minor bump; a correction is a patch.");
        lines.Add(string.Empty);
        lines.Add("If another branch already shipped the version you picked, take the next one above");
        lines.Add("what the base branch now holds rather than matching it: two branches landing the");
        lines.Add("same version leave the second change invisible to every client that fetched the first.");

        return string.Join(Environment.NewLine, lines);
    }

    /// <summary>
    /// The ref to compare the plugin version against: the environment's override, else the pull
    /// request's base branch, else the previous commit on a push to the main branch, else the main
    /// branch itself.
    /// </summary>
    /// <remarks>
    /// The previous commit is what makes a <em>collision</em> visible. Two branches that both bump
    /// 1.2.0 to 1.3.0 merge without a conflict — each side made the identical edit — and neither pull
    /// request's check ever saw the other, because GitHub does not re-run a pull request's checks
    /// when its base moves.
    /// </remarks>
    string ResolvePluginBaseRef()
    {
        var explicitRef = Environment.GetEnvironmentVariable(PluginBaseRefVariable);
        if (!string.IsNullOrWhiteSpace(explicitRef)) return explicitRef;

        // GITHUB_BASE_REF is a bare branch name; a CI checkout has it as a remote-tracking ref.
        var pullRequestBase = Environment.GetEnvironmentVariable("GITHUB_BASE_REF");
        if (!string.IsNullOrWhiteSpace(pullRequestBase)) return $"origin/{pullRequestBase}";

        if (IsServerBuild && GitRepository.IsOnMainOrMasterBranch()) return "HEAD^";

        return $"origin/{MainBranch}";
    }

    /// <summary>
    /// The published files that changed between the merge base with <paramref name="baseRef"/> and
    /// the working tree.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The merge base excludes commits that landed on the base branch after this one forked. The
    /// diff is then taken from it in the two-dot form on purpose: <c>diff a...b</c> compares two
    /// commits and ignores the working tree, so the check would pass for the person about to commit a
    /// skill edit with no bump and fail only once CI saw it committed.
    /// </para>
    /// <para>
    /// A diff lists no file git has never seen, so untracked ones are asked for separately. A brand
    /// new skill is the case where forgetting the bump costs the most, and it is exactly the case a
    /// diff alone cannot see.
    /// </para>
    /// </remarks>
    List<string> PublishedSkillChanges(string baseRef)
    {
        var mergeBase = RunGit("merge-base", baseRef, "HEAD");
        Assert.True(mergeBase.ExitCode == 0, $"git merge-base {baseRef} HEAD failed: {mergeBase.Error}");

        var diff = RunGit("diff", "--name-only", mergeBase.Output.Trim());
        Assert.True(diff.ExitCode == 0, $"git diff against {baseRef} failed: {diff.Error}");

        var untracked = RunGit("ls-files", "--others", "--exclude-standard");
        Assert.True(untracked.ExitCode == 0, $"git ls-files failed: {untracked.Error}");

        var published = new[] { $"{Relative(SkillsDirectory)}/", $"{Relative(PluginSkillsDirectory)}/" };

        return $"{diff.Output}\n{untracked.Output}"
            .Split('\n')
            .Select(x => x.Trim())
            .Where(x => x.Length > 0 && published.Any(prefix => x.StartsWith(prefix, StringComparison.Ordinal)))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(x => x, StringComparer.Ordinal)
            .ToList();
    }

    bool GitRefExists(string reference) =>
        RunGit("rev-parse", "--verify", $"{reference}^{{commit}}").ExitCode == 0;

    /// <summary>
    /// A JSON string field of <paramref name="path"/> as it stood at <paramref name="reference"/>, or
    /// <c>null</c> when the file did not exist there or carries no such field.
    /// </summary>
    string ReadJsonStringAt(string reference, AbsolutePath path, string field)
    {
        var show = RunGit("show", $"{reference}:{Relative(path)}");
        return show.ExitCode == 0 ? ReadJsonStringFrom(show.Output, field) : null;
    }

    static bool IsNewerVersion(string version, string previous) =>
        PluginVersionFormat.IsMatch(version)
        && PluginVersionFormat.IsMatch(previous)
        && Version.Parse(version) > Version.Parse(previous);

    static string ReadJsonString(AbsolutePath path, string field) =>
        File.Exists(path) ? ReadJsonStringFrom(File.ReadAllText(path), field) : null;

    static string ReadJsonStringFrom(string json, string field)
    {
        try
        {
            using var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty(field, out var value)) return null;

            return value.ValueKind == JsonValueKind.String ? value.GetString() : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    /// <summary>A string field of the plugin's entry in one of the two marketplace manifests.</summary>
    static string ReadMarketplaceEntryString(AbsolutePath path, string field)
    {
        var entry = MarketplaceEntry(path);
        if (entry == null) return null;

        if (!entry.Value.TryGetProperty(field, out var value)) return null;

        return value.ValueKind == JsonValueKind.String ? value.GetString() : null;
    }

    /// <summary>
    /// Where the Codex catalog says the plugin is. It nests the source as
    /// <c>{ "source": "local", "path": "…" }</c> rather than as the plain string the Claude Code
    /// marketplace uses.
    /// </summary>
    string ReadAgentsMarketplaceSourcePath()
    {
        var entry = MarketplaceEntry(AgentsMarketplaceManifest);
        if (entry == null) return null;

        if (!entry.Value.TryGetProperty("source", out var source)) return null;
        if (source.ValueKind != JsonValueKind.Object) return null;
        if (!source.TryGetProperty("path", out var path)) return null;

        return path.ValueKind == JsonValueKind.String ? path.GetString() : null;
    }

    static JsonElement? MarketplaceEntry(AbsolutePath path)
    {
        if (!File.Exists(path)) return null;

        try
        {
            using var document = JsonDocument.Parse(File.ReadAllText(path));

            if (!document.RootElement.TryGetProperty("plugins", out var plugins)) return null;
            if (plugins.ValueKind != JsonValueKind.Array) return null;

            foreach (var plugin in plugins.EnumerateArray())
            {
                // Cloned, because the element does not outlive the document it was read from.
                return plugin.Clone();
            }

            return null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    /// <summary>The path as it is written in the repository, so a message can be pasted into git.</summary>
    string Relative(AbsolutePath path) => Path.GetRelativePath(RootDirectory, path).Replace('\\', '/');

    static string Normalize(string content) => content.Replace("\r\n", "\n");

    static List<string> RelativeFiles(AbsolutePath directory)
    {
        if (!Directory.Exists(directory)) return [];

        return Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
            .Select(x => Path.GetRelativePath(directory, x).Replace('\\', '/'))
            .OrderBy(x => x, StringComparer.Ordinal)
            .ToList();
    }

    static void CopyDirectory(AbsolutePath source, AbsolutePath destination)
    {
        destination.CreateOrCleanDirectory();

        foreach (var file in RelativeFiles(source))
        {
            var target = (string)(destination / file);
            Directory.CreateDirectory(Path.GetDirectoryName(target));
            File.Copy(source / file, target, overwrite: true);
        }
    }

    /// <summary>
    /// Runs git without throwing, so a query whose failure is an ordinary answer — a ref that is not
    /// in this clone, a file that did not exist at a commit — can simply be asked.
    /// </summary>
    static (int ExitCode, string Output, string Error) RunGit(params string[] arguments)
    {
        var startInfo = new ProcessStartInfo("git")
        {
            WorkingDirectory = RootDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        foreach (var argument in arguments)
            startInfo.ArgumentList.Add(argument);

        using var process = Process.Start(startInfo).NotNull("git could not be started.");

        // Both pipes are drained before waiting: a full one would otherwise block git forever.
        var output = process.StandardOutput.ReadToEndAsync();
        var error = process.StandardError.ReadToEndAsync();

        process.WaitForExit();

        return (process.ExitCode, output.GetAwaiter().GetResult(), error.GetAwaiter().GetResult());
    }
}
