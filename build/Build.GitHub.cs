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
    "pr",
    GitHubActionsImage.UbuntuLatest,
    GitHubActionsImage.WindowsLatest,
    GitHubActionsImage.MacOsLatest,
    OnPullRequestBranches = [MainBranch],
    FetchDepth = 0,
    PublishArtifacts = false,
    InvokedTargets = [nameof(Test), nameof(Pack)])]
[NuGetTrustedPublishing(
    "release",
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = [MainBranch],
    FetchDepth = 0,
    PublishArtifacts = true,
    EnableGitHubToken = true,
    // Contents: create the release and its tag. IdToken: exchange an OIDC token for a
    // short-lived nuget.org key, so no long-lived NuGet API key is stored anywhere.
    WritePermissions = [GitHubActionsPermissions.Contents, GitHubActionsPermissions.IdToken],
    InvokedTargets = [nameof(Test), nameof(ReportCoverage), nameof(Pack), nameof(Publish)],
    ImportSecrets = [nameof(CoverallRepoKey)])]
partial class Build
{
    /// The single long-lived branch. Every merge into it is released.
    const string MainBranch = "master";
    
    // ReSharper disable once InconsistentNaming
    [CI] readonly GitHubActions GitHubActions;
    
    bool Prerelease => false;
    bool Draft => false;
    
    
    [UsedImplicitly]
    Target CreateGitHubRelease => _ => _
        .Requires(() => GitHubActions.Instance.Token != null)
        .TriggeredBy(Publish)
        .ProceedAfterFailure()
        .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
        .OnlyWhenDynamic(() => !SkipRelease)
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
                    // Creating the release also creates the tag at this commit, so the
                    // pipeline never has to push to the protected branch itself.
                    TargetCommitish = GitVersion.Sha,
                    GenerateReleaseNotes = true,
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