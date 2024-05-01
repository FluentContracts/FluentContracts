using System;
using FluentContracts.Contracts.Text;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Text;

[ContractTest("String")]
public class StringContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<string, StringContract, ArgumentNullException>(
            DummyData.GetString(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetStringPair();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetStringPair();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetStringPair();
        var array = DummyData.GetArray(() => DummyData.GetString(), pair.TestArgument, pair.DifferentArgument);

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetStringPair();
        var array = DummyData.GetArray(() => DummyData.GetString(), pair.TestArgument, pair.DifferentArgument);

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            string.Empty,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            string.Empty,
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNullOrEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            string.Empty,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrEmpty(message),
            "testArgument");

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNullOrEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            string.Empty,
            (testArgument, message) => testArgument.Must().NotBeNullOrEmpty(message),
            "testArgument");

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNullOrEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNullOrWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.WhiteSpace),
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrWhiteSpace(message),
            "testArgument");

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNullOrWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            DummyData.GetString(StringOption.WhiteSpace),
            (testArgument, message) => testArgument.Must().NotBeNullOrWhiteSpace(message),
            "testArgument");

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNullOrWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.WhiteSpace),
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            DummyData.GetString(StringOption.WhiteSpace),
            (testArgument, message) => testArgument.Must().NotBeWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeUppercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Uppercase),
            DummyData.GetString(StringOption.Lowercase),
            (testArgument, message) => testArgument.Must().BeUppercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeUppercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Lowercase),
            DummyData.GetString(StringOption.Uppercase),
            (testArgument, message) => testArgument.Must().NotBeUppercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLowercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Lowercase),
            DummyData.GetString(StringOption.Uppercase),
            (testArgument, message) => testArgument.Must().BeLowercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLowercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Uppercase),
            DummyData.GetString(StringOption.Lowercase),
            (testArgument, message) => testArgument.Must().NotBeLowercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Contain()
    {
        var pair = DummyData.GetStringPair(PairOption.Containing);
        var notContaining = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            notContaining,
            (testArgument, message) => testArgument.Must().Contain(pair.DifferentArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotContain()
    {
        var pair = DummyData.GetStringPair(PairOption.Containing);
        var notContaining = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            notContaining,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotContain(pair.DifferentArgument, message),
            "testArgument");
    }
}