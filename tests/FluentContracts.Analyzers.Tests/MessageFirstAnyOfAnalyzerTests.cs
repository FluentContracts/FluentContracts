using System.Threading.Tasks;
using FluentContracts.Analyzers;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Xunit;

namespace FluentContracts.Analyzers.Tests;

/// <summary>
/// FC0001 fires exactly where the message-first trap is live: a string argument, expanded-form
/// arguments, the deprecated overload. Everything else — a single value, an explicit array, a
/// non-string contract — stays silent, because those calls check what they say they check.
/// The snippets compile against the real library, so the tests bind the same overloads consumers do.
/// </summary>
public class MessageFirstAnyOfAnalyzerTests
{
    private static CSharpAnalyzerTest<MessageFirstAnyOfAnalyzer, DefaultVerifier> AnalyzerTest(string source)
    {
        var test = new CSharpAnalyzerTest<MessageFirstAnyOfAnalyzer, DefaultVerifier>
        {
            TestCode = source,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
        };

        test.TestState.AdditionalReferences.Add(typeof(FluentContracts.Contracts.BaseContract<,>).Assembly);
        return test;
    }

    [Fact]
    public async Task Flags_a_string_argument_bound_to_the_message_first_overload()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    {|FC0001:tag.Must().BeAnyOf("a", "b")|};
                }
            }
            """;

        await AnalyzerTest(source).RunAsync();
    }

    [Fact]
    public async Task Flags_NotBeAnyOf_the_same_way()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    {|FC0001:tag.Must().NotBeAnyOf("a", "b", "c")|};
                }
            }
            """;

        await AnalyzerTest(source).RunAsync();
    }

    [Fact]
    public async Task The_message_names_the_swallowed_value()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf("draft", "published");
                }
            }
            """;

        var test = AnalyzerTest(source);
        test.ExpectedDiagnostics.Add(
            new DiagnosticResult(MessageFirstAnyOfAnalyzer.DiagnosticId, Microsoft.CodeAnalysis.DiagnosticSeverity.Warning)
                .WithSpan(7, 9, 7, 49)
                .WithMessage(
                    "This call binds to the message-first overload of 'BeAnyOf', so '\"draft\"' becomes the " +
                    "exception message and is not checked as a value — pass the values as a sequence to check them all"));

        await test.RunAsync();
    }

    [Fact]
    public async Task A_single_value_binds_its_own_overload_and_stays_silent()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf("a");
                }
            }
            """;

        await AnalyzerTest(source).RunAsync();
    }

    [Fact]
    public async Task The_sequence_overload_stays_silent()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf(new[] { "a", "b" });
                }
            }
            """;

        await AnalyzerTest(source).RunAsync();
    }

    [Fact]
    public async Task An_explicit_array_after_a_message_is_deliberate_and_stays_silent()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf("the message", new[] { "a", "b" });
                }
            }
            """;

        await AnalyzerTest(source).RunAsync();
    }

    [Fact]
    public async Task A_non_string_contract_cannot_hit_the_trap_and_stays_silent()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(int quantity)
                {
                    quantity.Must().BeAnyOf(1, 2, 3);
                }
            }
            """;

        await AnalyzerTest(source).RunAsync();
    }
}
