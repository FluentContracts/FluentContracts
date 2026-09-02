using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Text;

/// <summary>
/// Pins the 4.0.0 default for the string containment checks: <see cref="StringComparison.Ordinal"/>,
/// i.e. case-sensitive, where 3.x silently compared case-insensitively. Any previous behaviour is
/// one argument away, since the comparison parameter stays and <c>Contain</c>/<c>NotContain</c>
/// gained one.
/// </summary>
[ContractTest("StringComparisonDefault")]
public class StringComparisonDefaultTests
{
    private const string Text = "Hello World";

    [Fact]
    public void Containment_is_case_sensitive_by_default()
    {
        FluentActions.Invoking(() => Text.Must().Contain("World")).Should().NotThrow();
        FluentActions.Invoking(() => Text.Must().Contain("world")).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => Text.Must().StartWith("hello")).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => Text.Must().EndWith("WORLD")).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void The_negations_follow_the_same_default()
    {
        FluentActions.Invoking(() => Text.Must().NotContain("world")).Should().NotThrow();
        FluentActions.Invoking(() => Text.Must().NotStartWith("hello")).Should().NotThrow();
        FluentActions.Invoking(() => Text.Must().NotEndWith("WORLD")).Should().NotThrow();
    }

    [Fact]
    public void Ignoring_case_is_one_argument_away()
    {
        FluentActions.Invoking(() => Text.Must().Contain("world", StringComparison.OrdinalIgnoreCase)).Should().NotThrow();
        FluentActions.Invoking(() => Text.Must().StartWith("hello", StringComparison.OrdinalIgnoreCase)).Should().NotThrow();
        FluentActions.Invoking(() => Text.Must().EndWith("WORLD", StringComparison.OrdinalIgnoreCase)).Should().NotThrow();
        FluentActions.Invoking(() => Text.Must().NotContain("world", StringComparison.OrdinalIgnoreCase)).Should().Throw<ArgumentException>();
    }
}
