using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Collections;

/// <summary>
/// The ordering checks on the list contract. Order is non-strict — equal neighbours are in order,
/// matching <c>List&lt;T&gt;.Sort</c> — and an empty or single-element list is vacuously in every
/// order, which is why it satisfies both <c>BeIn...</c> checks and fails both <c>NotBeIn...</c> ones.
/// </summary>
[ContractTest("ListOrdering")]
public class ListOrderingTests
{
    [Fact]
    public void BeInAscendingOrder_accepts_a_sorted_list_with_duplicates()
    {
        IList<int> myArgument = [1, 2, 2, 3];

        FluentActions
            .Invoking(() => myArgument.Must().BeInAscendingOrder())
            .Should()
            .NotThrow();
    }

    [Fact]
    public void BeInAscendingOrder_names_the_first_offending_neighbours()
    {
        IList<int> myArgument = [1, 5, 3, 4];

        FluentActions
            .Invoking(() => myArgument.Must().BeInAscendingOrder())
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(myArgument))
            .WithMessage("Expected myArgument to be in ascending order, but 5 appears before 3.*");
    }

    [Fact]
    public void BeInDescendingOrder_accepts_a_reverse_sorted_list()
    {
        IList<int> myArgument = [3, 2, 2, 1];

        FluentActions
            .Invoking(() => myArgument.Must().BeInDescendingOrder())
            .Should()
            .NotThrow();
    }

    [Fact]
    public void BeInDescendingOrder_rejects_an_ascent()
    {
        IList<int> myArgument = [3, 1, 2];

        FluentActions
            .Invoking(() => myArgument.Must().BeInDescendingOrder())
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(myArgument));
    }

    [Fact]
    public void NotBeInAscendingOrder_accepts_an_unsorted_list_and_rejects_a_sorted_one()
    {
        IList<int> unsorted = [2, 1, 3];
        IList<int> sorted = [1, 2, 3];

        FluentActions.Invoking(() => unsorted.Must().NotBeInAscendingOrder()).Should().NotThrow();

        FluentActions
            .Invoking(() => sorted.Must().NotBeInAscendingOrder())
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(sorted));
    }

    [Fact]
    public void NotBeInDescendingOrder_accepts_an_ascent_and_rejects_a_reverse_sorted_list()
    {
        IList<int> withAscent = [3, 1, 2];
        IList<int> sorted = [3, 2, 1];

        FluentActions.Invoking(() => withAscent.Must().NotBeInDescendingOrder()).Should().NotThrow();

        FluentActions
            .Invoking(() => sorted.Must().NotBeInDescendingOrder())
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(sorted));
    }

    [Fact]
    public void Empty_and_single_element_lists_are_in_every_order()
    {
        IList<int> empty = [];
        IList<int> single = [42];

        FluentActions.Invoking(() => empty.Must().BeInAscendingOrder()).Should().NotThrow();
        FluentActions.Invoking(() => empty.Must().BeInDescendingOrder()).Should().NotThrow();
        FluentActions.Invoking(() => single.Must().BeInAscendingOrder()).Should().NotThrow();
        FluentActions.Invoking(() => single.Must().BeInDescendingOrder()).Should().NotThrow();

        FluentActions.Invoking(() => empty.Must().NotBeInAscendingOrder())
            .Should().Throw<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => single.Must().NotBeInDescendingOrder())
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void A_custom_comparer_defines_the_order()
    {
        IList<int> myArgument = [3, 2, 1];
        var reversed = Comparer<int>.Create((a, b) => b.CompareTo(a));

        FluentActions
            .Invoking(() => myArgument.Must().BeInAscendingOrder(reversed))
            .Should()
            .NotThrow();
    }

    [Fact]
    public void A_null_comparer_is_rejected_naming_the_comparer()
    {
        IList<int> myArgument = [1, 2, 3];

        FluentActions
            .Invoking(() => myArgument.Must().BeInAscendingOrder((IComparer<int>)null!))
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("comparer");
    }

    [Fact]
    public void A_null_list_is_rejected_naming_the_argument()
    {
        IList<int>? myArgument = null;

        FluentActions
            .Invoking(() => myArgument.Must().BeInAscendingOrder())
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(myArgument));
    }

    [Fact]
    public void The_supplied_message_replaces_the_default()
    {
        IList<int> myArgument = [2, 1];

        FluentActions
            .Invoking(() => myArgument.Must().BeInAscendingOrder("Pages must be sorted"))
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Pages must be sorted*");
    }
}
