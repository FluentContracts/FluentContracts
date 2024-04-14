using System.Collections.Generic;
using Nuke.Common;
using Nuke.Components;
using Nuke.Common.ProjectModel;

// ReSharper disable AllUnderscoreLocalParameterName
partial class Build : IReportCoverage
{
    IEnumerable<Project> ITest.TestProjects => Partition.GetCurrent(Solution.GetAllProjects("*.Tests"));

    [Parameter] public int TestDegreeOfParallelism { get; } = 1;
    
    bool IReportCoverage.CreateCoverageHtmlReport => true;
    bool IReportCoverage.ReportToCodecov => true;
    
    Target ITest.Test => _ => _
        .Inherit<ITest>()
        .Partition(2);
}