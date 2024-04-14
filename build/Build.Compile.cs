using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;

partial class Build 
{
    Configure<DotNetBuildSettings> ICompile.CompileSettings => _ => _
        .When(!ScheduledTargets.Contains(((IPublish)this).Publish) , _ => _
            .ClearProperties());
}