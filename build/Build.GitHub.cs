
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Components;

[GitHubActions(
    "dev-linux",
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack)])]
[GitHubActions(
    "dev-windows",
    GitHubActionsImage.WindowsLatest,
    OnPushBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack)])]
[GitHubActions(
    "dev-macos",
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack)])]
[GitHubActions(
    "master-release",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranches = [MasterBranch],
    PublishArtifacts = true,
    EnableGitHubToken = true,
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack), nameof(IPublish.Publish)],
    ImportSecrets = [nameof(PublicNuGetApiKey)])]
partial class Build
{
    const string MasterBranch = "master";
    const string DevelopmentBranch = "dev";
    
    // ReSharper disable once InconsistentNaming
    [CI] readonly GitHubActions GitHubActions;
}