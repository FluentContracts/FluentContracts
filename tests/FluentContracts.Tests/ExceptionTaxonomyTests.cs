using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// Pins the 4.0.0 exception taxonomy (#62 §3) across every check family, so no check can drift:
/// <list type="bullet">
/// <item><see cref="ArgumentNullException"/> for a null argument;</item>
/// <item><see cref="ArgumentOutOfRangeException"/> only for ordinal checks — comparisons, ranges,
/// sign, the NaN policy — where "out of range" means what it says;</item>
/// <item><see cref="ArgumentException"/> for everything else: equality, type, format, containment,
/// string shape, collection order.</item>
/// </list>
/// <see cref="ArgumentOutOfRangeException"/> derives from <see cref="ArgumentException"/>, so a
/// <c>catch (ArgumentException)</c> still catches every contract failure but null.
/// </summary>
[ContractTest("ExceptionTaxonomy")]
public class ExceptionTaxonomyTests
{
    public static TheoryData<string, Action> OrdinalChecks =>
        new()
        {
            { "BeGreaterThan", () => 3.Must().BeGreaterThan(10) },
            { "BeLessOrEqualTo", () => 30.Must().BeLessOrEqualTo(10) },
            { "BeBetween", () => 42.Must().BeBetween(5, 10) },
            { "BePositive", () => (-1).Must().BePositive() },
            { "NotBeNaN", () => double.NaN.Must().BeGreaterThan(0) },
            { "BeFinite", () => double.PositiveInfinity.Must().BeFinite() },
            { "TimeSpan.BeShorterThan", () => TimeSpan.FromHours(2).Must().BeShorterThan(TimeSpan.FromHours(1)) },
            { "DateTime.BeInThePast", () => DateTime.MaxValue.Must().BeInThePast() },
            { "TimeOnly.BeBetween", () => new TimeOnly(12, 0).Must().BeBetween(new TimeOnly(1, 0), new TimeOnly(2, 0)) },
            { "HaveCountGreaterThan", () => new List<int> { 1 }.Must().HaveCountGreaterThan(5) },
            { "HaveLengthLessThan", () => "abcdef".Must().HaveLengthLessThan(3) },
        };

    public static TheoryData<string, Action> NonOrdinalChecks =>
        new()
        {
            { "Be", () => "a".Must().Be("b") },
            { "NotBe", () => "a".Must().NotBe("a") },
            { "BeAnyOf", () => "a".Must().BeAnyOf(["b", "c"]) },
            { "BeNull", () => "a".Must().BeNull() },
            { "BeOfType", () => ((object)1).Must().BeOfType<string>() },
            { "BeEmailAddress", () => "nope".Must().BeEmailAddress() },
            { "BeUppercase", () => "abc".Must().BeUppercase() },
            { "BeEven", () => 3.Must().BeEven() },
            { "Contain", () => new List<int> { 1 }.Must().Contain(2) },
            { "HaveUniqueItems", () => new List<int> { 1, 1 }.Must().HaveUniqueItems() },
            { "BeInAscendingOrder", () => new List<int> { 2, 1 }.Must().BeInAscendingOrder() },
            { "HaveCountEqualTo", () => new List<int> { 1 }.Must().HaveCountEqualTo(5) },
            { "ContainKey", () => new Dictionary<string, int>().Must().ContainKey("k") },
            { "Satisfy", () => 3.Must().Satisfy<int?>(n => n > 10) },
            { "BeDefined", () => ((DayOfWeek)9).Must().BeDefined() },
        };

    [Theory]
    [MemberData(nameof(OrdinalChecks))]
    public void Ordinal_checks_throw_ArgumentOutOfRangeException(string check, Action act)
    {
        act.Should().ThrowExactly<ArgumentOutOfRangeException>(check);
    }

    [Theory]
    [MemberData(nameof(NonOrdinalChecks))]
    public void Non_ordinal_checks_throw_ArgumentException(string check, Action act)
    {
        act.Should().ThrowExactly<ArgumentException>(check);
    }

    [Fact]
    public void A_null_argument_is_ArgumentNullException_whatever_the_check()
    {
        string? text = null;
        int? number = null;

        FluentActions.Invoking(() => text.Must().NotBeNull()).Should().ThrowExactly<ArgumentNullException>();
        FluentActions.Invoking(() => text.Must().BeUppercase()).Should().ThrowExactly<ArgumentNullException>();
        FluentActions.Invoking(() => number.Must().BeGreaterThan(1)).Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Catching_ArgumentException_catches_every_non_null_failure()
    {
        FluentActions.Invoking(() => 3.Must().BeGreaterThan(10)).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => "a".Must().Be("b")).Should().Throw<ArgumentException>();
    }
}
