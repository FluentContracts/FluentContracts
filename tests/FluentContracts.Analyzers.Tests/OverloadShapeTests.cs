using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Xunit;

namespace FluentContracts.Analyzers.Tests;

/// <summary>
/// Pins the 4.0.0 overload shape of every multi-value check by compiling snippets against the real
/// library: values travel as one bracketed argument, and a message can only follow a bracketed set.
/// The two calls that used to be the <c>BeAnyOf</c> trap — several bare strings, and a bare value
/// followed by a bare message — are compile errors now, which is the whole point: the shape enforces
/// the rule, so no analyzer has to guess intent.
/// </summary>
public class OverloadShapeTests
{
    private static CSharpAnalyzerTest<EmptyDiagnosticAnalyzer, DefaultVerifier> Compile(string source)
    {
        var test = new CSharpAnalyzerTest<EmptyDiagnosticAnalyzer, DefaultVerifier>
        {
            TestCode = source,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            CompilerDiagnostics = CompilerDiagnostics.Errors,
        };

        test.TestState.AdditionalReferences.Add(typeof(FluentContracts.Contracts.BaseContract<,>).Assembly);
        return test;
    }

    [Fact]
    public async Task The_triad_compiles()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag, int quantity)
                {
                    tag.Must().BeAnyOf("draft");
                    tag.Must().BeAnyOf(["draft", "published"]);
                    tag.Must().BeAnyOf(["draft", "published"], "Not a state");
                    quantity.Must().NotBeAnyOf([1, 2, 3], "Reserved");
                }
            }
            """;

        await Compile(source).RunAsync();
    }

    [Fact]
    public async Task Several_bare_strings_no_longer_compile()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf({|CS1503:"draft"|}, "published");
                }
            }
            """;

        await Compile(source).RunAsync();
    }

    [Fact]
    public async Task A_bare_value_followed_by_a_bare_message_no_longer_compiles()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf({|CS1503:"draft"|}, "Not a state");
                }
            }
            """;

        await Compile(source).RunAsync();
    }
}
