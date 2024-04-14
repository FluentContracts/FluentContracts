using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;
using Nuke.Components;

partial class Build : IHazGitVersion
{
    GitVersion GitVersion => From<IHazGitVersion>().Versioning;
    // ReSharper disable once MemberHidesInterfaceMemberWithDefaultImplementation
    GitRepository GitRepository => From<IHazGitRepository>().GitRepository;
}