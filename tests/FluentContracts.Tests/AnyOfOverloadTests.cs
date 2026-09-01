using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// <c>BeAnyOf</c> came as a pair: <c>(params T[])</c> and <c>(string? message, params T[])</c>. When the
/// argument is a string both are applicable to the same call, and the compiler prefers the one with more
/// declared parameters — so the first value was taken as the message. These tests pin down what each
/// overload now binds to.
/// </summary>
[ContractTest("AnyOfOverloads")]
public class AnyOfOverloadTests
{
    [Fact]
    public void A_single_string_is_a_value_and_not_a_message()
    {
        // Previously the message overload won, leaving an empty set: this threw even though it matches.
        FluentActions.Invoking(() => "a".Must().BeAnyOf("a")).Should().NotThrow();

        FluentActions
            .Invoking(() => "a".Must().BeAnyOf("b"))
            .Should()
            .Throw<ArgumentOutOfRangeException>("\"a\" is not \"b\"");
    }

    [Fact]
    public void NotBeAnyOf_with_a_single_string_is_enforced_again()
    {
        // Previously this passed silently: the set was empty, so nothing was ever excluded.
        FluentActions
            .Invoking(() => "a".Must().NotBeAnyOf("a"))
            .Should()
            .Throw<ArgumentOutOfRangeException>("\"a\" is in the set");

        FluentActions.Invoking(() => "a".Must().NotBeAnyOf("b")).Should().NotThrow();
    }

    [Fact]
    public void A_sequence_takes_the_message_second()
    {
        IEnumerable<string?> allowed = ["a", "b"];

        FluentActions.Invoking(() => "a".Must().BeAnyOf(allowed)).Should().NotThrow();

        FluentActions
            .Invoking(() => "z".Must().BeAnyOf(allowed, "must be a or b"))
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("must be a or b*");

        FluentActions
            .Invoking(() => "a".Must().NotBeAnyOf(allowed, "must not be a or b"))
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("must not be a or b*");
    }

    [Fact]
    public void An_array_of_values_still_binds_to_the_values_overload()
    {
        var allowed = new[] { "a", "b" };

        FluentActions.Invoking(() => "a".Must().BeAnyOf(allowed)).Should().NotThrow();
        FluentActions.Invoking(() => "z".Must().BeAnyOf(allowed)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void A_non_string_argument_was_never_affected()
    {
        FluentActions.Invoking(() => 1.Must().BeAnyOf(1, 2)).Should().NotThrow();
        FluentActions.Invoking(() => 9.Must().BeAnyOf(1, 2)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => 1.Must().NotBeAnyOf(1, 2)).Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    /// The one case that cannot be fixed without a breaking change: several string values still bind to
    /// the deprecated message-first overload, which takes the first as the message. Pinned here so the
    /// behaviour is recorded rather than assumed, and so removing that overload in 4.0.0 fails this test
    /// and forces it to be rewritten.
    /// </summary>
    [Fact]
    public void Several_string_values_still_bind_to_the_deprecated_overload()
    {
#pragma warning disable CS0618 // the point of the test is which overload this call reaches
        FluentActions
            .Invoking(() => "a".Must().BeAnyOf("a", "b"))
            .Should()
            .Throw<ArgumentOutOfRangeException>("\"a\" is taken as the message, so only \"b\" is the set");

        FluentActions
            .Invoking(() => "a".Must().NotBeAnyOf("a", "b"))
            .Should()
            .NotThrow("\"a\" is taken as the message, so it is never excluded");
#pragma warning restore CS0618
    }

    [Fact]
    public void The_deprecated_overload_still_works_when_called_deliberately()
    {
#pragma warning disable CS0618 // deliberately exercising the deprecated overload
        FluentActions
            .Invoking(() => "z".Must().BeAnyOf("boom", "a", "b"))
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("boom*");
#pragma warning restore CS0618
    }
}
