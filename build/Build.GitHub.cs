using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitHub;
using Octokit;

// ReSharper disable once AllUnderscoreLocalParameterName
[GitHubActions(
    "dev-linux",
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = [DevelopmentBranch],
    OnPullRequestBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    ImportSecrets = [nameof(CoverallRepoKey)],
    InvokedTargets = [nameof(Test), nameof(Pack)])]
[GitHubActions(
    "dev-windows",
    GitHubActionsImage.WindowsLatest,
    OnPushBranches = [DevelopmentBranch],
    OnPullRequestBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    ImportSecrets = [nameof(CoverallRepoKey)],
    InvokedTargets = [nameof(Test), nameof(ReportCoverage), nameof(Pack)])]
[GitHubActions(
    "dev-macos",
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = [DevelopmentBranch],
    OnPullRequestBranches = [DevelopmentBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    ImportSecrets = [nameof(CoverallRepoKey)],
    InvokedTargets = [nameof(Test), nameof(Pack)])]
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
    const string DevelopmentBranch = "develop";
    const string ReleaseBranchPrefix = "release";
    const string HotfixBranchPrefix = "hotfix";
    
    // ReSharper disable once InconsistentNaming
    [CI] readonly GitHubActions GitHubActions;
    
    bool Prerelease => false;
    bool Draft => false;
    
    
    [UsedImplicitly]
    Target CreateGitHubRelease => _ => _
        .Requires(() => GitHubActions.Instance.Token != null)
        //.TriggeredBy(Publish)
        .ProceedAfterFailure()
        .OnlyWhenStatic(() => GitRepository.IsOnMasterBranch())
        .Executes(async () =>
        {
            var token = GitHubActions.Instance.Token;
            GitHubTasks.GitHubClient.Credentials = new Credentials(token.NotNull());

            var release = await GetOrCreateReleaseAsync();

            var uploadTasks = NuGetPackageFiles.Select(async x =>
            {
                await using var assetFile = File.OpenRead(x);
                var asset = new ReleaseAssetUpload
                {
                    FileName = x.Name,
                    ContentType = "application/octet-stream",
                    RawData = assetFile
                };
                await GitHubTasks.GitHubClient.Repository.Release.UploadAsset(release, asset);
            }).ToArray();

            Task.WaitAll(uploadTasks);
        });
    
    async Task<Release> GetOrCreateReleaseAsync()
    {
        try
        {
            return await GitHubTasks.GitHubClient.Repository.Release.Create(
                GitRepository.GetGitHubOwner(),
                GitRepository.GetGitHubName(),
                new NewRelease(MajorMinorPatchVersion)
                {
                    Name = MajorMinorPatchVersion,
                    Prerelease = Prerelease,
                    Draft = Draft,
                });

        }
        catch
        {
            return await GitHubTasks.GitHubClient.Repository.Release.Get(
                GitRepository.GetGitHubOwner(),
                GitRepository.GetGitHubName(),
                MajorMinorPatchVersion);
        }
    }
}