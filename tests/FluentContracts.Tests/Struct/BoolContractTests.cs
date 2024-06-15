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
        TestContract<bool?, BoolContract, ArgumentOutOfRangeException>(
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
        TestContract<bool, BoolContract, ArgumentOutOfRangeException>(
            true,
            false,
            (testArgument, message) => testArgument.Must().Be(true, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        TestContract<bool, BoolContract, ArgumentOutOfRangeException>(
            false,
            true,
            (testArgument, message) => testArgument.Must().NotBe(true, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var array = DummyData.GetArray(() => true, true, false);

        TestContract<bool, BoolContract, ArgumentOutOfRangeException>(
            true,
            false,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var array = DummyData.GetArray(() => true, true, false);

        TestContract<bool, BoolContract, ArgumentOutOfRangeException>(
            false,
            true,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeTrue()
    {
        TestContract<bool, BoolContract, ArgumentOutOfRangeException>(
            true,
            false,
            (testArgument, message) => testArgument.Must().BeTrue(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeFalse()
    {
        TestContract<bool, BoolContract, ArgumentOutOfRangeException>(
            false,
            true,
            (testArgument, message) => testArgument.Must().BeFalse(message),
            "testArgument");
    }
}
