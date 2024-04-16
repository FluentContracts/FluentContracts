using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReportGenerator;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;
using static Nuke.Common.Tools.PowerShell.PowerShellTasks;

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
        .Requires(() => CoverallRepoKey)
        .Executes(() =>
        {
            ReportGenerator(_ => _
                .SetReports(TestResultDirectory / "*.xml")
                .AddReportTypes(ReportTypes.Xml, ReportTypes.lcov)
                .SetTargetDirectory(CoverageReportDirectory)
                .SetFramework("netcoreapp2.1"));

            PowerShell(
                $"-Command \"" +
                $"Invoke-WebRequest " +
                $"-Uri 'https://github.com/coverallsapp/coverage-reporter/releases/latest/download/coveralls-windows.exe' " +
                $"-OutFile '{CoverallsAppPath}'\"");

            var coverallsApp = ToolResolver.GetPathTool(CoverallsAppPath);
            
            coverallsApp($"report " +
                         $"{CoverageReportDirectory / "lcov.info"} " +
                         $"--repo-token={CoverallRepoKey}");
        });
}