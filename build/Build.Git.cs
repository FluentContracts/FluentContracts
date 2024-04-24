using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;

// ReSharper disable InconsistentNaming
partial class Build
{
    [GitRepository] 
    [Required] 
    GitRepository GitRepository;
        
    [GitVersion(Framework = "net5.0", NoFetch = true)]
    [Required]
    GitVersion GitVersion;
}