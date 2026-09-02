using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace FluentContracts.Analyzers;

/// <summary>
/// FC0001: a string argument's <c>BeAnyOf</c>/<c>NotBeAnyOf</c> call bound to the deprecated
/// message-first overload, so its first value is silently taken as the exception message and the
/// check runs against the wrong set.
/// </summary>
/// <remarks>
/// The <c>[Obsolete]</c> on the overload already raises CS0618 wherever it is used; this analyzer
/// adds what that warning cannot say — that <em>this particular call site</em> is losing its first
/// value, naming it — and only fires where the trap is live: the argument type is <c>string</c> and
/// the arguments arrived in expanded form. A caller passing an explicit array chose the message
/// deliberately and is left to CS0618 alone.
/// </remarks>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MessageFirstAnyOfAnalyzer : DiagnosticAnalyzer
{
    /// <summary>The diagnostic id reported by this analyzer.</summary>
    public const string DiagnosticId = "FC0001";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        title: "The first value is taken as the message",
        messageFormat: "This call binds to the message-first overload of '{0}', so {1} becomes the exception message " +
                       "and is not checked as a value — pass the values as a sequence to check them all",
        category: "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "On a string argument, the compiler prefers the deprecated BeAnyOf(string? message, params string[]) " +
                     "overload for a call like BeAnyOf(\"a\", \"b\"), because it has more declared parameters and both " +
                     "candidates are applicable only in expanded form. The first value silently becomes the exception " +
                     "message and the check runs against the remaining values only. Pass the values as a sequence — " +
                     "BeAnyOf(new[] { \"a\", \"b\" }) — which cannot confuse a value for a message.",
        helpLinkUri: "https://github.com/FluentContracts/FluentContracts/issues/43");

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(Rule);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterOperationAction(AnalyzeInvocation, OperationKind.Invocation);
    }

    private static void AnalyzeInvocation(OperationAnalysisContext context)
    {
        var invocation = (IInvocationOperation)context.Operation;
        var method = invocation.TargetMethod;

        if (method.Name is not ("BeAnyOf" or "NotBeAnyOf")) return;

        // The message-first shape: (string? message, params string[] values). The params element
        // type being string is what makes the trap live — on any other element type a string
        // cannot be a candidate value and no call is ambiguous.
        if (method.Parameters.Length != 2) return;
        if (method.Parameters[0].Type.SpecialType != SpecialType.System_String) return;
        if (!method.Parameters[1].IsParams) return;
        if (method.Parameters[1].Type is not IArrayTypeSymbol paramsArray) return;
        if (paramsArray.ElementType.SpecialType != SpecialType.System_String) return;

        if (!IsFluentContractsEqualityContract(method.ContainingType)) return;

        // Expanded form only: BeAnyOf("a", "b"). A caller writing BeAnyOf("msg", new[] { "a" })
        // passed the array deliberately, so the message is not an accident there.
        var valuesArgument = FindArgument(invocation, ordinal: 1);
        if (valuesArgument is null || valuesArgument.ArgumentKind != ArgumentKind.ParamArray) return;

        var messageArgument = FindArgument(invocation, ordinal: 0);
        if (messageArgument is null) return;

        var messageText = messageArgument.ArgumentKind == ArgumentKind.DefaultValue
            ? "the omitted message"
            : $"'{messageArgument.Value.Syntax}'";

        context.ReportDiagnostic(Diagnostic.Create(
            Rule,
            invocation.Syntax.GetLocation(),
            method.Name,
            messageText));
    }

    private static IArgumentOperation? FindArgument(IInvocationOperation invocation, int ordinal)
    {
        foreach (var argument in invocation.Arguments)
        {
            if (argument.Parameter?.Ordinal == ordinal) return argument;
        }

        return null;
    }

    private static bool IsFluentContractsEqualityContract(INamedTypeSymbol type)
    {
        var definition = type.OriginalDefinition;

        return definition.Name == "EqualityContract"
               && definition.ContainingNamespace.ToDisplayString() == "FluentContracts.Contracts";
    }
}
