using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tools.GitHub;
using Octokit;

// ReSharper disable once AllUnderscoreLocalParameterName
[GitHubActions(
    "pr",
    GitHubActionsImage.UbuntuLatest,
    GitHubActionsImage.WindowsLatest,
    GitHubActionsImage.MacOsLatest,
    // Pull requests into a release integration branch get the same checks as ones into master:
    // a major version is assembled there from several pull requests before one final merge.
    OnPullRequestBranches = [MainBranch, ReleaseBranches],
    FetchDepth = 0,
    PublishArtifacts = false,
    InvokedTargets = [nameof(Test), nameof(Pack)])]
[ReleaseWorkflow(
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
    ImportSecrets = [nameof(CoverallRepoKey)],
    // The changelog commit goes back to the protected main branch, which GITHUB_TOKEN may not do.
    AppIdSecret = "CHANGELOG_APP_ID",
    AppPrivateKeySecret = "CHANGELOG_APP_PRIVATE_KEY")]
partial class Build
{
    /// The single long-lived branch. Every merge into it is released.
    const string MainBranch = "master";

    /// Temporary integration branches for a major version (`release/4.0`): pull requests into
    /// one are tested and packed like pull requests into master, and nothing is released until
    /// the branch itself merges into master.
    const string ReleaseBranches = "release/*";
    
    // ReSharper disable once InconsistentNaming
    [CI] readonly GitHubActions GitHubActions;
    
    bool Prerelease => false;
    bool Draft => false;
    
    
    /// <summary>
    /// The files attached to the GitHub release: the packages, plus the archived agent-skill plugin,
    /// so a release carries a copy of exactly the plugin it published.
    /// </summary>
    IEnumerable<AbsolutePath> ReleaseAssetFiles => NuGetPackageFiles.Concat(PluginPackageFiles);

    [UsedImplicitly]
    Target CreateGitHubRelease => _ => _
        .Requires(() => GitHubActions.Instance.Token != null)
        .TriggeredBy(Publish)
        .After(PackPlugin)
        .ProceedAfterFailure()
        .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
        .OnlyWhenDynamic(() => !SkipRelease)
        .Executes(async () =>
        {
            var token = GitHubActions.Instance.Token;
            GitHubTasks.GitHubClient.Credentials = new Credentials(token.NotNull());

            var release = await GetOrCreateReleaseAsync();

            var uploadTasks = ReleaseAssetFiles.Select(async x =>
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