using System;
using FluentAssertions;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("TimeOnly")]
public class TimeOnlyContractTests : Tests
{
    private static readonly TimeOnly Noon = new(12, 0);

    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<TimeOnly?, TimeOnlyContract, ArgumentException>(
            null,
            Noon,
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<TimeOnly?, TimeOnlyContract, ArgumentNullException>(
            Noon,
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        TestContract<TimeOnly, TimeOnlyContract, ArgumentException>(
            Noon,
            Noon.AddHours(1),
            (testArgument, message) => testArgument.Must().Be(Noon, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        TestContract<TimeOnly, TimeOnlyContract, ArgumentException>(
            Noon.AddHours(1),
            Noon,
            (testArgument, message) => testArgument.Must().NotBe(Noon, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        TestContract<TimeOnly, TimeOnlyContract, ArgumentOutOfRangeException>(
            Noon.AddHours(1),
            Noon.AddHours(-1),
            (testArgument, message) => testArgument.Must().BeGreaterThan(Noon, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        TestContract<TimeOnly, TimeOnlyContract, ArgumentOutOfRangeException>(
            Noon.AddHours(-1),
            Noon.AddHours(1),
            (testArgument, message) => testArgument.Must().BeLessThan(Noon, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween()
    {
        TestContract<TimeOnly, TimeOnlyContract, ArgumentOutOfRangeException>(
            Noon,
            new TimeOnly(18, 0),
            (testArgument, message) =>
                testArgument.Must().BeBetween(new TimeOnly(9, 0), new TimeOnly(17, 0), message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeBetween()
    {
        TestContract<TimeOnly, TimeOnlyContract, ArgumentOutOfRangeException>(
            new TimeOnly(18, 0),
            Noon,
            (testArgument, message) =>
                testArgument.Must().NotBeBetween(new TimeOnly(9, 0), new TimeOnly(17, 0), message),
            "testArgument");
    }

    /// <summary>
    /// Pins <see cref="TimeOnly.IsBetween"/>'s semantics as the contract's: the window wraps
    /// midnight, the start is inclusive and the end is exclusive.
    /// </summary>
    [Fact]
    public void BeBetween_wraps_midnight_and_is_start_inclusive_end_exclusive()
    {
        var start = new TimeOnly(22, 0);
        var end = new TimeOnly(2, 0);

        var lateEvening = new TimeOnly(23, 30);
        var exactStart = new TimeOnly(22, 0);
        var exactEnd = new TimeOnly(2, 0);
        var midday = new TimeOnly(12, 0);

        FluentActions.Invoking(() => lateEvening.Must().BeBetween(start, end)).Should().NotThrow();
        FluentActions.Invoking(() => exactStart.Must().BeBetween(start, end)).Should().NotThrow();

        FluentActions.Invoking(() => exactEnd.Must().BeBetween(start, end))
            .Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => midday.Must().BeBetween(start, end))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Test_Must_Value()
    {
        TimeOnly? openingTime = Noon;

        TimeOnly value = openingTime.Must().BeLessThan(new TimeOnly(18, 0)).Value();

        Assert.Equal(Noon, value);
    }
}
