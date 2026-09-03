using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// <c>NotContain</c> asks the collection not to hold the given elements. It used to be implemented as the
/// negation of <c>Contain</c>, which asks whether the collection holds them <em>all</em> — so a collection
/// holding some of them satisfied the check and the contract went silently unenforced.
/// </summary>
[ContractTest("CollectionContainment")]
public class CollectionContainmentTests
{
    [Fact]
    public void NotContain_rejects_a_collection_holding_one_of_the_elements()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions
            .Invoking(() => myArgument.Must().NotContain([2, 99]))
            .Should()
            .Throw<ArgumentException>("2 is in the collection")
            .WithParameterName(nameof(myArgument));
    }

    [Fact]
    public void NotContain_accepts_a_collection_holding_none_of_them()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions
            .Invoking(() => myArgument.Must().NotContain([98, 99]))
            .Should()
            .NotThrow();
    }

    [Fact]
    public void NotContain_reports_the_supplied_message()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions
            .Invoking(() => myArgument.Must().NotContain([2, 99], "no reserved ids"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("no reserved ids*");
    }

    [Fact]
    public void NotContain_is_unchanged_for_a_single_element()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions.Invoking(() => myArgument.Must().NotContain(2)).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => myArgument.Must().NotContain(99)).Should().NotThrow();
    }

    /// <summary>
    /// The complement of <c>NotContain</c>: <c>Contain</c> keeps asking for all of them.
    /// </summary>
    [Fact]
    public void Contain_still_requires_every_element()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions.Invoking(() => myArgument.Must().Contain([1, 2])).Should().NotThrow();
        FluentActions
            .Invoking(() => myArgument.Must().Contain([1, 99]))
            .Should()
            .Throw<ArgumentException>("99 is missing");
    }

    /// <summary>
    /// Contain, ContainAnyOf and NotContain are all-of, at-least-one-of and none-of over the same input,
    /// so exactly what each one accepts is worth pinning down side by side.
    /// </summary>
    [Fact]
    public void The_three_containment_checks_differ_on_a_partial_overlap()
    {
        IList<int> myArgument = [1, 2, 3];
        int[] partiallyPresent = [2, 99];

        FluentActions.Invoking(() => myArgument.Must().Contain(partiallyPresent)).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => myArgument.Must().ContainAnyOf(partiallyPresent)).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotContain(partiallyPresent)).Should().Throw<ArgumentException>();
    }

    /// <summary>
    /// A single element is a value, not a message — the same rule the bracketed-set overloads follow —
    /// so <c>ContainAnyOf(2)</c> asks about the element 2 and nothing else.
    /// </summary>
    [Fact]
    public void ContainAnyOf_takes_a_single_element_as_a_value()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions.Invoking(() => myArgument.Must().ContainAnyOf(2)).Should().NotThrow();

        FluentActions
            .Invoking(() => myArgument.Must().ContainAnyOf(99))
            .Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(myArgument));
    }

    /// <summary>
    /// The expected elements are materialised before the collection is searched, because the failure
    /// message renders them a second time and the caller may have handed over a query that can only be
    /// walked once. A deferred sequence is the case that would break if that materialisation were
    /// dropped, and a list — already a collection — is the case that must not be copied for nothing.
    /// </summary>
    [Fact]
    public void The_expected_elements_may_be_a_deferred_sequence()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions.Invoking(() => myArgument.Must().Contain(Deferred(1, 2))).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().ContainAnyOf(Deferred(2, 99))).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotContain(Deferred(98, 99))).Should().NotThrow();

        FluentActions
            .Invoking(() => myArgument.Must().Contain(Deferred(1, 99)))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("*99*");

        FluentActions.Invoking(() => myArgument.Must().ContainAnyOf(Deferred(98, 99))).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => myArgument.Must().NotContain(Deferred(2, 99))).Should().Throw<ArgumentException>();
    }

    /// <summary>
    /// "At least one of nothing" cannot hold, so an empty set fails <c>ContainAnyOf</c> — the mirror of
    /// <c>Contain</c>, where "all of nothing" is vacuously true and passes.
    /// </summary>
    [Fact]
    public void An_empty_set_is_vacuously_true_for_Contain_and_false_for_ContainAnyOf()
    {
        IList<int> myArgument = [1, 2, 3];
        int[] nothing = [];

        FluentActions.Invoking(() => myArgument.Must().Contain(nothing)).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotContain(nothing)).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().ContainAnyOf(nothing)).Should().Throw<ArgumentException>();
    }

    private static IEnumerable<int> Deferred(params int[] values) => values.Select(v => v);
}
