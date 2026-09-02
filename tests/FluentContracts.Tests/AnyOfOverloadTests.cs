using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// The 4.0.0 shape of <c>BeAnyOf</c>/<c>NotBeAnyOf</c> on a string argument, where the old
/// message-first overload silently took the first value as the message. Now a value list is always
/// bracketed and a message can only follow one, so every call below checks exactly what it says.
/// The calls that used to be the trap no longer compile; that is pinned by
/// <c>OverloadShapeTests</c> in the analyzer test project, which compiles snippets against the
/// real library.
/// </summary>
[ContractTest("AnyOfOverloads")]
public class AnyOfOverloadTests
{
    [Fact]
    public void A_single_value_is_a_value()
    {
        const string myArgument = "a";

        FluentActions.Invoking(() => myArgument.Must().BeAnyOf("a")).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotBeAnyOf("a")).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void A_bracketed_set_checks_every_value()
    {
        const string myArgument = "b";

        FluentActions.Invoking(() => myArgument.Must().BeAnyOf(["a", "b"])).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().BeAnyOf(["a", "c"])).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => myArgument.Must().NotBeAnyOf(["a", "b"])).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void A_message_after_the_set_is_the_message()
    {
        const string myArgument = "c";

        FluentActions
            .Invoking(() => myArgument.Must().BeAnyOf(["a", "b"], "Not a known state"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Not a known state*")
            .WithParameterName(nameof(myArgument));
    }
}
