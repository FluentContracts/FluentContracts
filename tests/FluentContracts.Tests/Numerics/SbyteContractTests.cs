using System;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

[ContractTest("Sbyte")]
public class SbyteContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetSbyte(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<sbyte?, SbyteContract, ArgumentNullException>(
            DummyData.GetSbyte(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetSbytePair();

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableSbytePair();

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetSbytePair();

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableSbytePair();

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetSbytePair();
        var array = DummyData.GetArray(() => DummyData.GetSbyte(), pair.TestArgument, pair.DifferentArgument);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableSbytePair();
        var array = DummyData.GetArray(() => DummyData.GetSbyte(), pair.TestArgument, pair.DifferentArgument);

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetSbytePair();
        var array = DummyData.GetArray(() => DummyData.GetSbyte(), pair.TestArgument, pair.DifferentArgument);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
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
        var pair = DummyData.GetNullableSbytePair();
        var array = DummyData.GetArray(() => DummyData.GetNullableSbyte(), pair.TestArgument, pair.DifferentArgument);

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
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
        var success = DummyData.GetSbyte();
        var lower = (sbyte)(success - 10);
        var higher = (sbyte)(success + 10);
        var outOfRange = (sbyte)(higher + 10);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableSbyte();
        var lower = (sbyte?)(success - 10);
        var higher = (sbyte?)(success + 10);
        var outOfRange = (sbyte?)(higher + 10);

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetSbyte();
        var lower = (sbyte)(success - 10);
        var outOfRange = (sbyte)(lower - 10);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableSbyte();
        var lower = (sbyte?)(success - 10);
        var outOfRange = (sbyte?)(lower - 10);

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetSbyte();
        var outOfRange = (sbyte)(success - 10);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableSbyte();
        var outOfRange = (sbyte?)(success - 10);

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetSbyte();
        var higher = (sbyte)(success + 10);
        var outOfRange = (sbyte)(higher + 10);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableSbyte();
        var higher = (sbyte?)(success + 10);
        var outOfRange = (sbyte?)(higher + 10);

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetSbyte();
        var outOfRange = (sbyte)(success + 10);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableSbyte();
        var outOfRange = (sbyte?)(success + 10);

        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
        
        
        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
        
        
        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BePositive()
    {
        var success = DummyData.GetSbyte(NumberOption.Positive);
        var fail = DummyData.GetSbyte(NumberOption.Negative);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBePositive()
    {
        var success = DummyData.GetSbyte(NumberOption.Negative);
        var fail = DummyData.GetSbyte(NumberOption.Positive);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNegative()
    {
        var success = DummyData.GetSbyte(NumberOption.Negative);
        var fail = DummyData.GetSbyte(NumberOption.Positive);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeNegative(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNegative()
    {
        var success = DummyData.GetSbyte(NumberOption.Positive);
        var fail = DummyData.GetSbyte(NumberOption.Negative);

        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeNegative(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeOdd()
    {
        var successful = DummyData.GetSbyte(NumberOption.Odd);
        var failing = DummyData.GetSbyte(NumberOption.Even);
        
        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
        
        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeOdd()
    {
        var successful = DummyData.GetSbyte(NumberOption.Even);
        var failing = DummyData.GetSbyte(NumberOption.Odd);
        
        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
        
        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEven()
    {
        var successful = DummyData.GetSbyte(NumberOption.Even);
        var failing = DummyData.GetSbyte(NumberOption.Odd);
        
        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
        
        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeEven()
    {
        var successful = DummyData.GetSbyte(NumberOption.Odd);
        var failing = DummyData.GetSbyte(NumberOption.Even);
        
        TestContract<sbyte, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
        
        TestContract<sbyte?, SbyteContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
    }
}