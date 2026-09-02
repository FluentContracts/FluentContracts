using System;
using FluentContracts.Contracts.Struct;
using FluentContracts.Tests.Mocks;
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
        TestContract<StarWarsCharacter?, EnumContract<StarWarsCharacter>, ArgumentException>(
            null,
            StarWarsCharacter.LukeSkywalker,
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<StarWarsCharacter?, EnumContract<StarWarsCharacter>, ArgumentNullException>(
            StarWarsCharacter.LukeSkywalker,
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentException>(
            StarWarsCharacter.LukeSkywalker,
            StarWarsCharacter.DarthVader,
            (testArgument, message) => testArgument.Must().Be(StarWarsCharacter.LukeSkywalker, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        StarWarsCharacter? expectedValue = StarWarsCharacter.LukeSkywalker;
        
        TestContract<StarWarsCharacter?, EnumContract<StarWarsCharacter>, ArgumentException>(
            expectedValue,
            StarWarsCharacter.DarthVader,
            (testArgument, message) => testArgument.Must().Be(expectedValue, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentException>(
            StarWarsCharacter.DarthVader,
            StarWarsCharacter.LukeSkywalker,
            (testArgument, message) => testArgument.Must().NotBe(StarWarsCharacter.LukeSkywalker, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        StarWarsCharacter? expectedValue = StarWarsCharacter.LukeSkywalker;
        
        TestContract<StarWarsCharacter?, EnumContract<StarWarsCharacter>, ArgumentException>(
            StarWarsCharacter.DarthVader,
            expectedValue,
            (testArgument, message) => testArgument.Must().NotBe(expectedValue, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var included = DummyData.GetEnumValue<StarWarsCharacter>();
        var excluded = DummyData.GetEnumValue<StarWarsCharacter>(included);
        
        var array = DummyData.GetArray(() => DummyData.GetEnumValue<StarWarsCharacter>(), included, excluded);

        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentException>(
            included,
            excluded,
            (testArgument, message) =>
                testArgument.Must().BeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var included = DummyData.GetEnumValue<StarWarsCharacter>();
        var excluded = DummyData.GetEnumValue<StarWarsCharacter>(included);
        
        var array = DummyData.GetArray(() => DummyData.GetEnumValue<StarWarsCharacter>(), included, excluded);

        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentException>(
            excluded,
            included,
            (testArgument, message) =>
                testArgument.Must().NotBeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveFlag()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentException>(
            StarWarsCharacter.DarthMaul | StarWarsCharacter.DarthVader,
            StarWarsCharacter.LukeSkywalker | StarWarsCharacter.PrincessLeia,
            (testArgument, message) => testArgument.Must().HaveFlag(StarWarsCharacter.DarthVader, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHaveFlag()
    {
        TestContract<StarWarsCharacter, EnumContract<StarWarsCharacter>, ArgumentException>(
            StarWarsCharacter.LukeSkywalker | StarWarsCharacter.PrincessLeia,
            StarWarsCharacter.DarthMaul | StarWarsCharacter.DarthVader,
            (testArgument, message) => testArgument.Must().NotHaveFlag(StarWarsCharacter.DarthVader, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeDefined()
    {
        TestContract<DayOfWeek, EnumContract<DayOfWeek>, ArgumentException>(
            DayOfWeek.Monday,
            (DayOfWeek)9,
            (testArgument, message) => testArgument.Must().BeDefined(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeDefined()
    {
        TestContract<DayOfWeek, EnumContract<DayOfWeek>, ArgumentException>(
            (DayOfWeek)9,
            DayOfWeek.Monday,
            (testArgument, message) => testArgument.Must().NotBeDefined(message),
            "testArgument");
    }
}
