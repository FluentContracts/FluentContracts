using System;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("Guid")]
public class GuidContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<Guid?, GuidContract, ArgumentException>(
            null,
            DummyData.GetGuid(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<Guid?, GuidContract, ArgumentNullException>(
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

        TestContract<Guid, GuidContract, ArgumentException>(
            sameArgument,
            otherArgument,
            (testArgument, message) => testArgument.Must().Be(sameArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var sameArgument = DummyData.GetNullableGuid();
        var otherArgument = DummyData.GetNullableGuid();

        TestContract<Guid?, GuidContract, ArgumentException>(
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

        TestContract<Guid, GuidContract, ArgumentException>(
            otherArgument,
            sameArgument,
            (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var sameArgument = DummyData.GetNullableGuid();
        var otherArgument = DummyData.GetNullableGuid();

        TestContract<Guid?, GuidContract, ArgumentException>(
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

        TestContract<Guid, GuidContract, ArgumentException>(
            included,
            excluded,
            (testArgument, message) =>
                testArgument.Must().BeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var included = DummyData.GetGuid();
        var excluded = DummyData.GetGuid();
        var array = DummyData.GetArray(DummyData.GetGuid, included, excluded);

        TestContract<Guid, GuidContract, ArgumentException>(
            excluded,
            included,
            (testArgument, message) =>
                testArgument.Must().NotBeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<Guid, GuidContract, ArgumentException>(
            Guid.Empty,
            DummyData.GetGuid(),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<Guid, GuidContract, ArgumentException>(
            DummyData.GetGuid(),
            Guid.Empty,
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }
}