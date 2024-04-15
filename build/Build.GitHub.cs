using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;

[GitHubActions(
    "dev-linux",
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    ImportSecrets = [nameof(CoverallRepoKey)],
    InvokedTargets = [nameof(Test), nameof(ReportCoverage), nameof(Pack)])]
[GitHubActions(
    "dev-windows",
    GitHubActionsImage.WindowsLatest,
    OnPushBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    ImportSecrets = [nameof(CoverallRepoKey)],
    InvokedTargets = [nameof(Test), nameof(ReportCoverage), nameof(Pack)])]
[GitHubActions(
    "dev-macos",
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    ImportSecrets = [nameof(CoverallRepoKey)],
    InvokedTargets = [nameof(Test), nameof(ReportCoverage), nameof(Pack)])]
[GitHubActions(
    "master-release",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranches = [MasterBranch],
    PublishArtifacts = true,
    EnableGitHubToken = true,
    InvokedTargets = [nameof(Test), nameof(Pack), nameof(Publish)],
    ImportSecrets = [nameof(NuGetApiKey)])]
partial class Build
{
    const string MasterBranch = "master";
    const string DevelopmentBranch = "dev";
    
    // ReSharper disable once InconsistentNaming
    [CI] readonly GitHubActions GitHubActions;
}