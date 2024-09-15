using System;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

[ContractTest("Short")]
public class ShortContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetShort(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<short?, ShortContract, ArgumentNullException>(
            DummyData.GetShort(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetShortPair();

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableShortPair();

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetShortPair();

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableShortPair();

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetShortPair();
        var array = DummyData.GetArray(() => DummyData.GetShort(), pair.TestArgument, pair.DifferentArgument);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableShortPair();
        var array = DummyData.GetArray(() => DummyData.GetNullableShort(), pair.TestArgument, pair.DifferentArgument);

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetShortPair();
        var array = DummyData.GetArray(() => DummyData.GetShort(), pair.TestArgument, pair.DifferentArgument);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
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
        var pair = DummyData.GetNullableShortPair();
        var array = DummyData.GetArray(() => DummyData.GetNullableShort(), pair.TestArgument, pair.DifferentArgument);

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
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
        var success = DummyData.GetShort();
        var lower = (short)(success - 10);
        var higher = (short)(success + 10);
        var outOfRange = (short)(higher + 10);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableShort();
        var lower = (short?)(success - 10);
        var higher = (short?)(success + 10);
        var outOfRange = (short?)(higher + 10);

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetShort();
        var lower = (short)(success - 10);
        var outOfRange = (short)(lower - 10);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableShort();
        var lower = (short?)(success - 10);
        var outOfRange = (short?)(lower - 10);

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetShort();
        var outOfRange = (short)(success - 10);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableShort();
        var outOfRange = (short?)(success - 10);

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetShort();
        var higher = (short)(success + 10);
        var outOfRange = (short)(higher + 10);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableShort();
        var higher = (short?)(success + 10);
        var outOfRange = (short?)(higher + 10);

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetShort();
        var outOfRange = (short)(success + 10);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableShort();
        var outOfRange = (short?)(success + 10);

        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
        
        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
        
        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BePositive()
    {
        var success = DummyData.GetShort(NumberOption.Positive);
        var fail = DummyData.GetShort(NumberOption.Negative);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBePositive()
    {
        var success = DummyData.GetShort(NumberOption.Negative);
        var fail = DummyData.GetShort(NumberOption.Positive);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNegative()
    {
        var success = DummyData.GetShort(NumberOption.Negative);
        var fail = DummyData.GetShort(NumberOption.Positive);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeNegative(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNegative()
    {
        var success = DummyData.GetShort(NumberOption.Positive);
        var fail = DummyData.GetShort(NumberOption.Negative);

        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeNegative(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeOdd()
    {
        var successful = DummyData.GetShort(NumberOption.Odd);
        var failing = DummyData.GetShort(NumberOption.Even);
        
        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
        
        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeOdd()
    {
        var successful = DummyData.GetShort(NumberOption.Even);
        var failing = DummyData.GetShort(NumberOption.Odd);
        
        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
        
        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeOdd(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEven()
    {
        var successful = DummyData.GetShort(NumberOption.Even);
        var failing = DummyData.GetShort(NumberOption.Odd);
        
        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
        
        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeEven(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeEven()
    {
        var successful = DummyData.GetShort(NumberOption.Odd);
        var failing = DummyData.GetShort(NumberOption.Even);
        
        TestContract<short, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
        
        TestContract<short?, ShortContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeEven(message),
            "testArgument");
    }
}