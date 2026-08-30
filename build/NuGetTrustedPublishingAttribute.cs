using System.Collections.Generic;
using System.Linq;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Execution;
using Nuke.Common.Utilities;

/// <summary>
/// A <see cref="GitHubActionsAttribute"/> that authenticates to nuget.org with
/// <a href="https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing">Trusted Publishing</a>
/// rather than a long-lived API key.
/// </summary>
/// <remarks>
/// The generated job asks GitHub for an OIDC token, exchanges it with nuget.org for an API key
/// that is valid for one hour, and hands that key to the build as the <c>NuGetApiKey</c>
/// parameter. Nothing durable is stored, so there is no key to leak or rotate.
/// <para>
/// The job needs the <c>id-token: write</c> permission, and nuget.org needs a matching trusted
/// publishing policy naming the repository owner, the repository and this workflow's file name.
/// </para>
/// </remarks>
class NuGetTrustedPublishingAttribute(
    string name,
    GitHubActionsImage image,
    params GitHubActionsImage[] images)
    : GitHubActionsAttribute(name, image, images)
{
    /// <summary>
    /// Name of the repository secret holding the nuget.org username (the profile name, not the
    /// e-mail address). It is not a credential, but keeping it in a secret follows NuGet's guidance.
    /// </summary>
    public string NuGetUserSecret { get; set; } = "NUGET_USER";

    protected override GitHubActionsJob GetJobs(
        GitHubActionsImage image,
        IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        var job = base.GetJobs(image, relevantTargets);
        var steps = job.Steps.ToList();

        var runStepIndex = steps.FindIndex(x => x is GitHubActionsRunStep);
        if (runStepIndex < 0)
            return job;

        // The key lives for an hour, so acquire it immediately before the build that pushes.
        steps.Insert(runStepIndex, new NuGetLoginStep { UserSecretName = NuGetUserSecret });
        job.Steps = steps.ToArray();

        var runStep = (GitHubActionsRunStep)steps[runStepIndex + 1];
        runStep.Imports[NuGetApiKeyParameterName] =
            $"${{{{ steps.{NuGetLoginStep.StepId}.outputs.NUGET_API_KEY }}}}";

        return job;
    }

    /// <summary>Matches the <c>NuGetApiKey</c> parameter on the build.</summary>
    const string NuGetApiKeyParameterName = "NuGetApiKey";
}

/// <summary>
/// Exchanges the job's GitHub OIDC token for a short-lived nuget.org API key.
/// </summary>
class NuGetLoginStep : GitHubActionsStep
{
    public const string StepId = "nuget-login";

    public string UserSecretName { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- name: 'NuGet login (OIDC)'");

        using (writer.Indent())
        {
            writer.WriteLine($"id: {StepId}");
            writer.WriteLine("uses: NuGet/login@v1");
            writer.WriteLine("with:");

            using (writer.Indent())
                writer.WriteLine($"user: ${{{{ secrets.{UserSecretName} }}}}");
        }
    }
}
