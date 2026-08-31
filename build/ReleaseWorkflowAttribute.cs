using System.Collections.Generic;
using System.Linq;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Execution;
using Nuke.Common.Utilities;

/// <summary>
/// The <see cref="GitHubActionsAttribute"/> behind the <c>release</c> workflow. It adds the two
/// credentials the release needs and that NUKE does not generate on its own.
/// </summary>
/// <remarks>
/// <para>
/// <b>nuget.org.</b> The job asks GitHub for an OIDC token, exchanges it with nuget.org for an API
/// key that is valid for one hour and hands that key to the build as the <c>NuGetApiKey</c>
/// parameter — see
/// <a href="https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing">Trusted Publishing</a>.
/// Nothing durable is stored, so there is no key to leak or rotate. The job needs the
/// <c>id-token: write</c> permission, and nuget.org needs a policy naming the repository owner, the
/// repository and this workflow's file name.
/// </para>
/// <para>
/// <b>The changelog push.</b> After publishing, the build commits the finalised changelog back to
/// the main branch. The built-in <c>GITHUB_TOKEN</c> cannot do that: the branch is protected, and
/// <c>github-actions[bot]</c> cannot be granted a bypass — GitHub deliberately keeps it off the
/// bypass list, because that permission would not be scoped to this branch. A GitHub App can be
/// granted one, so the job mints a short-lived installation token instead and the build pushes with
/// that. Set <see cref="AppIdSecret"/> and <see cref="AppPrivateKeySecret"/> to the repository
/// secrets holding the app's credentials.
/// </para>
/// </remarks>
class ReleaseWorkflowAttribute(
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

    /// <summary>Name of the repository secret holding the GitHub App's numeric app id.</summary>
    public string AppIdSecret { get; set; }

    /// <summary>Name of the repository secret holding the GitHub App's PEM private key.</summary>
    public string AppPrivateKeySecret { get; set; }

    protected override GitHubActionsJob GetJobs(
        GitHubActionsImage image,
        IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        var job = base.GetJobs(image, relevantTargets);
        var steps = job.Steps.ToList();

        var runStepIndex = steps.FindIndex(x => x is GitHubActionsRunStep);
        if (runStepIndex < 0)
            return job;

        // Both credentials are short-lived, so acquire them immediately before the build that uses them.
        var addedSteps = new List<GitHubActionsStep> { new NuGetLoginStep { UserSecretName = NuGetUserSecret } };

        if (AppIdSecret != null && AppPrivateKeySecret != null)
            addedSteps.Add(new GitHubAppTokenStep
            {
                AppIdSecretName = AppIdSecret,
                PrivateKeySecretName = AppPrivateKeySecret
            });

        steps.InsertRange(runStepIndex, addedSteps);
        job.Steps = steps.ToArray();

        var runStep = (GitHubActionsRunStep)steps[runStepIndex + addedSteps.Count];

        runStep.Imports[NuGetApiKeyParameterName] =
            $"${{{{ steps.{NuGetLoginStep.StepId}.outputs.NUGET_API_KEY }}}}";

        if (AppIdSecret == null || AppPrivateKeySecret == null) return job;

        runStep.Imports[ChangelogPushTokenParameterName] =
            $"${{{{ steps.{GitHubAppTokenStep.StepId}.outputs.token }}}}";
        runStep.Imports[ChangelogPushAppSlugParameterName] =
            $"${{{{ steps.{GitHubAppTokenStep.StepId}.outputs.app-slug }}}}";

        return job;
    }

    /// <summary>Matches the <c>NuGetApiKey</c> parameter on the build.</summary>
    const string NuGetApiKeyParameterName = "NuGetApiKey";

    /// <summary>Matches the <c>ChangelogPushToken</c> parameter on the build.</summary>
    const string ChangelogPushTokenParameterName = "ChangelogPushToken";

    /// <summary>Matches the <c>ChangelogPushAppSlug</c> parameter on the build.</summary>
    const string ChangelogPushAppSlugParameterName = "ChangelogPushAppSlug";
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

/// <summary>
/// Mints an installation token for the GitHub App that is allowed to push to the protected main
/// branch. The token expires with the job.
/// </summary>
class GitHubAppTokenStep : GitHubActionsStep
{
    public const string StepId = "app-token";

    public string AppIdSecretName { get; set; }
    public string PrivateKeySecretName { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- name: 'Mint the changelog push token'");

        using (writer.Indent())
        {
            writer.WriteLine($"id: {StepId}");
            writer.WriteLine("uses: actions/create-github-app-token@v2");
            // The package is already published by the time the token is used, so a missing or
            // expired app credential must not fail the release. The build says so and moves on.
            writer.WriteLine("continue-on-error: true");
            writer.WriteLine("with:");

            using (writer.Indent())
            {
                writer.WriteLine($"app-id: ${{{{ secrets.{AppIdSecretName} }}}}");
                writer.WriteLine($"private-key: ${{{{ secrets.{PrivateKeySecretName} }}}}");
            }
        }
    }
}
