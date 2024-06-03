using System.IO;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.GitVersion;
using Serilog;
using static Nuke.Common.ChangeLog.ChangelogTasks;
using static Nuke.Common.Tools.Git.GitTasks;
using static Nuke.Common.Tools.GitVersion.GitVersionTasks;

// ReSharper disable InconsistentNaming
// ReSharper disable AllUnderscoreLocalParameterName
partial class Build
{
    [Parameter] readonly bool AutoStash = true;
    [Parameter] readonly bool Major;
    
    AbsolutePath ChangelogFile => RootDirectory / "CHANGELOG.md";
    
    [UsedImplicitly]
    Target Changelog => _ => _
        .Unlisted()
        .OnlyWhenStatic(() => GitRepository.IsOnReleaseBranch() || GitRepository.IsOnHotfixBranch())
        .Executes(() =>
        {
            FinalizeChangelog(ChangelogFile, MajorMinorPatchVersion, GitRepository);
            Log.Information("Please review CHANGELOG.md and press any key to continue ...");
            System.Console.ReadKey();

            Git($"add {ChangelogFile}");
            Git($"commit -m \"chore: {Path.GetFileName(ChangelogFile)} for {MajorMinorPatchVersion}\"");
        });
    
    [UsedImplicitly]
    Target Hotfix => _ => _
        .DependsOn(Changelog)
        .Requires(() => !GitRepository.IsOnHotfixBranch() || GitHasCleanWorkingCopy())
        .Executes(() =>
        {
            var masterVersion = GitVersion(s => s
                .SetFramework("netcoreapp3.1")
                .SetUrl(RootDirectory)
                .SetBranch(MasterBranch)
                .EnableNoFetch()
                .DisableProcessLogOutput()).Result;

            if (!GitRepository.IsOnHotfixBranch())
                Checkout($"{HotfixBranchPrefix}/{masterVersion.Major}.{masterVersion.Minor}.{masterVersion.Patch + 1}", start: MasterBranch);
            else
                FinishReleaseOrHotfix();
        });

    
    string MajorMinorPatchVersion => Major ? $"{GitVersion.Major + 1}.0.0" : GitVersion.MajorMinorPatch;
    
    [UsedImplicitly]
    Target Release => _ => _
        .DependsOn(Changelog)
        .Requires(() => !GitRepository.IsOnReleaseBranch() || GitHasCleanWorkingCopy())
        .Executes(() =>
        {
            if (!GitRepository.IsOnReleaseBranch())
                Checkout($"{ReleaseBranchPrefix}/{MajorMinorPatchVersion}", start: DevelopmentBranch);
            else
                FinishReleaseOrHotfix();
        });
    
    void FinishReleaseOrHotfix()
    {
        Git($"checkout {MasterBranch}");
        Git($"merge --no-ff --no-edit {GitRepository.Branch}");
        Git($"tag {MajorMinorPatchVersion}");

        Git($"checkout {DevelopmentBranch}");
        Git($"merge --no-ff --no-edit {GitRepository.Branch}");

        Git($"branch -D {GitRepository.Branch}");

        Git($"push origin {MasterBranch} {DevelopmentBranch} {MajorMinorPatchVersion}");
    }

    void Checkout(string branch, string start)
    {
        var hasCleanWorkingCopy = GitHasCleanWorkingCopy();

        if (!hasCleanWorkingCopy && AutoStash)
            Git("stash");

        Git($"checkout -b {branch} {start}");

        if (!hasCleanWorkingCopy && AutoStash)
            Git("stash apply");
    }
}