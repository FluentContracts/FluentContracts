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

    // FluentContracts multi-targets, but publishing (and the docs generator that reads
    // its output) needs a single framework. Keep this on the newest target.
    const string PublishFramework = "net8.0";
    
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
                    .AddPair("Version", GitVersion.SemVer));

            DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .When(_ => IsServerBuild, _ => _
                    .EnableContinuousIntegrationBuild())
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetNoRestore(SucceededTargets.Contains(Restore))
                .SetRepositoryUrl(GitRepository.HttpsUrl));

            DotNetPublish(_ => _
                    .SetConfiguration(Configuration)
                    .SetFramework(PublishFramework)
                    .EnableNoBuild()
                    .EnableNoLogo()
                    .When(_ => IsServerBuild, _ => _
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