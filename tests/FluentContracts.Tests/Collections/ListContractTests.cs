using System;
using System.Collections.Generic;
using FluentContracts.Contracts.Collections;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Collections;

[ContractTest("List")]
public class ListContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<List<string>, ListContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetList(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<List<string>, ListContract, ArgumentNullException>(
            DummyData.GetList(DummyData.GetString),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetListPair(DummyData.GetString);

        TestContract<List<string>, ListContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetListPair(DummyData.GetString);

        TestContract<List<string>, ListContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<List<string>, ListContract, ArgumentOutOfRangeException>(
            [],
            DummyData.GetList(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<List<string>, ListContract, ArgumentOutOfRangeException>(
            DummyData.GetList(DummyData.GetString),
            [],
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeWithCount()
    {
        const int size = 10;
        var success = DummyData.GetList(DummyData.GetString, size: size);
        var fail = DummyData.GetList(DummyData.GetString, size: size + 10);
        
        TestContract<List<string>, ListContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeWithCount(size, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWithCount()
    {
        const int size = 10;
        var success = DummyData.GetList(DummyData.GetString, size: size + 10);
        var fail = DummyData.GetList(DummyData.GetString, size: size);
        
        TestContract<List<string>, ListContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeWithCount(size, message),
            "testArgument");
    }
}