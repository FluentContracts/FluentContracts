using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable InconsistentNaming
// ReSharper disable AllUnderscoreLocalParameterName
partial class Build 
{
    IEnumerable<Project> TestProjects => Partition.GetCurrent(Solution.GetAllProjects("*.Tests"));

    [Parameter] public int TestDegreeOfParallelism { get; } = 1;
    
    AbsolutePath TestResultDirectory => OutputDirectory / "test-results";
    
    Target Test => _ => _
        .DependsOn(Compile)
        .Produces(TestResultDirectory / "*.trx")
        .Produces(TestResultDirectory / "*.xml")
        .Produces(TestResultDirectory / "*.html")
        .Executes(() =>
        {
            try
            {
                var logger = IsLocalBuild ? "html" : "trx";
                
                DotNetTest(_ => _
                        .SetConfiguration(Configuration)
                        .SetNoBuild(SucceededTargets.Contains(Compile))
                        .ResetVerbosity()
                        .SetResultsDirectory(TestResultDirectory)
                        .When(InvokedTargets.Contains(ReportCoverage) || IsServerBuild, _ => _
                            .EnableCollectCoverage()
                            .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
                            .SetExcludeByFile("*.Generated.cs")
                            .SetCoverletOutputFormat(
                                $"\\\"{CoverletOutputFormat.cobertura},{CoverletOutputFormat.json}\\\"")
                            .When(IsServerBuild, _ => _
                                .EnableUseSourceLink()))
                        .CombineWith(TestProjects, (_, v) => _
                                .SetProjectFile(v)
                                .AddLoggers($"{logger};LogFileName={v.Name}.{logger}")
                                .When(InvokedTargets.Contains(ReportCoverage) || IsServerBuild, _ => _
                                    .SetCoverletOutput(TestResultDirectory / $"{v.Name}.xml"))),
                    completeOnFailure: true,
                    degreeOfParallelism: TestDegreeOfParallelism);
            }
            finally
            {
                ReportTestCount();
            }
        });
    
    void ReportTestCount()
    {
        IEnumerable<string> GetOutcomes(AbsolutePath file)
            => XmlTasks.XmlPeek(
                file,
                "/xn:TestRun/xn:Results/xn:UnitTestResult/@outcome",
                ("xn", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010"));

        var resultFiles = TestResultDirectory.GlobFiles("*.trx");
        var outcomes = resultFiles.SelectMany(GetOutcomes).ToList();
        var passedTests = outcomes.Count(x => x == "Passed");
        var failedTests = outcomes.Count(x => x == "Failed");
        var skippedTests = outcomes.Count(x => x == "NotExecuted");

        ReportSummary(_ => _
            .When(failedTests > 0, _ => _
                .AddPair("Failed", failedTests.ToString()))
            .AddPair("Passed", passedTests.ToString())
            .When(skippedTests > 0, _ => _
                .AddPair("Skipped", skippedTests.ToString())));
    }
}