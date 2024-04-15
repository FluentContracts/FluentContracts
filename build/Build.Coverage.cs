using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.CoverallsNet;
using Nuke.Common.Tools.ReportGenerator;

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
            CoverallsNetTasks.CoverallsNet(_ => _
                .Apply(CoverallsNetSettings));
        });

    public Configure<ReportGeneratorSettings> ReportGeneratorSettings => _ => _
        .AddReportTypes(ReportTypes.Xml);

    Configure<CoverallsNetSettings> CoverallsNetSettings => _ => _
        .SetRepoToken(CoverallRepoKey)
        .SetCommitBranch(DevelopmentBranch)
        .SetInput(CoverageReportDirectory);
}