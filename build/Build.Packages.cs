using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable InconsistentNaming
// ReSharper disable AllUnderscoreLocalParameterName
partial class Build
{
    [Parameter] string NuGetSource = "https://api.nuget.org/v3/index.json";
    [Parameter] [Secret] string NuGetApiKey;
    
    IEnumerable<AbsolutePath> NuGetPackageFiles => PackagesDirectory.GlobFiles("*.nupkg");

    bool PushCompleteOnFailure => true;
    int PushDegreeOfParallelism => 5;
    
    Target Publish => _ => _
        .DependsOn(Test, Pack)
        .Requires(() => NuGetApiKey)
        // A merge can opt out of releasing; the build and tests still run.
        .OnlyWhenDynamic(() => !SkipRelease)
        .Executes(() =>
        {
            DotNetNuGetPush(_ => _
                    .SetSource(NuGetSource)
                    .SetApiKey(NuGetApiKey)
                    // Every merge releases, so a re-run of a workflow must not fail
                    // just because that version already exists.
                    .EnableSkipDuplicate()
                    .CombineWith(NuGetPackageFiles, (_, v) => _
                        .SetTargetPath(v)),
                PushDegreeOfParallelism,
                PushCompleteOnFailure);
        });
}