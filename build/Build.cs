using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;

// ReSharper disable InconsistentNaming
partial class Build : NukeBuild
{   
    public static int Main() => Execute<Build>(x => x.Pack);
    
    [Parameter] Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
    
    [Solution] readonly Solution Solution;

    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath SourceDirectory => RootDirectory / "src";
}
