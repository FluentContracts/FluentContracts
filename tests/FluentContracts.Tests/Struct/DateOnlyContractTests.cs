using System;
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
        TestContract<DateOnly?, DateOnlyContract, ArgumentOutOfRangeException>(
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
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor,
            Anchor.AddDays(1),
            (testArgument, message) => testArgument.Must().Be(Anchor, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
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
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            Anchor,
            Anchor.AddDays(1),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: ClockAt(Anchor)).BeToday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeToday()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
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
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            new DateOnly(2026, 6, 1),
            new DateOnly(2026, 6, 6),
            (testArgument, message) => testArgument.Must().BeWeekday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWeekend()
    {
        TestContract<DateOnly, DateOnlyContract, ArgumentOutOfRangeException>(
            new DateOnly(2026, 6, 6),
            new DateOnly(2026, 6, 1),
            (testArgument, message) => testArgument.Must().BeWeekend(message),
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
