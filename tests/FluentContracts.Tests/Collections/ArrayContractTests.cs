using System;
using FluentContracts.Contracts.Collections;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Collections;

[ContractTest("Array")]
public class ArrayContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<Array, ArrayContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetArray(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<Array, ArrayContract, ArgumentNullException>(
            DummyData.GetArray(DummyData.GetString),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetArrayPair(DummyData.GetString);

        TestContract<Array, ArrayContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetArrayPair(DummyData.GetString);

        TestContract<Array, ArrayContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<Array, ArrayContract, ArgumentOutOfRangeException>(
            Array.Empty<string>(),
            DummyData.GetArray(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<Array, ArrayContract, ArgumentOutOfRangeException>(
            DummyData.GetArray(DummyData.GetString),
            Array.Empty<string>(),
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeWithCount()
    {
        const int size = 10;
        var success = DummyData.GetArray(DummyData.GetString, size: size);
        var fail = DummyData.GetArray(DummyData.GetString, size: size + 10);
        
        TestContract<Array, ArrayContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeWithCount(size, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWithCount()
    {
        const int size = 10;
        var success = DummyData.GetArray(DummyData.GetString, size: size + 10);
        var fail = DummyData.GetArray(DummyData.GetString, size: size);
        
        TestContract<Array, ArrayContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeWithCount(size, message),
            "testArgument");
    }
}