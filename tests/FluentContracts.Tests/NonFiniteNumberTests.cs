using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// Ordering comparisons use <see cref="System.Collections.Generic.Comparer{T}"/>, which is a total
/// order and sorts NaN below every other value. So a check asking whether the argument was less than
/// something was satisfied by NaN, and the contract went silently unenforced — the same shape as the
/// null policy this sits beside. IEEE says every ordering comparison with NaN is false, so no ordering
/// check can be satisfied by one.
/// </summary>
[ContractTest("NonFiniteNumbers")]
public class NonFiniteNumberTests
{
    [Fact]
    public void NaN_no_longer_satisfies_an_ordering_check()
    {
        var myArgument = double.NaN;

        // Both of these used to pass, leaving the contract unenforced.
        FluentActions
            .Invoking(() => myArgument.Must().BeNegative())
            .Should()
            .Throw<ArgumentOutOfRangeException>("NaN is not negative")
            .WithParameterName(nameof(myArgument));

        FluentActions
            .Invoking(() => myArgument.Must().BeLessThan(0))
            .Should()
            .Throw<ArgumentOutOfRangeException>("no ordering comparison with NaN is true");
    }

    [Fact]
    public void Every_ordering_check_rejects_NaN()
    {
        var myArgument = double.NaN;

        FluentActions.Invoking(() => myArgument.Must().BePositive()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeNegative()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().NotBePositive()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().NotBeNegative()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeGreaterThan(0)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeLessThan(0)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeGreaterOrEqualTo(0)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeLessOrEqualTo(0)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeBetween(-1, 1)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Float_NaN_is_rejected_the_same_way()
    {
        var myArgument = float.NaN;

        FluentActions.Invoking(() => myArgument.Must().BeNegative()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeLessThan(0f)).Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    /// Infinity orders correctly and is deliberately left alone: it really is greater than everything.
    /// </summary>
    [Fact]
    public void Infinity_still_orders()
    {
        FluentActions.Invoking(() => double.PositiveInfinity.Must().BePositive()).Should().NotThrow();
        FluentActions.Invoking(() => double.NegativeInfinity.Must().BeNegative()).Should().NotThrow();
        FluentActions.Invoking(() => double.PositiveInfinity.Must().BeGreaterThan(double.MaxValue)).Should().NotThrow();
    }

    [Fact]
    public void An_ordinary_number_is_unaffected()
    {
        FluentActions.Invoking(() => (-2.5).Must().BeNegative()).Should().NotThrow();
        FluentActions.Invoking(() => 2.5.Must().BePositive()).Should().NotThrow();
        FluentActions.Invoking(() => 2.5.Must().BeBetween(1, 3)).Should().NotThrow();
    }

    [Fact]
    public void Test_Must_BeNaN()
    {
        FluentActions.Invoking(() => double.NaN.Must().BeNaN()).Should().NotThrow();
        FluentActions.Invoking(() => 1.0.Must().BeNaN()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => 1.0.Must().NotBeNaN()).Should().NotThrow();
        FluentActions.Invoking(() => double.NaN.Must().NotBeNaN()).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Test_Must_BeInfinity()
    {
        FluentActions.Invoking(() => double.PositiveInfinity.Must().BeInfinity()).Should().NotThrow();
        FluentActions.Invoking(() => double.NegativeInfinity.Must().BeInfinity()).Should().NotThrow();
        FluentActions.Invoking(() => 1.0.Must().BeInfinity()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => 1.0.Must().NotBeInfinity()).Should().NotThrow();
        FluentActions.Invoking(() => double.NaN.Must().NotBeInfinity()).Should().NotThrow("NaN is not infinite");
    }

    [Fact]
    public void Test_Must_BeFinite()
    {
        FluentActions.Invoking(() => 1.0.Must().BeFinite()).Should().NotThrow();
        FluentActions.Invoking(() => double.NaN.Must().BeFinite()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => double.PositiveInfinity.Must().BeFinite()).Should().Throw<ArgumentOutOfRangeException>();

        FluentActions.Invoking(() => double.NaN.Must().NotBeFinite()).Should().NotThrow();
        FluentActions.Invoking(() => double.PositiveInfinity.Must().NotBeFinite()).Should().NotThrow();
        FluentActions.Invoking(() => 1.0.Must().NotBeFinite()).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void The_float_checks_are_there_too()
    {
        FluentActions.Invoking(() => float.NaN.Must().BeNaN()).Should().NotThrow();
        FluentActions.Invoking(() => float.PositiveInfinity.Must().BeInfinity()).Should().NotThrow();
        FluentActions.Invoking(() => 1f.Must().BeFinite()).Should().NotThrow();
    }

    /// <summary>
    /// The float contract carries its own copy of the six checks rather than inheriting them, so each
    /// negation is its own method that can be wired to the wrong validator. The double ones are pinned
    /// above; these are the float twins.
    /// </summary>
    [Fact]
    public void The_float_negations_are_there_too()
    {
        FluentActions.Invoking(() => 1f.Must().NotBeNaN()).Should().NotThrow();
        FluentActions.Invoking(() => float.NaN.Must().NotBeNaN()).Should().Throw<ArgumentOutOfRangeException>();

        FluentActions.Invoking(() => 1f.Must().NotBeInfinity()).Should().NotThrow();
        FluentActions.Invoking(() => float.NaN.Must().NotBeInfinity()).Should().NotThrow("NaN is not infinite");
        FluentActions
            .Invoking(() => float.NegativeInfinity.Must().NotBeInfinity())
            .Should()
            .Throw<ArgumentOutOfRangeException>();

        FluentActions.Invoking(() => float.NaN.Must().NotBeFinite()).Should().NotThrow();
        FluentActions.Invoking(() => float.PositiveInfinity.Must().NotBeFinite()).Should().NotThrow();
        FluentActions.Invoking(() => 1f.Must().NotBeFinite()).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void NotBeInfinity_rejects_an_infinite_argument()
    {
        var myArgument = double.PositiveInfinity;

        FluentActions
            .Invoking(() => myArgument.Must().NotBeInfinity())
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(myArgument));

        FluentActions
            .Invoking(() => double.NegativeInfinity.Must().NotBeInfinity())
            .Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    /// The NaN and infinity tests read the boxed underlying type out of the contract's nullable, so a
    /// null argument matches neither and is simply not infinite. These checks say nothing about null —
    /// <c>NotBeNull</c> is how a caller asks about that — so they must not invent a failure for one.
    /// </summary>
    [Fact]
    public void A_null_argument_is_neither_NaN_nor_infinite()
    {
        double? myArgument = null;

        FluentActions.Invoking(() => myArgument.Must().NotBeNaN()).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotBeInfinity()).Should().NotThrow();
    }
}
