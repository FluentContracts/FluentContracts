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
        TestContract<TimeSpan?, TimeSpanContract, ArgumentOutOfRangeException>(
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

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableTimeSpanPair();

        TestContract<TimeSpan?, TimeSpanContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetTimeSpanPair();

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableTimeSpanPair();

        TestContract<TimeSpan?, TimeSpanContract, ArgumentOutOfRangeException>(
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

    [Fact]
    public void Test_Must_BeEqualTo()
    {
        var timeSpan = DummyData.GetTimeSpan();
        var different = timeSpan.Ticks + 1_000L;
        
        var successful = TimeSpan.FromTicks(timeSpan.Ticks);
        var failing = TimeSpan.FromTicks(different);

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
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

        TestContract<TimeSpan, TimeSpanContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeEqualTo(timeSpan, message),
            "testArgument");
    }
}