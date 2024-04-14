using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Components;

// ReSharper disable AllUnderscoreLocalParameterName
partial class Build 
{
    Target Clean => _ => _
        .Before<IRestore>()
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("*/bin", "*/obj").DeleteDirectories();
            OutputDirectory.CreateOrCleanDirectory();
        });
    
    T From<T>()
        where T : INukeBuild
        => (T)(object)this;
}