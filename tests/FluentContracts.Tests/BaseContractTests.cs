using System;
using Bogus;
using FluentAssertions;
using FluentContracts.Contracts;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

[ContractTest("Common")]
public class BaseContractTests : Tests
{
    [Fact]
    public void Test_Must_Satisfy()
    {
        var success = DummyData.GetPerson();
        var fail = DummyData.GetPerson();
        Func<Person, bool> testCondition = p => p.Email == success.Email;
            
        TestContract<Person, NullableContract<object>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().Satisfy(testCondition, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Satisfy_Own_Exception()
    {
        var success = DummyData.GetPerson();
        var fail = DummyData.GetPerson();
        Func<Person, bool> testCondition = p => p.Email == success.Email;
        
        TestContract<Person, NullableContract<object>, MockException>(
            success,
            fail,
            null,
            (testArgument, _) => 
                testArgument.Must().Satisfy<Person, MockException>(testCondition));
    }

    [Fact]
    public void Test_Must_Satisfy_Own_Exception_With_Message()
    {
        var errorMessage = DummyData.GetRandomMessage();
        var success = DummyData.GetPerson();
        var fail = DummyData.GetPerson();
        Func<Person, bool> testCondition = p => p.Email == success.Email;

        TestContract<Person, NullableContract<object>, MockException>(
            success,
            fail,
            errorMessage,
            (testArgument, message) => 
                testArgument.Must().Satisfy<Person, MockException>(testCondition, message!));
    }

    [Fact]
    public void Test_And_Linker_Throws_On_The_First_Only()
    {
        string? nullString = null;

        var action =
            // ReSharper disable once ExpressionIsAlwaysNull
            // ReSharper disable once AssignNullToNotNullAttribute
            () => nullString.Must().NotBeNull().And.Be("hello");

        action
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(nullString));
    }

    [Fact]
    public void Test_And_Linker_Throws_On_The_Second_Only()
    {
        const string testString = "specific_value";

        var action =
            () => testString.Must().NotBeNull().And.Be("another_value");

        action
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(testString));
    }

    [Fact]
    public void Test_NotBeNull_Own_Exception()
    {
        TestContract<int?, IntContract, MockException>(
            DummyData.GetInt(),
            null,
            null,
            (testArgument, _) => testArgument.Must().NotBeNull<MockException>());
    }

    [Fact]
    public void Test_NotBeNull_Own_Exception_With_Message()
    {
        var errorMessage = DummyData.GetRandomMessage();

        TestContract<int?, IntContract, MockException>(
            DummyData.GetInt(),
            null,
            errorMessage,
            (testArgument, message) => testArgument.Must().NotBeNull<MockException>(message));
    }
}