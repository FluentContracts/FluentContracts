using System;
using FluentAssertions;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// Pins the 4.0.0 chain shape: every check returns the contract itself, <c>And</c> is that same
/// contract kept for source compatibility, and a chain therefore allocates one object however many
/// checks it runs. The <c>Linker</c> that every hierarchy level used to allocate is gone.
/// </summary>
[ContractTest("ChainShape")]
public class ChainShapeTests
{
    [Fact]
    public void A_check_returns_the_contract_it_was_called_on()
    {
        var contract = 42.Must();

        IntContract returned = contract.BeGreaterThan(5);

        returned.Should().BeSameAs(contract);
    }

    [Fact]
    public void And_is_the_contract_itself()
    {
        var contract = 42.Must();

        contract.And.Should().BeSameAs(contract);
        contract.BeGreaterThan(5).And.Should().BeSameAs(contract);
    }

    [Fact]
    public void Chains_read_with_or_without_And()
    {
        int? port = 8080;

        int withAnd = port.Must().NotBeNull().And.BeBetween(1, 65535).Value();
        int without = port.Must().NotBeNull().BeBetween(1, 65535).Value();

        withAnd.Should().Be(8080);
        without.Should().Be(8080);
    }

    [Fact]
    public void Value_directly_after_Must_is_a_null_check()
    {
        int? present = 7;
        int? missing = null;

        present.Must().Value().Should().Be(7);

        FluentActions
            .Invoking(() => missing.Must().Value())
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(missing));
    }

    [Fact]
    public void The_Linker_type_no_longer_exists()
    {
        typeof(IntContract).Assembly.GetType("FluentContracts.Infrastructure.Linker`1").Should().BeNull();
    }
}
