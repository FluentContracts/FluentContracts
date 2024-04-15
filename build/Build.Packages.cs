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
    
    IEnumerable<AbsolutePath> PushPackageFiles => PackagesDirectory.GlobFiles("*.nupkg");

    bool PushCompleteOnFailure => true;
    int PushDegreeOfParallelism => 5;
    
    Target Publish => _ => _
        .DependsOn(Test, Pack)
        .Requires(() => NuGetApiKey)
        .Executes(() =>
        {
            DotNetNuGetPush(_ => _
                    .SetSource(NuGetSource)
                    .SetApiKey(NuGetApiKey)
                    .CombineWith(PushPackageFiles, (_, v) => _
                        .SetTargetPath(v)),
                PushDegreeOfParallelism,
                PushCompleteOnFailure);
        });
}