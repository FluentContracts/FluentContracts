using System;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("Enum")]
public class EnumContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<StarWarsCharacter?, NullableEnumContract<StarWarsCharacter?>, ArgumentOutOfRangeException>(
            null,
            StarWarsCharacter.LukeSkywalker,
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<StarWarsCharacter?, NullableEnumContract<StarWarsCharacter?>, ArgumentNullException>(
            StarWarsCharacter.LukeSkywalker,
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentOutOfRangeException>(
            StarWarsCharacter.LukeSkywalker,
            StarWarsCharacter.DarthVader,
            (testArgument, message) => testArgument.Must().Be(StarWarsCharacter.LukeSkywalker, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentOutOfRangeException>(
            StarWarsCharacter.DarthVader,
            StarWarsCharacter.LukeSkywalker,
            (testArgument, message) => testArgument.Must().NotBe(StarWarsCharacter.LukeSkywalker, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var included = DummyData.GetEnumValue<StarWarsCharacter>();
        var excluded = DummyData.GetEnumValue<StarWarsCharacter>(included);
        
        var array = DummyData.GetArray(() => DummyData.GetEnumValue<StarWarsCharacter>(), included, excluded);

        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentOutOfRangeException>(
            included,
            excluded,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var included = DummyData.GetEnumValue<StarWarsCharacter>();
        var excluded = DummyData.GetEnumValue<StarWarsCharacter>(included);
        
        var array = DummyData.GetArray(() => DummyData.GetEnumValue<StarWarsCharacter>(), included, excluded);

        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentOutOfRangeException>(
            excluded,
            included,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveFlag()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentOutOfRangeException>(
            StarWarsCharacter.DarthMaul | StarWarsCharacter.DarthVader,
            StarWarsCharacter.LukeSkywalker | StarWarsCharacter.PrincessLeia,
            (testArgument, message) => testArgument.Must().HaveFlag(StarWarsCharacter.DarthVader, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHaveFlag()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentOutOfRangeException>(
            StarWarsCharacter.LukeSkywalker | StarWarsCharacter.PrincessLeia,
            StarWarsCharacter.DarthMaul | StarWarsCharacter.DarthVader,
            (testArgument, message) => testArgument.Must().NotHaveFlag(StarWarsCharacter.DarthVader, message),
            "testArgument");
    }
}
