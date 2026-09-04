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
/// <b>The repository root is the plugin.</b> There is exactly one <c>skills/</c> tree and all three
/// harnesses read it in place: the marketplace entry's source resolves to the marketplace root, so
/// Claude Code's default <c>skills/</c> scan finds it; the Codex manifest points at <c>./skills/</c>
/// from the same root; and the Gemini extension manifest discovers it there too. Hence the plugin
/// manifests sitting at the root rather than under a plugin directory of their own.
/// </para>
/// <para>
/// A plugin directory holding a second copy of the tree is the obvious alternative and was the first
/// shape this took. It is worse twice over: the copy is real duplication that has to be regenerated
/// and gated against drift, and it buys nothing the root layout does not already give. Replacing the
/// copy with a <em>symlink</em> would be worse still — Git for Windows tests at clone time whether it
/// can create links, and where it cannot it sets <c>core.symlinks=false</c> and writes the link out
/// as a small text file holding the target path, so the plugin would ship no skills at all with
/// nothing anywhere to say so.
/// </para>
/// <para>
/// What remains is a build target because none of it fails on its own. The folders are loaded
/// directly by three harnesses, so a frontmatter name that no longer matches its directory silently
/// never loads; and the version the manifests carry is bumped by hand, so skills shipped without a
/// bump never reach an agent already holding the old copy. Each is a change that looks correct in
/// the repository and reaches nobody.
/// </para>
/// </remarks>
partial class Build
{
    /// <summary>The skills tree that every distribution surface serves.</summary>
    AbsolutePath SkillsDirectory => RootDirectory / "skills";

    /// <summary>The plugin's own manifest, whose version the marketplace reads.</summary>
    AbsolutePath PluginManifest => RootDirectory / ".claude-plugin" / "plugin.json";

    /// <summary>The Codex-native copy of the plugin manifest.</summary>
    AbsolutePath CodexPluginManifest => RootDirectory / ".codex-plugin" / "plugin.json";

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

    /// <summary>
    /// The files at the repository root that belong to the plugin rather than to the library. The
    /// root is the plugin, so an archive of it has to be assembled from a list rather than taken
    /// wholesale.
    /// </summary>
    IReadOnlyList<AbsolutePath> PluginRootFiles =>
    [
        PluginManifest,
        CodexPluginManifest,
        MarketplaceManifest,
        AgentsMarketplaceManifest,
        GeminiExtensionManifest,
        RootDirectory / "LICENSE"
    ];

    /// <summary>The name every manifest must agree on.</summary>
    const string PluginName = "fluentcontracts";

    /// <summary>
    /// The plugin's location, as the two marketplace manifests spell it: the marketplace root, which
    /// is the repository root, so that the one <see cref="SkillsDirectory"/> is what gets served.
    /// </summary>
    /// <remarks>
    /// Documented for Claude Code, which resolves a marketplace-root source and then runs its default
    /// <c>skills/</c> scan against it. Codex's own documentation requires only that the path be
    /// relative to the marketplace root, be <c>./</c>-prefixed and stay inside that root — all of
    /// which the root itself satisfies — but every example it gives is a subdirectory, so the root
    /// form is undocumented rather than blessed there. If it ever proves unsupported, the fallback is
    /// a Codex-only plugin directory carrying its own copy of the skills.
    /// </remarks>
    const string PluginSourcePath = "./";

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
        .DependsOn(CheckSkillDocuments, CheckSkillCatalogue, CheckPluginManifests, CheckPluginVersion)
        .Unlisted();

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

    /// <summary>The reference whose catalogue section is generated from the library itself.</summary>
    AbsolutePath SkillCheatsheet => SkillsDirectory / PluginName / "references" / "cheatsheet.md";

    /// <summary>Opens the generated region of <see cref="SkillCheatsheet"/>.</summary>
    const string CatalogueBeginMarker = "<!-- BEGIN GENERATED CATALOGUE -->";

    /// <summary>Closes the generated region of <see cref="SkillCheatsheet"/>.</summary>
    const string CatalogueEndMarker = "<!-- END GENERATED CATALOGUE -->";

    /// <summary>
    /// The order the catalogue's areas are rendered in, from the broadly useful to the niche. An area
    /// missing from here is appended rather than dropped, so a contract in a brand new namespace
    /// still reaches the catalogue — and the gate still notices it.
    /// </summary>
    static readonly string[] CatalogueAreaOrder =
        [CoreArea, "Text", "Numeric", "Struct", "Collections", "Web", "Streams", "IO"];

    /// <summary>Readable headings for the areas; anything absent is titled by its namespace.</summary>
    static readonly Dictionary<string, string> CatalogueAreaHeadings = new()
    {
        // Not "the shared chain": `Collection` also lives in the root namespace, and it is a base
        // for the collection contracts rather than something every contract inherits.
        [CoreArea] = "Core",
        ["Struct"] = "Values, dates and times",
        ["Collections"] = "Collections",
        ["Web"] = "URIs",
        ["Streams"] = "Streams",
        ["IO"] = "Files and directories"
    };

