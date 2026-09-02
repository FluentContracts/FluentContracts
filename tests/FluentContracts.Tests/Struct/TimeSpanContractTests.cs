using System;
using FluentContracts.Contracts.Struct;
using FluentContracts.Infrastructure;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("TimeSpan")]
public class TimeSpanContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<TimeSpan?, TimeSpanContract, ArgumentException>(
            null,
            DummyData.GetTimeSpan(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<TimeSpan?, TimeSpanContract, ArgumentNullException>(
            DummyData.GetTimeSpan(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetTimeSpanPair();

        TestContract<TimeSpan, TimeSpanContract, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableTimeSpanPair();

        TestContract<TimeSpan?, TimeSpanContract, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetTimeSpanPair();

        TestContract<TimeSpan, TimeSpanContract, ArgumentException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableTimeSpanPair();

        TestContract<TimeSpan?, TimeSpanContract, ArgumentException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeShorterThan()
    {
        var timeSpan = DummyData.GetTimeSpan(TimeSpan.FromTicks(100_000), TimeSpan.FromTicks(1_000));

        var successful = TimeSpan.FromTicks(timeSpan.Ticks - 100);
        var failing = TimeSpan.FromTicks(timeSpan.Ticks + 100);

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeShorterThan(timeSpan, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeShorterThan()
    {
        var timeSpan = DummyData.GetTimeSpan(TimeSpan.FromTicks(100_000), TimeSpan.FromTicks(1_000));

        var successful = TimeSpan.FromTicks(timeSpan.Ticks + 100);
        var failing = TimeSpan.FromTicks(timeSpan.Ticks - 100);

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeShorterThan(timeSpan, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLongerThan()
    {
        var timeSpan = DummyData.GetTimeSpan(TimeSpan.FromTicks(100_000), TimeSpan.FromTicks(1_000));

        var successful = TimeSpan.FromTicks(timeSpan.Ticks + 100);
        var failing = TimeSpan.FromTicks(timeSpan.Ticks - 100);

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeLongerThan(timeSpan, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLongerThan()
    {
        var timeSpan = DummyData.GetTimeSpan(TimeSpan.FromTicks(100_000), TimeSpan.FromTicks(1_000));

        var successful = TimeSpan.FromTicks(timeSpan.Ticks - 100);
        var failing = TimeSpan.FromTicks(timeSpan.Ticks + 100);

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeLongerThan(timeSpan, message),
            "testArgument");
    }

    /// <summary>
    /// Pins the boundary of the four length comparisons: a span exactly equal to the expected value
    /// is neither shorter nor longer, so the strict checks fail and their negations pass.
    /// <c>NotBeShorterThan</c> and <c>NotBeLongerThan</c> used to throw on equality.
    /// </summary>
    [Fact]
    public void Test_Length_Comparisons_On_Equal_Spans()
    {
        var timeSpan = DummyData.GetTimeSpan();
        var equal = TimeSpan.FromTicks(timeSpan.Ticks);

        equal.Must().NotBeShorterThan(timeSpan);
        equal.Must().NotBeLongerThan(timeSpan);

        Assert.Throws<ArgumentOutOfRangeException>(() => equal.Must().BeShorterThan(timeSpan));
        Assert.Throws<ArgumentOutOfRangeException>(() => equal.Must().BeLongerThan(timeSpan));
    }

    [Fact]
    public void Test_Must_BeEqualTo()
    {
        var timeSpan = DummyData.GetTimeSpan();
        var different = timeSpan.Ticks + 1_000L;
        
        var successful = TimeSpan.FromTicks(timeSpan.Ticks);
        var failing = TimeSpan.FromTicks(different);

        TestContract<TimeSpan, TimeSpanContract, ArgumentException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeEqualTo(timeSpan, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEqualTo()
    {
        var timeSpan = DummyData.GetTimeSpan();
        var different = timeSpan.Ticks + 1_000L;
        
        var successful = TimeSpan.FromTicks(different);
        var failing = TimeSpan.FromTicks(timeSpan.Ticks);

        TestContract<TimeSpan, TimeSpanContract, ArgumentException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeEqualTo(timeSpan, message),
            "testArgument");
    }
}