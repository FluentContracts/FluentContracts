using System;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

[ContractTest("Uint")]
public class UintContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetUint(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<uint?, UintContract, ArgumentNullException>(
            DummyData.GetUint(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetUintPair();

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableUintPair();

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetUintPair();

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableUintPair();

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetUintPair();
        var array = DummyData.GetArray(() => DummyData.GetUint(), pair.TestArgument, pair.DifferentArgument);

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableUintPair();
        var array = DummyData.GetArray(DummyData.GetNullableUint, pair.TestArgument, pair.DifferentArgument);

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetUintPair();
        var array = DummyData.GetArray(() => DummyData.GetUint(), pair.TestArgument, pair.DifferentArgument);

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
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
        var pair = DummyData.GetNullableUintPair();
        var array = DummyData.GetArray(DummyData.GetNullableUint, pair.TestArgument, pair.DifferentArgument);

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
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
        var success = DummyData.GetUint();
        var lower = success - 10;
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableUint();
        var lower = success - 10;
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetUint();
        var lower = success - 10;
        var outOfRange = lower - 10;

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableUint();
        var lower = success - 10;
        var outOfRange = lower - 10;

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetUint();
        var outOfRange = success - 10;

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableUint();
        var outOfRange = success - 10;

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetUint();
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableUint();
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetUint();
        var outOfRange = success + 10;

        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableUint();
        var outOfRange = success + 10;

        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
        
        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
        
        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeOdd()
    {
        var successful = DummyData.GetUint(NumberOption.Odd);
        var failing = DummyData.GetUint(NumberOption.Even);
        
        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
        
        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeOdd()
    {
        var successful = DummyData.GetUint(NumberOption.Even);
        var failing = DummyData.GetUint(NumberOption.Odd);
        
        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
        
        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEven()
    {
        var successful = DummyData.GetUint(NumberOption.Even);
        var failing = DummyData.GetUint(NumberOption.Odd);
        
        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
        
        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeEven()
    {
        var successful = DummyData.GetUint(NumberOption.Odd);
        var failing = DummyData.GetUint(NumberOption.Even);
        
        TestContract<uint, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
        
        TestContract<uint?, UintContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
    }
}