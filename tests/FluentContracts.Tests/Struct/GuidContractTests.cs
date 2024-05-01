using System;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("Guid")]
public class GuidContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<Guid?, NullableGuidContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetGuid(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<Guid?, NullableGuidContract, ArgumentNullException>(
            DummyData.GetGuid(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var sameArgument = DummyData.GetGuid();
        var otherArgument = DummyData.GetGuid();

        TestContract<Guid, GuidContract, ArgumentOutOfRangeException>(
            sameArgument,
            otherArgument,
            (testArgument, message) => testArgument.Must().Be(sameArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var sameArgument = DummyData.GetGuid();
        var otherArgument = DummyData.GetGuid();

        TestContract<Guid, GuidContract, ArgumentOutOfRangeException>(
            otherArgument,
            sameArgument,
            (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var included = DummyData.GetGuid();
        var excluded = DummyData.GetGuid();
        var array = DummyData.GetArray(DummyData.GetGuid, included, excluded);

        TestContract<Guid, GuidContract, ArgumentOutOfRangeException>(
            included,
            excluded,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var included = DummyData.GetGuid();
        var excluded = DummyData.GetGuid();
        var array = DummyData.GetArray(DummyData.GetGuid, included, excluded);

        TestContract<Guid, GuidContract, ArgumentOutOfRangeException>(
            excluded,
            included,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<Guid, GuidContract, ArgumentOutOfRangeException>(
            Guid.Empty,
            DummyData.GetGuid(),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<Guid, GuidContract, ArgumentOutOfRangeException>(
            DummyData.GetGuid(),
            Guid.Empty,
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }
}