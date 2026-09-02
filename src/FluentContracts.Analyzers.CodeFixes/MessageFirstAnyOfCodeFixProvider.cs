using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentContracts.Analyzers;

/// <summary>
/// Fixes FC0001 by moving to the sequence overload, which cannot confuse a value for a message.
/// Two fixes are offered because only the author knows what the first argument was meant to be:
/// check every argument as a value, or keep the first one as the message.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MessageFirstAnyOfCodeFixProvider))]
public sealed class MessageFirstAnyOfCodeFixProvider : CodeFixProvider
{
    /// <inheritdoc/>
    public override ImmutableArray<string> FixableDiagnosticIds { get; } =
        ImmutableArray.Create(MessageFirstAnyOfAnalyzer.DiagnosticId);

    /// <inheritdoc/>
    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    /// <inheritdoc/>
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

        if (root?.FindNode(context.Span, getInnermostNodeForTie: true)
                ?.FirstAncestorOrSelf<InvocationExpressionSyntax>() is not { } invocation)
            return;

        var arguments = invocation.ArgumentList.Arguments;
        if (arguments.Count < 2) return;

        context.RegisterCodeFix(
            CodeAction.Create(
                "Check every argument as a value",
                _ => Task.FromResult(RewriteAsValues(context.Document, root!, invocation, keepMessage: false)),
                equivalenceKey: "FC0001_AllValues"),
            context.Diagnostics);

        context.RegisterCodeFix(
            CodeAction.Create(
                "Keep the first argument as the message",
                _ => Task.FromResult(RewriteAsValues(context.Document, root!, invocation, keepMessage: true)),
                equivalenceKey: "FC0001_KeepMessage"),
            context.Diagnostics);
    }

    private static Document RewriteAsValues(
        Document document,
        SyntaxNode root,
        InvocationExpressionSyntax invocation,
        bool keepMessage)
    {
        var arguments = invocation.ArgumentList.Arguments;
        var values = keepMessage ? arguments.Skip(1) : arguments;

        // Built from the original argument text, so each value keeps the formatting the caller
        // wrote and the whole array stays on one line.
        var array = SyntaxFactory.ParseExpression(
            $"new[] {{ {string.Join(", ", values.Select(a => a.Expression.ToString()))} }}");

        var newArguments = new List<ArgumentSyntax> { SyntaxFactory.Argument(array) };
        if (keepMessage) newArguments.Add(arguments[0]);

        var newInvocation = invocation.WithArgumentList(
            SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(newArguments)));

        return document.WithSyntaxRoot(root.ReplaceNode(invocation, newInvocation));
    }
}
