using System;
using FluentContracts.Contracts.Collections;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Collections;

[ContractTest("Array")]
public class ArrayContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            null,
            DummyData.GetArray(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<string[], ListContract<string>, ArgumentNullException>(
            DummyData.GetArray(DummyData.GetString),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetArrayPair(DummyData.GetString);

        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetArrayPair(DummyData.GetString);

        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            Array.Empty<string>(),
            DummyData.GetArray(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
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
        
        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
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
        
        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeWithCount(size, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_Contain()
    {
        var success = DummyData.GetArray(DummyData.GetString, size: 20);
        var fail = DummyData.GetArray(DummyData.GetString, size: 20);
        var list = success[10..15];

        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().Contain(list, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_Contain_Single()
    {
        var included = DummyData.GetString();
        
        var success = DummyData.GetArray(DummyData.GetString, included);
        var fail = DummyData.GetArray(DummyData.GetString, excludedValue: included);
        
        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, _) => testArgument.Must().Contain(included),
            "testArgument", 
            true);
    }
    
    [Fact]
    public void Test_Must_NotContain()
    {
        var fail = DummyData.GetArray(DummyData.GetString, size: 20);
        var success = DummyData.GetArray(DummyData.GetString, size: 20);
        var list = fail[10..15];

        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotContain(list, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotContain_Single()
    {
        var included = DummyData.GetString();
        
        var success = DummyData.GetArray(DummyData.GetString, excludedValue: included);
        var fail = DummyData.GetArray(DummyData.GetString, included);
        
        TestContract<string[], ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, _) => testArgument.Must().NotContain(included),
            "testArgument",
            true);
    }
}