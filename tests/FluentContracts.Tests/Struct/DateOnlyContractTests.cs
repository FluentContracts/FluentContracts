using System;
using FluentAssertions;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("DateOnly")]
public class DateOnlyContractTests : Tests
{
    private static readonly DateOnly Anchor = new(2026, 6, 1);

    private static MockDateTimeProvider ClockAt(DateOnly today) =>
        new(today.ToDateTime(new TimeOnly(12, 0)));

    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<DateOnly?, DateOnlyContract, ArgumentException>(
            null,
            Anchor,
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<DateOnly?, DateOnlyContract, ArgumentNullException>(
            Anchor,
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            Anchor,
            Anchor.AddDays(1),
            (testArgument, message) => testArgument.Must().Be(Anchor, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            Anchor.AddDays(1),
            Anchor,
            (testArgument, message) => testArgument.Must().NotBe(Anchor, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(1),
            Anchor.AddDays(-1),
            (testArgument, message) => testArgument.Must().BeGreaterThan(Anchor, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(-1),
            Anchor.AddDays(1),
            (testArgument, message) => testArgument.Must().BeLessThan(Anchor, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor,
            Anchor.AddDays(10),
            (testArgument, message) =>
                testArgument.Must().BeBetween(Anchor.AddDays(-5), Anchor.AddDays(5), message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInThePast()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(-7),
            Anchor.AddDays(7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: ClockAt(Anchor)).BeInThePast(message),
            "testArgument");

        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(-7),
            Anchor.AddDays(7),
            (testArgument, message) => testArgument.Must().BeInThePast(Anchor, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInThePast()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(7),
            Anchor.AddDays(-7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: ClockAt(Anchor)).NotBeInThePast(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInTheFuture()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(7),
            Anchor.AddDays(-7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: ClockAt(Anchor)).BeInTheFuture(message),
            "testArgument");

        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(7),
            Anchor.AddDays(-7),
            (testArgument, message) => testArgument.Must().BeInTheFuture(Anchor, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInTheFuture()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor.AddDays(-7),
            Anchor.AddDays(7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: ClockAt(Anchor)).NotBeInTheFuture(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeToday()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            Anchor,
            Anchor.AddDays(1),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: ClockAt(Anchor)).BeToday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeToday()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            Anchor.AddDays(1),
            Anchor,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: ClockAt(Anchor)).NotBeToday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWeekday()
    {
        // 2026-06-01 is a Monday, 2026-06-06 a Saturday.
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            new DateOnly(2026, 6, 1),
            new DateOnly(2026, 6, 6),
            (testArgument, message) => testArgument.Must().BeWeekday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWeekend()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            new DateOnly(2026, 6, 6),
            new DateOnly(2026, 6, 1),
            (testArgument, message) => testArgument.Must().BeWeekend(message),
            "testArgument");
    }

    /// <summary>
    /// The negations are the other check under a different name — a week has only weekdays and weekend
    /// days — so they are aliases rather than separate rules, and these pin that they stay pointed at
    /// the right one.
    /// </summary>
    /// <summary>
    /// The weekend is Saturday <em>or</em> Sunday, and a check that only knew about one of them would
    /// pass every test written against the other.
    /// </summary>
    [Fact]
    public void Sunday_is_a_weekend_day_too()
    {
        // 2026-06-07 is a Sunday.
        var sunday = new DateOnly(2026, 6, 7);

        FluentActions.Invoking(() => sunday.Must().BeWeekend()).Should().NotThrow();
        FluentActions.Invoking(() => sunday.Must().BeWeekday()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_Must_NotBeWeekday()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            new DateOnly(2026, 6, 6),
            new DateOnly(2026, 6, 1),
            (testArgument, message) => testArgument.Must().NotBeWeekday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWeekend()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentException>(
            new DateOnly(2026, 6, 1),
            new DateOnly(2026, 6, 6),
            (testArgument, message) => testArgument.Must().NotBeWeekend(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Value()
    {
        DateOnly? deadline = Anchor;

        DateOnly value = deadline.Must().BeInTheFuture(Anchor.AddDays(-1)).Value();

        Assert.Equal(Anchor, value);
    }
}
