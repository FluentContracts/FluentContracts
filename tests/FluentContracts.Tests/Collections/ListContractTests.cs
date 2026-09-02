using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using FluentAssertions;
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
        TestContract<List<string>?, ListContract<string>, ArgumentException>(
            null,
            DummyData.GetList(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<List<string>?, ListContract<string>, ArgumentNullException>(
            DummyData.GetList(DummyData.GetString),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetListPair(DummyData.GetString);

        TestContract<List<string>, ListContract<string>, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetListPair(DummyData.GetString);

        TestContract<List<string>, ListContract<string>, ArgumentException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<List<string>, ListContract<string>, ArgumentException>(
            [],
            DummyData.GetList(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<List<string>, ListContract<string>, ArgumentException>(
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
        
        TestContract<List<string>, ListContract<string>, ArgumentException>(
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
        
        TestContract<List<string>, ListContract<string>, ArgumentException>(
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

        TestContract<List<string>, ListContract<string>, ArgumentException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().Contain(list, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_Contain_Single()
    {
        var included = DummyData.GetString();
        
        IList<string> success = DummyData.GetList(DummyData.GetString, included);
        IList<string> fail = DummyData.GetList(DummyData.GetString, excludedValue: included);
        
        TestContract<IList<string>, ListContract<string>, ArgumentException>(
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

        TestContract<List<string>, ListContract<string>, ArgumentException>(
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
        
        TestContract<List<string>, ListContract<string>, ArgumentException>(
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
        
        TestContract<List<Person>, ListContract<Person>, ArgumentException>(
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

    [Fact]
    public void Test_Must_ContainAnyOf()
    {
        TestContract<List<int>, ListContract<int>, ArgumentException>(
            [1, 2, 3],
            [7, 8, 9],
            (testArgument, message) => testArgument.Must().ContainAnyOf([2, 99], message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveUniqueItems()
    {
        TestContract<List<int>, ListContract<int>, ArgumentException>(
            [1, 2, 3],
            [1, 2, 2],
            (testArgument, message) => testArgument.Must().HaveUniqueItems(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotContainNull()
    {
        TestContract<List<string?>, ListContract<string?>, ArgumentException>(
            ["a", "b"],
            ["a", null],
            (testArgument, message) => testArgument.Must().NotContainNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_AllSatisfy()
    {
        TestContract<List<int>, ListContract<int>, ArgumentException>(
            [2, 4, 6],
            [2, 3, 6],
            (testArgument, message) => testArgument.Must().AllSatisfy(x => x % 2 == 0, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_AnySatisfy()
    {
        TestContract<List<int>, ListContract<int>, ArgumentException>(
            [1, 2, 3],
            [1, 1, 1],
            (testArgument, message) => testArgument.Must().AnySatisfy(x => x > 2, message),
            "testArgument");
    }

    /// <summary>
    /// The empty collection is the edge both quantifiers turn on: everything holds of no elements, and
    /// nothing does.
    /// </summary>
    [Fact]
    public void Test_Must_AllSatisfy_And_AnySatisfy_On_An_Empty_Collection()
    {
        List<int> testArgument = [];

        FluentActions
            .Invoking(() => testArgument.Must().AllSatisfy(x => x > 100))
            .Should()
            .NotThrow("every element of an empty collection satisfies any condition");

        FluentActions
            .Invoking(() => testArgument.Must().AnySatisfy(x => x > 100))
            .Should()
            .Throw<ArgumentException>("no element of an empty collection satisfies anything");
    }

    [Fact]
    public void Test_Must_AllSatisfy_Rejects_A_Null_Condition()
    {
        List<int> testArgument = [1, 2, 3];

        FluentActions
            .Invoking(() => testArgument.Must().AllSatisfy(null!))
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("condition");
    }
}
