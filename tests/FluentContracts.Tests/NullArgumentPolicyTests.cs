using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// Pins down the null policy shared by every contract:
/// <list type="bullet">
/// <item>an ordering comparison can never be satisfied by a null argument, it throws
/// <see cref="ArgumentNullException"/>;</item>
/// <item>equality and null checks continue to accept null.</item>
/// </list>
/// <c>BeNegative</c> and <c>NotBePositive</c> used to pass silently for a null argument, because
/// <see cref="System.Collections.Generic.Comparer{T}"/> orders null below every other value, while
/// <c>BePositive</c> and <c>NotBeNegative</c> threw <see cref="ArgumentOutOfRangeException"/>
/// instead of <see cref="ArgumentNullException"/>. The whole family is asserted here so no
/// individual check can drift away from the policy again.
/// </summary>
[ContractTest("NullPolicy")]
public class NullArgumentPolicyTests
{
    private static readonly int? NullInt = null;
    private static readonly uint? NullUint = null;
    private static readonly long? NullLong = null;
    private static readonly ulong? NullUlong = null;
    private static readonly short? NullShort = null;
    private static readonly ushort? NullUshort = null;
    private static readonly byte? NullByte = null;
    private static readonly sbyte? NullSbyte = null;
    private static readonly double? NullDouble = null;
    private static readonly float? NullFloat = null;
    private static readonly decimal? NullDecimal = null;

    /// <summary>
    /// The sign checks, which are the ones that used to be inconsistent.
    /// </summary>
    public static TheoryData<string, Action> SignChecks =>
        new()
        {
            { "int.BePositive", () => NullInt.Must().BePositive() },
            { "int.NotBePositive", () => NullInt.Must().NotBePositive() },
            { "int.BeNegative", () => NullInt.Must().BeNegative() },
            { "int.NotBeNegative", () => NullInt.Must().NotBeNegative() },

            { "long.BePositive", () => NullLong.Must().BePositive() },
            { "long.NotBePositive", () => NullLong.Must().NotBePositive() },
            { "long.BeNegative", () => NullLong.Must().BeNegative() },
            { "long.NotBeNegative", () => NullLong.Must().NotBeNegative() },

            { "short.BePositive", () => NullShort.Must().BePositive() },
            { "short.NotBePositive", () => NullShort.Must().NotBePositive() },
            { "short.BeNegative", () => NullShort.Must().BeNegative() },
            { "short.NotBeNegative", () => NullShort.Must().NotBeNegative() },

            { "sbyte.BePositive", () => NullSbyte.Must().BePositive() },
            { "sbyte.NotBePositive", () => NullSbyte.Must().NotBePositive() },
            { "sbyte.BeNegative", () => NullSbyte.Must().BeNegative() },
            { "sbyte.NotBeNegative", () => NullSbyte.Must().NotBeNegative() },

            { "double.BePositive", () => NullDouble.Must().BePositive() },
            { "double.NotBePositive", () => NullDouble.Must().NotBePositive() },
            { "double.BeNegative", () => NullDouble.Must().BeNegative() },
            { "double.NotBeNegative", () => NullDouble.Must().NotBeNegative() },

            { "float.BePositive", () => NullFloat.Must().BePositive() },
            { "float.NotBePositive", () => NullFloat.Must().NotBePositive() },
            { "float.BeNegative", () => NullFloat.Must().BeNegative() },
            { "float.NotBeNegative", () => NullFloat.Must().NotBeNegative() },

            { "decimal.BePositive", () => NullDecimal.Must().BePositive() },
            { "decimal.NotBePositive", () => NullDecimal.Must().NotBePositive() },
            { "decimal.BeNegative", () => NullDecimal.Must().BeNegative() },
            { "decimal.NotBeNegative", () => NullDecimal.Must().NotBeNegative() }
        };

