using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using FluentContracts.Contracts.Collections;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Collections;

[ContractTest("List")]
public class ListContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            null,
            DummyData.GetList(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<List<string>, ListContract<string>, ArgumentNullException>(
            DummyData.GetList(DummyData.GetString),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetListPair(DummyData.GetString);

        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetListPair(DummyData.GetString);

        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            [],
            DummyData.GetList(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            DummyData.GetList(DummyData.GetString),
            [],
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountEqualTo()
    {
        const int size = 10;
        var success = DummyData.GetList(DummyData.GetString, size: size);
        var fail = DummyData.GetList(DummyData.GetString, size: size + 10);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountEqualTo(size, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHaveCountEqualTo()
    {
        const int size = 10;
        var success = DummyData.GetList(DummyData.GetString, size: size + 10);
        var fail = DummyData.GetList(DummyData.GetString, size: size);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotHaveCountEqualTo(size, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_Contain()
    {
        var success = DummyData.GetList(DummyData.GetString, size: 20);
        var fail = DummyData.GetList(DummyData.GetString, size: 20);
        var list = success[10..15];

        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().Contain(list, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_Contain_Single()
    {
        var included = DummyData.GetString();
        
        var success = DummyData.GetList(DummyData.GetString, included);
        var fail = DummyData.GetList(DummyData.GetString, excludedValue: included);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, _) => testArgument.Must().Contain(included),
            "testArgument", 
            true);
    }
    
    [Fact]
    public void Test_Must_NotContain()
    {
        var fail = DummyData.GetList(DummyData.GetString, size: 20);
        var success = DummyData.GetList(DummyData.GetString, size: 20);
        var list = fail[10..15];

        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotContain(list, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotContain_Single()
    {
        var included = DummyData.GetString();
        
        var success = DummyData.GetList(DummyData.GetString, excludedValue: included);
        var fail = DummyData.GetList(DummyData.GetString, included);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, _) => testArgument.Must().NotContain(included),
            "testArgument",
            true);
    }
    
    [Fact]
    public void Test_Must_HaveElementsOfType()
    {
        var success = new List<Person>(DummyData.GetArray(DummyData.GetEmployee));
        var fail = DummyData.GetArray(DummyData.GetPerson).ToList();
        
        TestContract<List<Person>, ListContract<Person>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, _) => testArgument.Must().HaveElementsOfType<Employee>(),
            "testArgument",
            true);
    }
    
    [Fact]
    public void Test_Must_HaveCountGreaterThan()
    {
        const int targetLength = 42;
        var success = DummyData.GetList(DummyData.GetString, size: targetLength + 10);
        var fail = DummyData.GetList(DummyData.GetString, size: targetLength - 10);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountGreaterThan(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountGreaterOrEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetList(DummyData.GetString, size: targetLength);
        var success2 = DummyData.GetList(DummyData.GetString, size: targetLength + 10);
        var fail = DummyData.GetList(DummyData.GetString, size: targetLength - 10);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountGreaterOrEqualTo(targetLength, message),
            "testArgument");
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success2,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountGreaterOrEqualTo(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountLessThan()
    {
        const int targetLength = 42;
        var success = DummyData.GetList(DummyData.GetString, size: targetLength - 10);
        var fail = DummyData.GetList(DummyData.GetString, size: targetLength + 10);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountLessThan(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountLessOrEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetList(DummyData.GetString, size: targetLength);
        var success2 = DummyData.GetList(DummyData.GetString, size: targetLength - 10);
        var fail = DummyData.GetList(DummyData.GetString, size: targetLength + 10);
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountLessOrEqualTo(targetLength, message),
            "testArgument");
        
        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success2,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountLessOrEqualTo(targetLength, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveCountBetween()
    {
        const int targetLengthLow = 42;
        const int targetLengthHigh = 69;
        
        var success = DummyData.GetList(DummyData.GetString, size: targetLengthLow + 8);
        var fail = DummyData.GetList(DummyData.GetString, size: targetLengthHigh + 1);

        TestContract<List<string>, ListContract<string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountBetween(targetLengthLow, targetLengthHigh, message),
            "testArgument");
    }
}