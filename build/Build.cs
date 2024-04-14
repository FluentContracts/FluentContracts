using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Components;

partial class Build : NukeBuild
{   
    public static int Main() => Execute<Build>(x => ((IPack)x).Pack);
    
    [Solution(GenerateProjects = true)] readonly Solution Solution;
    Nuke.Common.ProjectModel.Solution IHazSolution.Solution => Solution;

    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath IHazArtifacts.ArtifactsDirectory => RootDirectory / "output";
}