    /// <summary>
    /// The explicit ordering comparisons, across signed and unsigned types alike.
    /// </summary>
    public static TheoryData<string, Action> ComparisonChecks =>
        new()
        {
            { "int.BeGreaterThan", () => NullInt.Must().BeGreaterThan(1) },
            { "int.BeGreaterOrEqualTo", () => NullInt.Must().BeGreaterOrEqualTo(1) },
            { "int.BeLessThan", () => NullInt.Must().BeLessThan(1) },
            { "int.BeLessOrEqualTo", () => NullInt.Must().BeLessOrEqualTo(1) },
            { "int.BeBetween", () => NullInt.Must().BeBetween(1, 5) },

            { "uint.BeGreaterThan", () => NullUint.Must().BeGreaterThan(1) },
            { "uint.BeLessThan", () => NullUint.Must().BeLessThan(1) },
            { "uint.BeBetween", () => NullUint.Must().BeBetween(1, 5) },

            { "long.BeLessThan", () => NullLong.Must().BeLessThan(1) },
            { "ulong.BeLessThan", () => NullUlong.Must().BeLessThan(1) },
            { "short.BeLessThan", () => NullShort.Must().BeLessThan(1) },
            { "ushort.BeLessThan", () => NullUshort.Must().BeLessThan(1) },
            { "byte.BeLessThan", () => NullByte.Must().BeLessThan(1) },
            { "sbyte.BeLessThan", () => NullSbyte.Must().BeLessThan(1) },
            { "double.BeLessThan", () => NullDouble.Must().BeLessThan(1) },
            { "float.BeLessThan", () => NullFloat.Must().BeLessThan(1) },
            { "decimal.BeLessThan", () => NullDecimal.Must().BeLessThan(1) },

            { "DateTime.BeGreaterThan", () => ((DateTime?)null).Must().BeGreaterThan(DateTime.Now) },
            { "DateTime.BeBetween", () => ((DateTime?)null).Must().BeBetween(DateTime.MinValue, DateTime.MaxValue) },
            { "TimeSpan.BeShorterThan", () => ((TimeSpan?)null).Must().BeShorterThan(TimeSpan.MaxValue) },
            { "TimeSpan.BeLongerThan", () => ((TimeSpan?)null).Must().BeLongerThan(TimeSpan.MinValue) },
            { "char.BeLessThan", () => ((char?)null).Must().BeLessThan('z') },
            { "char.BeGreaterThan", () => ((char?)null).Must().BeGreaterThan('a') }
        };

    /// <summary>
    /// Equality and null checks are unaffected: null is a legitimate value to compare for.
    /// </summary>
    public static TheoryData<string, Action> NullAcceptingChecks =>
        new()
        {
            { "int.BeNull", () => NullInt.Must().BeNull() },
            { "int.Be(null)", () => NullInt.Must().Be(null) },
            { "double.BeNull", () => NullDouble.Must().BeNull() },
            { "decimal.Be(null)", () => NullDecimal.Must().Be(null) },
            { "string.BeNull", () => ((string?)null).Must().BeNull() },
            { "string.BeNullOrEmpty", () => ((string?)null).Must().BeNullOrEmpty() },
            { "string.BeNullOrWhiteSpace", () => ((string?)null).Must().BeNullOrWhiteSpace() }
        };

    [Theory]
    [MemberData(nameof(SignChecks))]
    public void Sign_check_on_null_argument_throws_ArgumentNullException(string check, Action act)
    {
        act
            .Should()
            .Throw<ArgumentNullException>($"\"{check}\" must not be satisfiable by a null argument");
    }

    [Theory]
    [MemberData(nameof(ComparisonChecks))]
    public void Comparison_check_on_null_argument_throws_ArgumentNullException(string check, Action act)
    {
        act
            .Should()
            .Throw<ArgumentNullException>($"\"{check}\" must not be satisfiable by a null argument");
    }

    [Theory]
    [MemberData(nameof(NullAcceptingChecks))]
    public void Null_accepting_check_allows_null_argument(string check, Action act)
    {
        act
            .Should()
            .NotThrow($"\"{check}\" is a null check and must accept a null argument");
    }

    [Fact]
    public void Sign_checks_still_evaluate_non_null_arguments()
    {
        int? positive = 5;
        int? negative = -5;

        FluentActions.Invoking(() => positive.Must().BePositive()).Should().NotThrow();
        FluentActions.Invoking(() => positive.Must().NotBeNegative()).Should().NotThrow();
        FluentActions.Invoking(() => negative.Must().BeNegative()).Should().NotThrow();
        FluentActions.Invoking(() => negative.Must().NotBePositive()).Should().NotThrow();

        FluentActions.Invoking(() => positive.Must().BeNegative())
            .Should()
            .Throw<ArgumentOutOfRangeException>();

        FluentActions.Invoking(() => negative.Must().BePositive())
            .Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Null_rejection_reports_the_argument_name()
    {
        int? myArgument = null;

        FluentActions.Invoking(() => myArgument.Must().BeNegative())
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(myArgument));
    }
}
