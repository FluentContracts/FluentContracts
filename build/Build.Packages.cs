using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;

// ReSharper disable AllUnderscoreLocalParameterName
partial class Build : IPublish
{
    string IPublish.NuGetSource => "https://api.nuget.org/v3/index.json";
    string IPublish.NuGetApiKey => PublicNuGetApiKey;

    Configure<DotNetPublishSettings> ICompile.PublishSettings => _ => _
        .When(!ScheduledTargets.Contains(((IPublish)this).Publish) , _ => _
            .ClearProperties());

    // ReSharper disable once InconsistentNaming
    [Parameter] [Secret] readonly string PublicNuGetApiKey;
    
    Target IPublish.Publish => _ => _
        .Inherit<IPublish>()
        .Consumes(From<IPack>().Pack)
        .WhenSkipped(DependencyBehavior.Execute);
}