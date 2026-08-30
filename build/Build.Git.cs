using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;

// ReSharper disable InconsistentNaming
partial class Build
{
    [GitRepository] 
    [Required] 
    GitRepository GitRepository;
        
    [GitVersion(Framework = "net10.0", NoFetch = true)]
    [Required]
    GitVersion GitVersion;

    /// <summary>
    /// The version this build would release: the next version after the most recent tag.
    /// GitHubFlow increments the patch by default; put <c>+semver: minor</c> or
    /// <c>+semver: major</c> in a merge commit message to bump further. See GitVersion.yml.
    /// </summary>
    string MajorMinorPatchVersion => GitVersion.MajorMinorPatch;
}
