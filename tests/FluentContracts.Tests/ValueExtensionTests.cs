using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// <c>Value()</c> ends a chain with the value it just validated: unwrapped for nullable-wrapped
/// arguments, the same instance for references, and failing a null argument with
/// <see cref="ArgumentNullException"/> naming the argument — the same contract <c>NotBeNull</c>
/// enforces, so ending a chain with <c>Value()</c> is itself a null check.
/// </summary>
[ContractTest("Value")]
public class ValueExtensionTests
{
    [Fact]
    public void Value_unwraps_a_nullable_value_type()
    {
        int? port = 8080;

        int value = port.Must().BeBetween(1, 65535).Value();

        value.Should().Be(8080);
    }

    [Fact]
    public void Value_returns_the_reference_it_validated()
    {
        var name = "totollygeek";

        name.Must().NotBeNullOrEmpty().Value().Should().BeSameAs(name);
    }

    [Fact]
    public void Value_on_a_null_argument_fails_naming_the_argument()
    {
        int? missing = null;

        FluentActions
            .Invoking(() => missing.Must().BeNull().Value())
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(missing));
    }

    [Fact]
    public void Value_returns_the_validated_list_instance()
    {
        IList<int> pages = [1, 2, 3];

        pages.Must().BeInAscendingOrder().Value().Should().BeSameAs(pages);
    }

    [Fact]
    public void Value_returns_the_validated_enum()
    {
        var day = DayOfWeek.Friday;

        day.Must().BeDefined().Value().Should().Be(DayOfWeek.Friday);
    }

    [Fact]
    public void Value_returns_the_validated_guid()
    {
        var id = Guid.NewGuid();

        Guid value = id.Must().NotBeEmpty().Value();

        value.Should().Be(id);
    }
}
