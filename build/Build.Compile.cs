using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName
partial class Build
{
    AbsolutePath[] PublishProjects => [SourceDirectory / "FluentContracts"];
    AbsolutePath PublishDirectory => OutputDirectory / "publish";
    
    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });
    
    int PublishDegreeOfParallelism => 10;
    Target Compile => _ => _
        .DependsOn(Restore)
        .WhenSkipped(DependencyBehavior.Skip)
        .Executes(() =>
        {
            ReportSummary(_ => _
                    .AddPair("Version", GitVersion.NuGetVersionV2));

            DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .When(IsServerBuild, _ => _
                    .EnableContinuousIntegrationBuild())
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetNoRestore(SucceededTargets.Contains(Restore))
                .SetRepositoryUrl(GitRepository.HttpsUrl));

            DotNetPublish(_ => _
                    .SetConfiguration(Configuration)
                    .EnableNoBuild()
                    .EnableNoLogo()
                    .When(IsServerBuild, _ => _
                        .EnableContinuousIntegrationBuild())
                    .SetAssemblyVersion(GitVersion.AssemblySemVer)
                    .SetFileVersion(GitVersion.AssemblySemFileVer)
                    .SetInformationalVersion(GitVersion.InformationalVersion)
                    .SetRepositoryUrl(GitRepository.HttpsUrl)
                    .CombineWith(PublishProjects, (_, p) => _
                        .SetProject(p)
                        .SetOutput(PublishDirectory / p.Name)),
                PublishDegreeOfParallelism);
        });
}