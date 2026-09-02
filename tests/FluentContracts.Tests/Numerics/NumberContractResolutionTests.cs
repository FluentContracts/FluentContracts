using System;
using System.Numerics;
using FluentAssertions;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Contracts.Struct;
using FluentContracts.Contracts.Text;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

/// <summary>
/// Pins which contract <c>Must()</c> binds to now that a generic <c>Must&lt;T&gt;() where T : INumber&lt;T&gt;</c>
/// exists beside the hand-written overloads. C# prefers a non-generic method on a tie, so every type
/// with a hand-written contract keeps it — including <c>char</c>, which implements
/// <c>INumber&lt;char&gt;</c> — and only the types without one land on <see cref="NumberContract{T}"/>.
/// An enum fails the <c>INumber</c> constraint and keeps its own contract. The earlier worry (#57)
/// that the generic overload would be ambiguous with the <c>object</c> catch-all is disproved here:
/// an identity conversion beats a boxing one.
/// </summary>
[ContractTest("NumberResolution")]
public class NumberContractResolutionTests
{
    [Fact]
    public void Hand_written_contracts_keep_winning()
    {
        const int number = 1;
        int? nullable = 1;
        const decimal money = 1m;
        const double ratio = 1d;
        const long big = 1L;
        const byte small = 1;

        number.Must().Should().BeOfType<IntContract>();
        nullable.Must().Should().BeOfType<IntContract>();
        money.Must().Should().BeOfType<DecimalContract>();
        ratio.Must().Should().BeOfType<DoubleContract>();
        big.Must().Should().BeOfType<LongContract>();
        small.Must().Should().BeOfType<ByteContract>();
    }

    [Fact]
    public void Char_and_enum_keep_their_own_contracts()
    {
        const char letter = 'a';
        var role = Role.Developer;
        Role? maybe = Role.Manager;

        letter.Must().Should().BeOfType<CharContract>();
        role.Must().Should().BeOfType<EnumContract<Role>>();
        maybe.Must().Should().BeOfType<EnumContract<Role>>();
    }

    [Fact]
    public void Numbers_without_a_hand_written_contract_get_the_generic_one()
    {
        Int128 wide = 1;
        UInt128? maybeWide = 1;
        var half = (Half)1;
        var arbitrary = BigInteger.One;
        nint native = 1;

        wide.Must().Should().BeOfType<NumberContract<Int128>>();
        maybeWide.Must().Should().BeOfType<NumberContract<UInt128>>();
        half.Must().Should().BeOfType<NumberContract<Half>>();
        arbitrary.Must().Should().BeOfType<NumberContract<BigInteger>>();
        native.Must().Should().BeOfType<NumberContract<nint>>();
    }

    [Fact]
    public void The_generic_contract_chains_like_any_other()
    {
        Int128 quantity = 42;

        quantity.Must().NotBeNull().BePositive().And.BeLessThan(100).Value().Should().Be(quantity);
    }
}
