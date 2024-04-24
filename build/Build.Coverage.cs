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
        .Executes(() =>
        {
            ReportGenerator(_ => _
                .SetReports(TestResultDirectory / "*.xml")
                .AddReportTypes(ReportTypes.Xml, ReportTypes.lcov)
                .When(IsLocalBuild, _ => _
                    .AddReportTypes(ReportTypes.Html))
                .SetTargetDirectory(CoverageReportDirectory)
                .SetFramework("netcoreapp2.1"));

            if (IsLocalBuild) return;

            var coverallsApp =
                OutputDirectory.CreateDownloadableTool(
                    "https://github.com/coverallsapp/coverage-reporter/releases/latest/download/coveralls-windows.exe",
                    "coveralls.exe");

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