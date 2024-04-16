using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.CoverallsNet;
using Nuke.Common.Tools.ReportGenerator;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

// ReSharper disable InconsistentNaming
// ReSharper disable AllUnderscoreLocalParameterName
partial class Build 
{
    [Parameter] [Secret] string CoverallRepoKey;
    AbsolutePath ReportDirectory => OutputDirectory / "reports";
    AbsolutePath CoverageReportDirectory => ReportDirectory / "coverage-report";
    AbsolutePath CoverageReportArchive => CoverageReportDirectory.WithExtension("zip");
  
    Target ReportCoverage => _ => _
        .DependsOn(Test)
        .Consumes(Test)
        .Produces(CoverageReportArchive)
        .Requires(() => CoverallRepoKey)
        .Executes(() =>
        {
            ReportGenerator(_ => _
                .SetReports(TestResultDirectory / "*.xml")
                .AddReportTypes(ReportTypes.Xml, ReportTypes.lcov)
                .SetTargetDirectory(CoverageReportDirectory)
                .SetFramework("netcoreapp2.1"));
            
            CoverallsNetTasks.CoverallsNet(
                "--lcov " +
                "--useRelativePaths " +
                $"--basePath {RootDirectory} " +
                $"--input {CoverageReportDirectory / "lcov.info"} " +
                $"--repoToken {CoverallRepoKey} " +
                $"--commitBranch {DevelopmentBranch} " +
                $"--commitId {GitRepository.Commit}");

            // Commenting until --reportgenerator is part of the settings 
            // CoverallsNetTasks.CoverallsNet(_ => _
            //     .SetRepoToken(CoverallRepoKey)
            //     .SetCommitBranch(DevelopmentBranch)
            //     .EnableUserRelativePaths()
            //     .SetInput(CoverageReportDirectory / "lcov.info"));
        });
}