using System;
using Bogus;
using FluentAssertions;
using FluentContracts.Specifications;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// The extensibility design (#62 §4): a rule written once as an <see cref="ISpecification{T}"/>
/// runs through <c>Satisfy(specification)</c> and fails like a built-in check, using the rule's own
/// expectation phrase. Spec-based <c>Satisfy</c> inherits the <c>Func</c> overloads' semantics
/// exactly — implicit not-null, conversion to <c>T</c>, the message parameter, the chain message.
/// </summary>
[ContractTest("Specifications")]
public class SpecificationTests
{
    private static readonly ISpecification<string> ValidIban =
        Spec.From<string>(s => s.StartsWith("DE", StringComparison.Ordinal) && s.Length == 22, "be a valid IBAN");

    private static readonly ISpecification<string> SepaCountry =
        Spec.From<string>(s => s.StartsWith("DE", StringComparison.Ordinal), "be in a SEPA country");

    private sealed class AdultSpecification() : Specification<Person>("be an adult")
    {
        public override bool IsSatisfiedBy(Person value) => value.DateOfBirth <= DateTime.Today.AddYears(-18);
    }

    [Fact]
    public void A_satisfied_specification_passes_and_keeps_the_chain()
    {
        const string iban = "DE89370400440532013000";

        iban.Must().Satisfy(ValidIban).NotBeEmpty().Value().Should().Be(iban);
    }

    [Fact]
    public void A_failed_specification_fails_with_its_expectation_phrase()
    {
        const string iban = "XX00";

        var exception = Assert.Throws<ArgumentException>(() => iban.Must().Satisfy(ValidIban));

        exception.ParamName.Should().Be(nameof(iban));
        exception.Message.Should().Be("Expected iban to be a valid IBAN, but found \"XX00\". (Parameter 'iban')");
    }

    [Fact]
    public void A_class_based_specification_works_the_same_way()
    {
        var person = new Person { DateOfBirth = DateTime.Today.AddYears(-12) };

        var exception = Assert.Throws<ArgumentException>(() => person.Must().Satisfy(new AdultSpecification()));

        exception.Message.Should().StartWith("Expected person to be an adult, but found ");
    }

    [Fact]
    public void Without_an_expectation_the_message_falls_back_to_the_generic_one()
    {
        const int number = 3;

        var exception = Assert.Throws<ArgumentException>(
            () => number.Must().Satisfy(Spec.From<int?>(n => n > 10)));

        exception.Message.Should().Be(
            "Expected number to satisfy the given condition, but found 3. (Parameter 'number')");
    }

    [Fact]
    public void The_callers_message_wins_and_takes_tokens()
    {
        const string iban = "XX00";

        var exception = Assert.Throws<ArgumentException>(
            () => iban.Must().Satisfy(ValidIban, "{argument} is not an IBAN: {value}"));

        exception.Message.Should().Be("iban is not an IBAN: \"XX00\" (Parameter 'iban')");
    }

    [Fact]
    public void The_chain_message_applies_to_a_specification_too()
    {
        const string iban = "XX00";

        var exception = Assert.Throws<ArgumentException>(
            () => iban.Must("Bad account").NotBeEmpty().Satisfy(ValidIban));

        exception.Message.Should().Be("Bad account (Parameter 'iban')");
    }

    [Fact]
    public void A_null_argument_is_rejected_before_the_rule_runs()
    {
        string? iban = null;
        var ran = false;
        var spec = Spec.From<string>(_ => ran = true, "be anything");

        Assert.Throws<ArgumentNullException>(() => iban.Must().Satisfy(spec));

        ran.Should().BeFalse();
    }

    [Fact]
    public void A_null_specification_is_rejected_naming_the_specification()
    {
        const string iban = "DE89370400440532013000";

        var exception = Assert.Throws<ArgumentNullException>(
            () => iban.Must().Satisfy((ISpecification<string>)null!));

        exception.ParamName.Should().Be("specification");
    }

    [Fact]
    public void A_specification_for_a_base_type_applies_to_a_derived_argument()
    {
        var person = new Person { DateOfBirth = DateTime.Today.AddYears(-12) };
        var notNull = Spec.From<object>(o => o is not null, "be anything");

        FluentActions.Invoking(() => person.Must().Satisfy<Person>(notNull)).Should().NotThrow();
    }

    [Fact]
    public void A_user_defined_exception_is_thrown_on_failure()
    {
        const string iban = "XX00";

        Assert.Throws<MockException>(() => iban.Must().Satisfy<string, MockException>(ValidIban));

        var exception = Assert.Throws<MockException>(
            () => iban.Must().Satisfy<string, MockException>(ValidIban, "Not an IBAN"));

        exception.Message.Should().Be("Not an IBAN");
    }

