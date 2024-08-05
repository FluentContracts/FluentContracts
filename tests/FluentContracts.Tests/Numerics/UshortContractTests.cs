using System;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

[ContractTest("Ushort")]
public class UshortContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetUshort(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<ushort?, UshortContract, ArgumentNullException>(
            DummyData.GetUshort(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetUshortPair();

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableUshortPair();

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetUshortPair();

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableUshortPair();

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetUshortPair();
        var array = DummyData.GetArray(DummyData.GetUshort, pair.TestArgument, pair.DifferentArgument);

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableUshortPair();
        var array = DummyData.GetArray(DummyData.GetNullableUshort, pair.TestArgument, pair.DifferentArgument);

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetUshortPair();
        var array = DummyData.GetArray(DummyData.GetUshort, pair.TestArgument, pair.DifferentArgument);

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
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
        var pair = DummyData.GetNullableUshortPair();
        var array = DummyData.GetArray(DummyData.GetNullableUshort, pair.TestArgument, pair.DifferentArgument);

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
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
        var success = DummyData.GetUshort();
        var lower = (ushort)(success - 10);
        var higher = (ushort)(success + 10);
        var outOfRange = (ushort)(higher + 10);

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableUshort();
        var lower = (ushort?)(success - 10);
        var higher = (ushort?)(success + 10);
        var outOfRange = (ushort?)(higher + 10);

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetUshort();
        var lower = (ushort)(success - 10);
        var outOfRange = (ushort)(lower - 10);

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableUshort();
        var lower = (ushort?)(success - 10);
        var outOfRange = (ushort?)(lower - 10);

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetUshort();
        var outOfRange = (ushort)(success - 10);

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableUshort();
        var outOfRange = (ushort?)(success - 10);

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetUshort();
        var higher = (ushort)(success + 10);
        var outOfRange = (ushort)(higher + 10);

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableUshort();
        var higher = (ushort?)(success + 10);
        var outOfRange = (ushort?)(higher + 10);

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetUshort();
        var outOfRange = (ushort)(success + 10);

        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableUshort();
        var outOfRange = (ushort?)(success + 10);

        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
        
        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            0,
            42,
            (testArgument, message) =>
                testArgument.Must().BeZero(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<ushort, UshortContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
        
        TestContract<ushort?, UshortContract, ArgumentOutOfRangeException>(
            69,
            0,
            (testArgument, message) =>
                testArgument.Must().NotBeZero(message),
            "testArgument");
    }
}