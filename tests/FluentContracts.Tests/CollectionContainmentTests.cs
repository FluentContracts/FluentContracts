using System;
using System.Collections.Generic;
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
            .Throw<ArgumentOutOfRangeException>("2 is in the collection")
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
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("no reserved ids*");
    }

    [Fact]
    public void NotContain_is_unchanged_for_a_single_element()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions.Invoking(() => myArgument.Must().NotContain(2)).Should().Throw<ArgumentOutOfRangeException>();
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
            .Throw<ArgumentOutOfRangeException>("99 is missing");
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

        FluentActions.Invoking(() => myArgument.Must().Contain(partiallyPresent)).Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => myArgument.Must().ContainAnyOf(partiallyPresent)).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotContain(partiallyPresent)).Should().Throw<ArgumentOutOfRangeException>();
    }
}
