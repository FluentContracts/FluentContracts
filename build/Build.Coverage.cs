using System;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Utilities;
using Utils;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

// ReSharper disable InconsistentNaming
// ReSharper disable AllUnderscoreLocalParameterName
partial class Build 
{
    [Parameter] [Secret] string CoverallRepoKey;
    AbsolutePath ReportDirectory => OutputDirectory / "reports";
    AbsolutePath CoverallsAppPath => OutputDirectory / "coveralls.exe";
    AbsolutePath CoverageReportDirectory => ReportDirectory / "coverage-report";
  
    Target ReportCoverage => _ => _
        .DependsOn(Test)
        .Consumes(Test)
        .Requires(() => IsLocalBuild || !CoverallRepoKey.IsNullOrEmpty())
        // Reporting coverage is not worth blocking a release over.
        .ProceedAfterFailure()
        .Executes(() =>
        {
            ReportGenerator(_ => _
                .SetReports(TestResultDirectory / "*.xml")
                .AddReportTypes(ReportTypes.Xml, ReportTypes.lcov)
                .When(_ => IsLocalBuild, _ => _
                    .AddReportTypes(ReportTypes.Html))
                .SetTargetDirectory(CoverageReportDirectory)
                .SetFramework("net10.0"));

            if (IsLocalBuild) return;

            // Coveralls ships one reporter binary per OS. The release agent runs Linux;
            // Windows stays supported for anyone invoking this target on a Windows agent.
            var (assetName, executableName) = EnvironmentInfo.Platform switch
            {
                PlatformFamily.Windows => ("coveralls-windows.exe", "coveralls.exe"),
                PlatformFamily.Linux => ("coveralls-linux", "coveralls"),
                _ => (null, null)
            };

            if (assetName == null)
            {
                Serilog.Log.Error(
                    "Coveralls does not publish a reporter for {Platform}.",
                    EnvironmentInfo.Platform);
                Environment.Exit(1);
            }

            var coverallsApp =
                OutputDirectory.CreateDownloadableTool(
                    $"https://github.com/coverallsapp/coverage-reporter/releases/latest/download/{assetName}",
                    executableName);

            if (coverallsApp == null)
            {
                Serilog.Log.Error("Coveralls CLI could not be found!");
                Environment.Exit(1);
            }

            coverallsApp($"report " +
                         $"{CoverageReportDirectory / "lcov.info"} " +
                         $"--allow-empty " +
                         $"--repo-token={CoverallRepoKey}");
        });
}