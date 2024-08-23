using System;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

[ContractTest("Ulong")]
public class UlongContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetUlong(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<ulong?, UlongContract, ArgumentNullException>(
            DummyData.GetUlong(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetUlongPair();

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableUlongPair();

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetUlongPair();

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableUlongPair();

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetUlongPair();
        var array = DummyData.GetArray(() => DummyData.GetUlong(), pair.TestArgument, pair.DifferentArgument);

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableUlongPair();
        var array = DummyData.GetArray(DummyData.GetNullableUlong, pair.TestArgument, pair.DifferentArgument);

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetUlongPair();
        var array = DummyData.GetArray(() => DummyData.GetUlong(), pair.TestArgument, pair.DifferentArgument);

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableUlongPair();
        var array = DummyData.GetArray(DummyData.GetNullableUlong, pair.TestArgument, pair.DifferentArgument);

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween()
    {
        var success = DummyData.GetUlong();
        var lower = success - 10;
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableUlong();
        var lower = success - 10;
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetUlong();
        var lower = success - 10;
        var outOfRange = lower - 10;

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableUlong();
        var lower = success - 10;
        var outOfRange = lower - 10;

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetUlong();
        var outOfRange = success - 10;

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableUlong();
        var outOfRange = success - 10;

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetUlong();
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableUlong();
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetUlong();
        var outOfRange = success + 10;

        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableUlong();
        var outOfRange = success + 10;

        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
        
        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeOdd()
    {
        var successful = DummyData.GetUlong(NumberOption.Odd);
        var failing = DummyData.GetUlong(NumberOption.Even);
        
        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
        
        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeOdd()
    {
        var successful = DummyData.GetUlong(NumberOption.Even);
        var failing = DummyData.GetUlong(NumberOption.Odd);
        
        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
        
        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEven()
    {
        var successful = DummyData.GetUlong(NumberOption.Even);
        var failing = DummyData.GetUlong(NumberOption.Odd);
        
        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
        
        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeEven()
    {
        var successful = DummyData.GetUlong(NumberOption.Odd);
        var failing = DummyData.GetUlong(NumberOption.Even);
        
        TestContract<ulong, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
        
        TestContract<ulong?, UlongContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
    }
}