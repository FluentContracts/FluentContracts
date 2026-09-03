using System;
using FluentAssertions;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Numerics;

/// <summary>
/// The generic number contract (#62 §2): any <c>INumber&lt;T&gt;</c> without a hand-written contract
/// gets the same checks the hand-written ones have. <c>Int128</c> stands in for the integers and
/// <c>Half</c> for the floating-point types, which also exercises the NaN policy asked of the type
/// itself rather than of the <c>double</c>/<c>float</c> switch the hand-written contracts use.
/// </summary>
[ContractTest("Number")]
public class NumberContractTests : Tests
{
    private static Int128 GetInt128() => DummyData.GetInt();
    private static Half GetHalf() => (Half)DummyData.GetInt(minValue: -1_000, maxValue: 1_000);

    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<Int128?, NumberContract<Int128>, ArgumentException>(
            null,
            GetInt128(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<Int128?, NumberContract<Int128>, ArgumentNullException>(
            GetInt128(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetIntPair();

        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be((Int128)pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetIntPair();
        Int128? expected = pair.TestArgument;

        TestContract<Int128?, NumberContract<Int128>, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(expected, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetIntPair();

        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe((Int128)pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetIntPair();
        Int128 included = pair.TestArgument;
        Int128 excluded = pair.DifferentArgument;
        var array = DummyData.GetArray(GetInt128, included, excluded);

        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            included,
            excluded,
            (testArgument, message) => testArgument.Must().BeAnyOf(array, message),
            "testArgument");

        included.Must().BeAnyOf(included);
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetIntPair();
        Int128 included = pair.TestArgument;
        Int128 excluded = pair.DifferentArgument;
        var array = DummyData.GetArray(GetInt128, included, excluded);

        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            excluded,
            included,
            (testArgument, message) => testArgument.Must().NotBeAnyOf(array, message),
            "testArgument");

        excluded.Must().NotBeAnyOf(included);
    }

    [Fact]
    public void Test_Must_BePositive()
    {
        TestContract<Half, NumberContract<Half>, ArgumentOutOfRangeException>(
            (Half)1.5,
            (Half)(-1.5),
            (testArgument, message) => testArgument.Must().BePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBePositive()
    {
        TestContract<Half, NumberContract<Half>, ArgumentOutOfRangeException>(
            Half.Zero,
            (Half)1.5,
            (testArgument, message) => testArgument.Must().NotBePositive(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNegative()
    {
        TestContract<Int128, NumberContract<Int128>, ArgumentOutOfRangeException>(
            -1,
            0,
            (testArgument, message) => testArgument.Must().BeNegative(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNegative()
    {
        TestContract<Int128, NumberContract<Int128>, ArgumentOutOfRangeException>(
            0,
            -1,
            (testArgument, message) => testArgument.Must().NotBeNegative(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween()
    {
        TestContract<Int128, NumberContract<Int128>, ArgumentOutOfRangeException>(
            5,
            11,
            (testArgument, message) => testArgument.Must().BeBetween(1, 10, message),
            "testArgument");

        Int128? start = 1;
        Int128? end = 10;
        ((Int128)5).Must().BeBetween(start, end);
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var pair = DummyData.GetIntPair();
        Int128 value = pair.TestArgument;

        TestContract<Int128, NumberContract<Int128>, ArgumentOutOfRangeException>(
            value + 1,
            value,
            (testArgument, message) => testArgument.Must().BeGreaterThan(value, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualTo()
    {
        var pair = DummyData.GetIntPair();
        Int128 value = pair.TestArgument;

        TestContract<Int128, NumberContract<Int128>, ArgumentOutOfRangeException>(
            value,
            value - 1,
            (testArgument, message) => testArgument.Must().BeGreaterOrEqualTo(value, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var pair = DummyData.GetIntPair();
        Int128 value = pair.TestArgument;

        TestContract<Int128, NumberContract<Int128>, ArgumentOutOfRangeException>(
            value - 1,
            value,
            (testArgument, message) => testArgument.Must().BeLessThan(value, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualTo()
    {
        var pair = DummyData.GetIntPair();
        Int128 value = pair.TestArgument;

        TestContract<Int128, NumberContract<Int128>, ArgumentOutOfRangeException>(
            value,
            value + 1,
            (testArgument, message) => testArgument.Must().BeLessOrEqualTo(value, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualTo_Nullable_Operand()
    {
        Int128? value = 10;

        ((Int128)10).Must().BeLessOrEqualTo(value);
        FluentActions.Invoking(() => ((Int128)11).Must().BeLessOrEqualTo(value))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    /// Every comparison takes a nullable operand as well as a plain one, so a caller can compare
    /// against a value that is itself optional. Each is a separate method, and the plain twins are
    /// pinned above; these walk the nullable ones.
    /// </summary>
    [Fact]
    public void Test_Must_NotBe_Nullable_Operand()
    {
        Int128? unexpected = 10;

        ((Int128)11).Must().NotBe(unexpected);
        FluentActions.Invoking(() => ((Int128)10).Must().NotBe(unexpected))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_Must_BeGreaterThan_Nullable_Operand()
    {
        Int128? value = 10;

        ((Int128)11).Must().BeGreaterThan(value);
        FluentActions.Invoking(() => ((Int128)10).Must().BeGreaterThan(value))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualTo_Nullable_Operand()
    {
        Int128? value = 10;

        ((Int128)10).Must().BeGreaterOrEqualTo(value);
        FluentActions.Invoking(() => ((Int128)9).Must().BeGreaterOrEqualTo(value))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable_Operand()
    {
        Int128? value = 10;

        ((Int128)9).Must().BeLessThan(value);
        FluentActions.Invoking(() => ((Int128)10).Must().BeLessThan(value))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Test_Must_BeZero()
    {
        TestContract<Half, NumberContract<Half>, ArgumentException>(
            Half.Zero,
            (Half)1,
            (testArgument, message) => testArgument.Must().BeZero(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeZero()
    {
        TestContract<Half, NumberContract<Half>, ArgumentException>(
            (Half)1,
            Half.Zero,
            (testArgument, message) => testArgument.Must().NotBeZero(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeOdd()
    {
        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            3,
            4,
            (testArgument, message) => testArgument.Must().BeOdd(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeOdd()
    {
        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            4,
            3,
            (testArgument, message) => testArgument.Must().NotBeOdd(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeEven()
    {
        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            4,
            3,
            (testArgument, message) => testArgument.Must().BeEven(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEven()
    {
        TestContract<Int128, NumberContract<Int128>, ArgumentException>(
            3,
            4,
            (testArgument, message) => testArgument.Must().NotBeEven(message),
            "testArgument");
    }

    /// <summary>
    /// Parity on a floating-point number means an integral value; <c>2.5</c> is neither.
    /// </summary>
    [Fact]
    public void Parity_of_a_fraction_is_neither_odd_nor_even()
    {
        var half = (Half)2.5;

        FluentActions.Invoking(() => half.Must().BeOdd()).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => half.Must().BeEven()).Should().Throw<ArgumentException>();
        ((Half)2).Must().BeEven();
    }

    [Fact]
    public void Test_Must_BeNaN()
    {
        TestContract<Half, NumberContract<Half>, ArgumentOutOfRangeException>(
            Half.NaN,
            GetHalf(),
            (testArgument, message) => testArgument.Must().BeNaN(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNaN()
    {
        TestContract<Half, NumberContract<Half>, ArgumentOutOfRangeException>(
            GetHalf(),
            Half.NaN,
            (testArgument, message) => testArgument.Must().NotBeNaN(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInfinity()
    {
        TestContract<Half, NumberContract<Half>, ArgumentOutOfRangeException>(
            Half.NegativeInfinity,
            GetHalf(),
            (testArgument, message) => testArgument.Must().BeInfinity(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInfinity()
    {
        TestContract<Half, NumberContract<Half>, ArgumentOutOfRangeException>(
            GetHalf(),
            Half.PositiveInfinity,
            (testArgument, message) => testArgument.Must().NotBeInfinity(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeFinite()
    {
        TestContract<Half, NumberContract<Half>, ArgumentOutOfRangeException>(
            GetHalf(),
            Half.NaN,
            (testArgument, message) => testArgument.Must().BeFinite(message),
            "testArgument");

        FluentActions.Invoking(() => Half.PositiveInfinity.Must().BeFinite())
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    /// An integer is never NaN or infinite, so the checks are decided by the type and the
    /// negations always pass.
    /// </summary>
    [Fact]
    public void An_integer_is_always_finite()
    {
        var value = GetInt128();

        value.Must().NotBeNaN().NotBeInfinity().BeFinite();
        FluentActions.Invoking(() => value.Must().BeNaN()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => value.Must().BeInfinity()).Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    /// The NaN policy from <c>NonFiniteNumberTests</c>, for a type the hand-written switch does not
    /// know: every ordering check rejects NaN with <see cref="ArgumentOutOfRangeException"/>, and
    /// equality is untouched by the policy.
    /// </summary>
    [Fact]
    public void Every_ordering_check_rejects_NaN()
    {
        var myArgument = Half.NaN;

        FluentActions.Invoking(() => myArgument.Must().BePositive()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeNegative()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().NotBePositive()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().NotBeNegative()).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeGreaterThan(Half.Zero)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeGreaterOrEqualTo(Half.Zero)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeLessThan(Half.Zero)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeLessOrEqualTo(Half.Zero)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().BeBetween(Half.NegativeOne, Half.One)).Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Expected myArgument to not be NaN.*");

        myArgument.Must().NotBe(Half.Zero).NotBeZero();
    }

    [Fact]
    public void A_null_argument_is_rejected_by_every_ordering_check()
    {
        Half? myArgument = null;

        FluentActions.Invoking(() => myArgument.Must().BePositive()).Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(myArgument));
        FluentActions.Invoking(() => myArgument.Must().BeGreaterThan(Half.Zero)).Should().Throw<ArgumentNullException>();
        FluentActions.Invoking(() => myArgument.Must().BeBetween(Half.Zero, Half.One)).Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Failure_messages_read_like_the_hand_written_contracts()
    {
        Int128 quantity = 3;

        FluentActions.Invoking(() => quantity.Must().BeGreaterThan(10))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Expected quantity to be greater than 10, but found 3. (Parameter 'quantity')");

        FluentActions.Invoking(() => quantity.Must().BeEven())
            .Should().Throw<ArgumentException>()
            .WithMessage("Expected quantity to be even, but found 3. (Parameter 'quantity')");
    }

    [Fact]
    public void The_chain_message_and_Value_work_as_on_every_contract()
    {
        Int128 quantity = 3;

        quantity.Must().BePositive().BeOdd().Value().Should().Be(quantity);

        FluentActions.Invoking(() => quantity.Must("{argument} must be at least 10, got {value}").BeGreaterOrEqualTo(10))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("quantity must be at least 10, got 3 (Parameter 'quantity')");

        Int128? missing = null;
        FluentActions.Invoking(() => missing.Must().Value()).Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(missing));
    }
}
