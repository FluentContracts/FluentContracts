using System;
using FluentAssertions;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("DateTimeOffset")]
public class DateTimeOffsetContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<DateTimeOffset?, DateTimeOffsetContract, ArgumentException>(
            null,
            DummyData.GetDateTimeOffset(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<DateTimeOffset?, DateTimeOffsetContract, ArgumentNullException>(
            DummyData.GetDateTimeOffset(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetDateTimeOffsetPair();

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetDateTimeOffsetPair();

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.DifferentArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var pair = DummyData.GetDateTimeOffsetPair();

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().BeGreaterThan(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualTo()
    {
        var pair = DummyData.GetDateTimeOffsetPair();

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.TestArgument.AddTicks(-1),
            (testArgument, message) => testArgument.Must().BeGreaterOrEqualTo(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var pair = DummyData.GetDateTimeOffsetPair();

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().BeLessThan(pair.DifferentArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualTo()
    {
        var pair = DummyData.GetDateTimeOffsetPair();

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.TestArgument.AddTicks(1),
            (testArgument, message) => testArgument.Must().BeLessOrEqualTo(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween()
    {
        var moment = DummyData.GetDateTimeOffset();

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            moment,
            moment.AddDays(10),
            (testArgument, message) =>
                testArgument.Must().BeBetween(moment.AddDays(-1), moment.AddDays(1), message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeUtc()
    {
        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentException>(
            DummyData.GetDateTimeOffset(),
            DummyData.GetDateTimeOffset(TimeSpan.FromHours(3)),
            (testArgument, message) => testArgument.Must().BeUtc(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeUtc()
    {
        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentException>(
            DummyData.GetDateTimeOffset(TimeSpan.FromHours(3)),
            DummyData.GetDateTimeOffset(),
            (testArgument, message) => testArgument.Must().NotBeUtc(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveOffset()
    {
        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentException>(
            DummyData.GetDateTimeOffset(TimeSpan.FromHours(2)),
            DummyData.GetDateTimeOffset(TimeSpan.FromHours(-5)),
            (testArgument, message) => testArgument.Must().HaveOffset(TimeSpan.FromHours(2), message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHaveOffset()
    {
        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentException>(
            DummyData.GetDateTimeOffset(TimeSpan.FromHours(-5)),
            DummyData.GetDateTimeOffset(TimeSpan.FromHours(2)),
            (testArgument, message) => testArgument.Must().NotHaveOffset(TimeSpan.FromHours(2), message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInThePast()
    {
        var now = DateTime.SpecifyKind(new DateTime(2026, 6, 1, 12, 0, 0), DateTimeKind.Local);
        var reference = new DateTimeOffset(now);

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            reference.AddDays(-7),
            reference.AddDays(7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(now)).BeInThePast(message),
            "testArgument");

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            reference.AddDays(-7),
            reference.AddDays(7),
            (testArgument, message) => testArgument.Must().BeInThePast(reference, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInThePast()
    {
        var now = DateTime.SpecifyKind(new DateTime(2026, 6, 1, 12, 0, 0), DateTimeKind.Local);
        var reference = new DateTimeOffset(now);

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            reference.AddDays(7),
            reference.AddDays(-7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(now)).NotBeInThePast(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInTheFuture()
    {
        var now = DateTime.SpecifyKind(new DateTime(2026, 6, 1, 12, 0, 0), DateTimeKind.Local);
        var reference = new DateTimeOffset(now);

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            reference.AddDays(7),
            reference.AddDays(-7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(now)).BeInTheFuture(message),
            "testArgument");

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            reference.AddDays(7),
            reference.AddDays(-7),
            (testArgument, message) => testArgument.Must().BeInTheFuture(reference, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInTheFuture()
    {
        var now = DateTime.SpecifyKind(new DateTime(2026, 6, 1, 12, 0, 0), DateTimeKind.Local);
        var reference = new DateTimeOffset(now);

        TestContract<DateTimeOffset, DateTimeOffsetContract, ArgumentOutOfRangeException>(
            reference.AddDays(-7),
            reference.AddDays(7),
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(now)).NotBeInTheFuture(message),
            "testArgument");
    }

    /// <summary>
    /// Two moments describing the same instant in different zones are equal, so an offset check has to
    /// look at the offset itself rather than at the moment.
    /// </summary>
    [Fact]
    public void Test_Same_Instant_In_Different_Offsets_Compares_Equal_But_Has_A_Different_Offset()
    {
        var utc = new DateTimeOffset(2026, 6, 1, 12, 0, 0, TimeSpan.Zero);
        var sameInstantInBerlin = utc.ToOffset(TimeSpan.FromHours(2));

        FluentActions.Invoking(() => sameInstantInBerlin.Must().Be(utc))
            .Should()
            .NotThrow("both describe the same instant");

        FluentActions.Invoking(() => sameInstantInBerlin.Must().BeUtc())
            .Should()
            .Throw<ArgumentException>("the offset is +02:00, even though the instant matches");
    }
}