    /// <summary>
    /// The user-defined-exception overloads still have to return the contract when the rule holds, so
    /// the chain carries on. Only their failure path is pinned above, and a check that threw on the way
    /// out would look the same to a test that only ever fails it.
    /// </summary>
    [Fact]
    public void A_satisfied_specification_keeps_the_chain_under_a_user_defined_exception()
    {
        const string iban = "DE89370400440532013000";

        FluentActions
            .Invoking(() => iban.Must().Satisfy<string, MockException>(ValidIban).And.NotBeNullOrEmpty())
            .Should()
            .NotThrow();

        FluentActions
            .Invoking(() => iban.Must().Satisfy<string, MockException>(ValidIban, "Not an IBAN").And.NotBeNullOrEmpty())
            .Should()
            .NotThrow();
    }

    [Fact]
    public void And_requires_both_and_joins_the_phrases()
    {
        const string iban = "FR7630006000011234567890189";
        var composed = ValidIban.And(SepaCountry);

        composed.Expectation.Should().Be("be a valid IBAN and be in a SEPA country");

        var exception = Assert.Throws<ArgumentException>(() => iban.Must().Satisfy(composed));

        exception.Message.Should().StartWith("Expected iban to be a valid IBAN and be in a SEPA country, but found ");

        // The left rule holding is not enough on its own; the right one still has to be consulted.
        "DE89370400440532013000".Must().Satisfy(composed);
    }

    [Fact]
    public void Or_requires_either_and_joins_the_phrases()
    {
        const string iban = "DE00";
        var composed = ValidIban.Or(SepaCountry);

        composed.Expectation.Should().Be("be a valid IBAN or be in a SEPA country");
        FluentActions.Invoking(() => iban.Must().Satisfy(composed)).Should().NotThrow();
    }

    [Fact]
    public void Not_negates_and_prefixes_the_phrase()
    {
        const string iban = "DE89370400440532013000";
        var composed = ValidIban.Not();

        composed.Expectation.Should().Be("not be a valid IBAN");

        var exception = Assert.Throws<ArgumentException>(() => iban.Must().Satisfy(composed));

        exception.Message.Should().StartWith("Expected iban to not be a valid IBAN, but found ");
    }

    [Fact]
    public void Composition_keeps_the_phrase_it_has_when_one_side_has_none()
    {
        var unnamed = Spec.From<string>(_ => true);

        ValidIban.And(unnamed).Expectation.Should().Be("be a valid IBAN");
        unnamed.Or(ValidIban).Expectation.Should().Be("be a valid IBAN");
        unnamed.Not().Expectation.Should().BeNull();
    }

    [Fact]
    public void Combinators_reject_null_operands()
    {
        Assert.Throws<ArgumentNullException>(() => ValidIban.And(null!)).ParamName.Should().Be("right");
        Assert.Throws<ArgumentNullException>(() => ((ISpecification<string>)null!).Or(ValidIban)).ParamName.Should().Be("left");
        Assert.Throws<ArgumentNullException>(() => ((ISpecification<string>)null!).Not()).ParamName.Should().Be("specification");
        Assert.Throws<ArgumentNullException>(() => Spec.From<string>(null!)).ParamName.Should().Be("predicate");
    }

    /// <summary>
    /// <c>And</c> and <c>Or</c> compose the predicates with <c>&amp;&amp;</c> and <c>||</c>, so the right-hand rule
    /// is only consulted when the left leaves the answer open. A rule that is expensive, or that would
    /// throw on input the left-hand one already rejected, depends on that.
    /// </summary>
    [Fact]
    public void Composition_short_circuits_on_the_left_hand_rule()
    {
        var rightWasAsked = false;
        var recordsTheCall = Spec.From<string>(_ =>
        {
            rightWasAsked = true;
            return true;
        }, "be asked");

        // The left rule fails, so And has its answer already.
        FluentActions
            .Invoking(() => "XX00".Must().Satisfy(ValidIban.And(recordsTheCall)))
            .Should()
            .Throw<ArgumentException>();

        rightWasAsked.Should().BeFalse("And stops at the first rule that fails");

        // The left rule holds, so Or has its answer already.
        "DE89370400440532013000".Must().Satisfy(ValidIban.Or(recordsTheCall));

        rightWasAsked.Should().BeFalse("Or stops at the first rule that holds");
    }
}
