using Nuke.Common;
using Nuke.Common.IO;

// ReSharper disable AllUnderscoreLocalParameterName
partial class Build 
{
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("*/bin", "*/obj").DeleteDirectories();
            OutputDirectory.CreateOrCleanDirectory();
        });
    
    T From<T>()
        where T : INukeBuild
        => (T)(object)this;
}