    /// <summary>
    /// Regenerates the catalogue in <see cref="SkillCheatsheet"/> from the built library. Run it
    /// after adding or removing a check; <see cref="CheckSkillCatalogue"/> fails the build until you
    /// have.
    /// </summary>
    [UsedImplicitly]
    Target SyncSkillCatalogue => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var content = SkillCheatsheet.ReadAllText();
            var updated = SpliceCatalogue(content, RenderCatalogue());

            if (updated == content)
            {
                Log.Information("{File} is already current.", Relative(SkillCheatsheet));
                return;
            }

            SkillCheatsheet.WriteAllText(updated);
            Log.Information("Regenerated the catalogue in {File}.", Relative(SkillCheatsheet));
        });

    /// <summary>
    /// Fails when the library has a contract or a check the skill's cheatsheet does not, or the
    /// other way round.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The cheatsheet is how an agent answers "does this check exist?" without guessing, so a
    /// catalogue that has fallen behind the library is worse than no catalogue: it reads as
    /// authoritative and is wrong. Adding a check and forgetting the skill is the easiest possible
    /// miss — the two live nowhere near each other, and every test still passes.
    /// </para>
    /// <para>
    /// So the section is generated from the built assembly by the same reflection that produces
    /// <c>docs/SupportedContracts.md</c>, and this fails the build when the committed copy has
    /// drifted, naming the contracts that differ rather than just saying the file changed.
    /// </para>
    /// </remarks>
    [UsedImplicitly]
    Target CheckSkillCatalogue => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var expected = RenderCatalogue();
            var found = ExtractCatalogue(SkillCheatsheet.ReadAllText());

            Assert.True(
                found != null,
                $"{Relative(SkillCheatsheet)} has no generated catalogue region. It must contain "
                + $"{CatalogueBeginMarker} and {CatalogueEndMarker}; restore them and run "
                + $"./build.sh {nameof(SyncSkillCatalogue)}.");

            var differences = CatalogueDifferences(expected, found);

            Assert.True(
                differences.Count == 0,
                $"The skill's catalogue no longer matches the library:{Environment.NewLine}"
                + string.Join(Environment.NewLine, differences.Select(x => $"  {x}"))
                + Environment.NewLine + Environment.NewLine
                + $"Run ./build.sh {nameof(SyncSkillCatalogue)} and commit the result, then bump the "
                + "plugin version so the change reaches agents holding the old copy."
                + Environment.NewLine
                + "The catalogue is what an agent trusts instead of guessing a check name, so one "
                + "that has fallen behind is worse than none.");

            Log.Information("The skill's catalogue matches the library.");
        });

    /// <summary>The catalogue as the library currently defines it, newline-normalised.</summary>
    string RenderCatalogue()
    {
        var contracts = ExtractClasses();
        var areas = contracts
            .GroupBy(x => x.Area)
            .OrderBy(x => Array.IndexOf(CatalogueAreaOrder, x.Key) is var index && index >= 0
                ? index
                : CatalogueAreaOrder.Length)
            .ThenBy(x => x.Key, StringComparer.Ordinal);

        var lines = new List<string>();

        foreach (var area in areas)
        {
            lines.Add(string.Empty);
            lines.Add($"### {(CatalogueAreaHeadings.TryGetValue(area.Key, out var heading) ? heading : area.Key)}");
            lines.Add(string.Empty);

            // Alphabetical, so the rendering does not depend on the order reflection happens to
            // return types in — a generated file a gate compares against has to be reproducible.
            foreach (var contract in area.OrderBy(x => x.Name, StringComparer.Ordinal))
            {
                var extends = contract.Extends == null ? string.Empty : $" (extends `{contract.Extends}`)";
                var checks = string.Join(", ", contract.Contracts.Select(x => $"`{x}`"));
                lines.Add($"- **`{contract.Name}`**{extends} — {checks}");
            }
        }

        return string.Join("\n", lines).Trim('\n');
    }

    /// <summary>The generated region of <paramref name="content"/>, or <c>null</c> when it has none.</summary>
    static string ExtractCatalogue(string content)
    {
        var normalised = content.Replace("\r\n", "\n");
        var start = normalised.IndexOf(CatalogueBeginMarker, StringComparison.Ordinal);
        var end = normalised.IndexOf(CatalogueEndMarker, StringComparison.Ordinal);

        if (start < 0 || end < start) return null;

        return normalised[(start + CatalogueBeginMarker.Length)..end].Trim('\n');
    }

    /// <summary>
    /// <paramref name="content"/> with its generated region replaced by <paramref name="catalogue"/>,
    /// keeping whichever newline the file already uses so a regeneration on Windows does not rewrite
    /// every line of it.
    /// </summary>
    string SpliceCatalogue(string content, string catalogue)
    {
        var start = content.IndexOf(CatalogueBeginMarker, StringComparison.Ordinal);
        var end = content.IndexOf(CatalogueEndMarker, StringComparison.Ordinal);

        Assert.True(
            start >= 0 && end > start,
            $"{Relative(SkillCheatsheet)} has no generated catalogue region to write into. It must "
            + $"contain {CatalogueBeginMarker} and {CatalogueEndMarker}.");

        var newline = content.Contains("\r\n") ? "\r\n" : "\n";
        var body = catalogue.Replace("\n", newline);

        return content[..(start + CatalogueBeginMarker.Length)]
            + newline + newline + body + newline + newline
            + content[end..];
    }

    /// <summary>
    /// How <paramref name="found"/> differs from <paramref name="expected"/>, described per contract
    /// so the message says what changed in the library rather than that a file changed.
    /// </summary>
    static List<string> CatalogueDifferences(string expected, string found)
    {
        var differences = new List<string>();

        if (expected == found) return differences;

        var expectedContracts = CatalogueContracts(expected);
        var foundContracts = CatalogueContracts(found);

        foreach (var contract in expectedContracts.Where(x => !foundContracts.ContainsKey(x.Key)))
            differences.Add($"`{contract.Key}` is in the library but not in the cheatsheet.");

        foreach (var contract in foundContracts.Where(x => !expectedContracts.ContainsKey(x.Key)))
            differences.Add($"`{contract.Key}` is in the cheatsheet but not in the library.");

        foreach (var contract in expectedContracts.Where(x =>
                     foundContracts.TryGetValue(x.Key, out var line) && line != x.Value))
        {
            differences.Add(
                $"`{contract.Key}`'s checks changed."
                + $"{Environment.NewLine}    library:    {contract.Value}"
                + $"{Environment.NewLine}    cheatsheet: {foundContracts[contract.Key]}");
        }

        // A heading moved or an area appeared, with every contract line still identical.
        if (differences.Count == 0)
            differences.Add("the catalogue's sections differ from the generated ones.");

        return differences;
    }

    /// <summary>The contract lines of a rendered catalogue, keyed by contract name.</summary>
    static Dictionary<string, string> CatalogueContracts(string catalogue)
    {
        var contracts = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var line in catalogue.Split('\n').Select(x => x.Trim()))
        {
            var match = Regex.Match(line, @"^- \*\*`(?<name>[^`]+)`\*\*");
            if (match.Success) contracts[match.Groups["name"].Value] = line;
        }

        return contracts;
    }

    /// <summary>
    /// Archives the plugin, so a release carries a copy of exactly what it published. Attached to the
    /// GitHub release next to the packages.
    /// </summary>
    /// <remarks>
    /// Staged from <see cref="PluginRootFiles"/> and <see cref="SkillsDirectory"/> rather than zipped
    /// wholesale: the plugin's root is the repository's root, and an archive of that would be the
    /// whole library.
    /// </remarks>
    [UsedImplicitly]
    Target PackPlugin => _ => _
        .TriggeredBy(Pack)
        .After(Pack)
        .DependsOn(VerifySkills)
        .Executes(() =>
        {
            PluginPackagesDirectory.CreateOrCleanDirectory();

            var staged = PluginPackagesDirectory / "staging";
            staged.CreateOrCleanDirectory();

            foreach (var file in PluginRootFiles)
                CopyInto(file, staged / Relative(file));

            foreach (var file in RelativeFiles(SkillsDirectory))
                CopyInto(SkillsDirectory / file, staged / Relative(SkillsDirectory) / file);

            var archive = PluginPackagesDirectory / $"{PluginName}-plugin-{PluginVersion}.zip";
            ZipFile.CreateFromDirectory(staged, archive, CompressionLevel.Optimal, includeBaseDirectory: false);

            // The staging tree is an implementation detail; leaving it beside the archive would put a
            // loose second copy of the plugin into the release artifacts.
            staged.DeleteDirectory();

            ReportSummary(_ => _.AddPair("Plugin", $"{PluginName} {PluginVersion}"));
        });

    /// <summary>Copies <paramref name="source"/> to <paramref name="target"/>, creating its parent.</summary>
    static void CopyInto(AbsolutePath source, AbsolutePath target)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(target));
        File.Copy(source, target, overwrite: true);
    }

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

        var published = $"{Relative(SkillsDirectory)}/";

        return $"{diff.Output}\n{untracked.Output}"
            .Split('\n')
            .Select(x => x.Trim())
            .Where(x => x.StartsWith(published, StringComparison.Ordinal))
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

    static List<string> RelativeFiles(AbsolutePath directory)
    {
        if (!Directory.Exists(directory)) return [];

        return Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
            .Select(x => Path.GetRelativePath(directory, x).Replace('\\', '/'))
            .OrderBy(x => x, StringComparer.Ordinal)
            .ToList();
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
