using System;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

[ContractTest("Float")]
public class FloatContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetFloat(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<float?, FloatContract, ArgumentNullException>(
            DummyData.GetFloat(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetFloatPair();

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableFloatPair();

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetFloatPair();

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableFloatPair();

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetFloatPair();
        var array = DummyData.GetArray(() => DummyData.GetFloat(), pair.TestArgument, pair.DifferentArgument);

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableFloatPair();
        var array = DummyData.GetArray(() => DummyData.GetNullableFloat(), pair.TestArgument, pair.DifferentArgument);

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetFloatPair();
        var array = DummyData.GetArray(() => DummyData.GetFloat(), pair.TestArgument, pair.DifferentArgument);

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
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
        var pair = DummyData.GetNullableFloatPair();
        var array = DummyData.GetArray(() => DummyData.GetNullableFloat(), pair.TestArgument, pair.DifferentArgument);

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
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
        var success = DummyData.GetFloat();
        var lower = success - 10;
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableFloat();
        var lower = success - 10;
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetFloat();
        var lower = success - 10;
        var outOfRange = lower - 10;

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableFloat();
        var lower = success - 10;
        var outOfRange = lower - 10;

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetFloat();
        var outOfRange = success - 10;

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableFloat();
        var outOfRange = success - 10;

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetFloat();
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableFloat();
        var higher = success + 10;
        var outOfRange = higher + 10;

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetFloat();
        var outOfRange = success + 10;

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableFloat();
        var outOfRange = success + 10;

        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            0F,
            0.42F,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
        
        
        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            0F,
            0.42F,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            0.69F,
            0F,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
        
        
        TestContract<float?, FloatContract, ArgumentOutOfRangeException>(
            0.69F,
            0F,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BePositive()
    {
        var success = DummyData.GetFloat(NumberOption.Positive);
        var fail = DummyData.GetFloat(NumberOption.Negative);

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBePositive()
    {
        var success = DummyData.GetFloat(NumberOption.Negative);
        var fail = DummyData.GetFloat(NumberOption.Positive);

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNegative()
    {
        var success = DummyData.GetFloat(NumberOption.Negative);
        var fail = DummyData.GetFloat(NumberOption.Positive);

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeNegative(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNegative()
    {
        var success = DummyData.GetFloat(NumberOption.Positive);
        var fail = DummyData.GetFloat(NumberOption.Negative);

        TestContract<float, FloatContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeNegative(message),
            "testArgument");
    }
}