using System;
using Bogus;
using FluentContracts.Contracts;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

[ContractTest("Object")]
public class ObjectContractTests : Tests
{
    [Fact]
    public void Test_BeOfType()
    {
        var success = DummyData.GetEmployee();
        var fail = DummyData.GetPerson();

        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeOfType<Employee>(message),
            "testArgument");
        
        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeOfType(typeof(Employee), message),
            "testArgument");
    }
    
    [Fact]
    public void Test_NotBeOfType()
    {
        var success = DummyData.GetPerson();
        var fail = DummyData.GetEmployee();

        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeOfType<Employee>(message),
            "testArgument");
        
        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeOfType(typeof(Employee), message),
            "testArgument");
    }
    
    [Fact]
    public void Test_BeAssignableTo()
    {
        var success = DummyData.GetEmployee();
        var fail = DummyData.GetPerson();

        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeAssignableTo<Employee>(message),
            "testArgument");
        
        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeAssignableTo(typeof(Employee), message),
            "testArgument");
    }
    
    
    [Fact]
    public void Test_NotBeAssignableTo()
    {
        var success = DummyData.GetPerson();
        var fail = DummyData.GetEmployee();

        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeAssignableTo<Employee>(message),
            "testArgument");
        
        TestContract<Person, ObjectContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeAssignableTo(typeof(Employee), message),
            "testArgument");
    }
}