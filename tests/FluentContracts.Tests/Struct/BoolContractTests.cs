using System;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("Bool")]
public class BoolContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<bool?, BoolContract, ArgumentException>(
            null,
            true,
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<bool?, BoolContract, ArgumentNullException>(
            true,
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        TestContract<bool, BoolContract, ArgumentException>(
            true,
            false,
            (testArgument, message) => testArgument.Must().Be(true, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        bool? successful = true;
        bool? failing = false;
        
        TestContract<bool?, BoolContract, ArgumentException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().Be(successful, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        TestContract<bool, BoolContract, ArgumentException>(
            false,
            true,
            (testArgument, message) => testArgument.Must().NotBe(true, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        bool? successful = false;
        bool? failing = true;
        
        TestContract<bool?, BoolContract, ArgumentException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBe(failing, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var array = DummyData.GetArray(() => true, true, false);

        TestContract<bool, BoolContract, ArgumentException>(
            true,
            false,
            (testArgument, message) =>
                testArgument.Must().BeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var array = DummyData.GetArray(() => true, true, false);

        TestContract<bool, BoolContract, ArgumentException>(
            false,
            true,
            (testArgument, message) =>
                testArgument.Must().NotBeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeTrue()
    {
        TestContract<bool, BoolContract, ArgumentException>(
            true,
            false,
            (testArgument, message) => testArgument.Must().BeTrue(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeFalse()
    {
        TestContract<bool, BoolContract, ArgumentException>(
            false,
            true,
            (testArgument, message) => testArgument.Must().BeFalse(message),
            "testArgument");
    }
}
