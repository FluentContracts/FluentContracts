using System;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

[ContractTest("Decimal")]
public class DecimalContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<decimal?, NullableDecimalContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetDecimal(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<decimal?, NullableDecimalContract, ArgumentNullException>(
            DummyData.GetDecimal(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetDecimalPair();

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetDecimalPair();

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetDecimalPair();
        var array = DummyData.GetArray(() => DummyData.GetDecimal(), pair.TestArgument, pair.DifferentArgument);

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetDecimalPair();
        var array = DummyData.GetArray(() => DummyData.GetDecimal(), pair.TestArgument, pair.DifferentArgument);

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
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
        var success = DummyData.GetDecimal();
        var lower = success - 10;
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetDecimal();
        var lower = success - 10;
        var outOfRange = lower - 10;

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetDecimal();
        var outOfRange = success - 10;

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetDecimal();
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetDecimal();
        var outOfRange = success + 10;

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            0M,
            0.42M,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
        
        TestContract<decimal?, NullableDecimalContract, ArgumentOutOfRangeException>(
            0M,
            0.42M,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            0.69M,
            0M,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
        
        TestContract<decimal?, NullableDecimalContract, ArgumentOutOfRangeException>(
            0.69M,
            0M,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BePositive()
    {
        var success = DummyData.GetDecimal(NumberOption.Positive);
        var fail = DummyData.GetDecimal(NumberOption.Negative);

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBePositive()
    {
        var success = DummyData.GetDecimal(NumberOption.Negative);
        var fail = DummyData.GetDecimal(NumberOption.Positive);

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNegative()
    {
        var success = DummyData.GetDecimal(NumberOption.Negative);
        var fail = DummyData.GetDecimal(NumberOption.Positive);

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeNegative(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNegative()
    {
        var success = DummyData.GetDecimal(NumberOption.Positive);
        var fail = DummyData.GetDecimal(NumberOption.Negative);

        TestContract<decimal, DecimalContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeNegative(message),
            "testArgument");
    }
}