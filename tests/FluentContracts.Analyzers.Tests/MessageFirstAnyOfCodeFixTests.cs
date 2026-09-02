using System.Threading.Tasks;
using FluentContracts.Analyzers;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Xunit;

namespace FluentContracts.Analyzers.Tests;

/// <summary>
/// The two fixes for FC0001, one per possible intent: every argument was meant as a value, or the
/// first really was the message. Both rewrite to the sequence overload, which cannot confuse the two.
/// </summary>
public class MessageFirstAnyOfCodeFixTests
{
    private static CSharpCodeFixTest<MessageFirstAnyOfAnalyzer, MessageFirstAnyOfCodeFixProvider, DefaultVerifier>
        CodeFixTest(string source, string fixedSource, int codeActionIndex)
    {
        var test = new CSharpCodeFixTest<MessageFirstAnyOfAnalyzer, MessageFirstAnyOfCodeFixProvider, DefaultVerifier>
        {
            TestCode = source,
            FixedCode = fixedSource,
            CodeActionIndex = codeActionIndex,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
        };

        test.TestState.AdditionalReferences.Add(typeof(FluentContracts.Contracts.BaseContract<,>).Assembly);
        test.FixedState.AdditionalReferences.Add(typeof(FluentContracts.Contracts.BaseContract<,>).Assembly);
        return test;
    }

    [Fact]
    public async Task Checks_every_argument_as_a_value()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    {|FC0001:tag.Must().BeAnyOf("a", "b", "c")|};
                }
            }
            """;

        const string fixedSource = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf(new[] { "a", "b", "c" });
                }
            }
            """;

        await CodeFixTest(source, fixedSource, codeActionIndex: 0).RunAsync();
    }

    [Fact]
    public async Task Keeps_the_first_argument_as_the_message()
    {
        const string source = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    {|FC0001:tag.Must().BeAnyOf("must be a known tag", "a", "b")|};
                }
            }
            """;

        const string fixedSource = """
            using FluentContracts;

            public class C
            {
                public void M(string tag)
                {
                    tag.Must().BeAnyOf(new[] { "a", "b" }, "must be a known tag");
                }
            }
            """;

        await CodeFixTest(source, fixedSource, codeActionIndex: 1).RunAsync();
    }
}
