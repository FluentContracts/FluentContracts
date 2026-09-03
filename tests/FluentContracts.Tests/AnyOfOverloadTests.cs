using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// The 4.0.0 shape of <c>BeAnyOf</c>/<c>NotBeAnyOf</c> on a string argument, where the old
/// message-first overload silently took the first value as the message. Now a value list is always
/// bracketed and a message can only follow one, so every call below checks exactly what it says.
/// The calls that used to be the trap no longer compile; that is pinned by
/// <c>OverloadShapeTests</c> in the analyzer test project, which compiles snippets against the
/// real library.
/// </summary>
[ContractTest("AnyOfOverloads")]
public class AnyOfOverloadTests
{
    [Fact]
    public void A_single_value_is_a_value()
    {
        const string myArgument = "a";

        FluentActions.Invoking(() => myArgument.Must().BeAnyOf("a")).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotBeAnyOf("a")).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void A_bracketed_set_checks_every_value()
    {
        const string myArgument = "b";

        FluentActions.Invoking(() => myArgument.Must().BeAnyOf(["a", "b"])).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().BeAnyOf(["a", "c"])).Should().Throw<ArgumentException>();
        FluentActions.Invoking(() => myArgument.Must().NotBeAnyOf(["a", "b"])).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void A_message_after_the_set_is_the_message()
    {
        const string myArgument = "c";

        FluentActions
            .Invoking(() => myArgument.Must().BeAnyOf(["a", "b"], "Not a known state"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Not a known state*")
            .WithParameterName(nameof(myArgument));
    }

    /// <summary>
    /// A contract holding a value type carries its own <c>BeAnyOf</c>/<c>NotBeAnyOf</c> pair rather
    /// than inheriting the generic one, because it has to unwrap the nullable first. That is one
    /// hand-written pair per contract, each able to be wired to the wrong validator or to skip the
    /// implicit null check, so every one of them is walked here on both the passing and the failing
    /// side. <c>Value()</c> would not catch it: these overloads take no message, so the
    /// <c>TestContract</c> harness cannot exercise them either.
    /// </summary>
    [Fact]
    public void A_single_value_passes_when_the_argument_is_it()
    {
        ((bool?)true).Must().BeAnyOf(true);
        ((byte?)7).Must().BeAnyOf((byte)7);
        ((char?)'k').Must().BeAnyOf('k');
        ((decimal?)19.99m).Must().BeAnyOf(19.99m);
        ((double?)0.5d).Must().BeAnyOf(0.5d);
        ((float?)1.5f).Must().BeAnyOf(1.5f);
        ((int?)42).Must().BeAnyOf(42);
        ((long?)9_000_000_000L).Must().BeAnyOf(9_000_000_000L);
        ((sbyte?)-3).Must().BeAnyOf((sbyte)-3);
        ((short?)2026).Must().BeAnyOf((short)2026);
        ((uint?)42u).Must().BeAnyOf(42u);
        ((ulong?)4_000_000_000UL).Must().BeAnyOf(4_000_000_000UL);
        ((ushort?)8080).Must().BeAnyOf((ushort)8080);
        ((DateTime?)Stamp).Must().BeAnyOf(Stamp);
        ((Guid?)KnownId).Must().BeAnyOf(KnownId);
        ((StarWarsCharacter?)StarWarsCharacter.Yoda).Must().BeAnyOf(StarWarsCharacter.Yoda);
    }

    [Fact]
    public void A_single_value_fails_when_the_argument_is_something_else()
    {
        Rejects(() => ((bool?)true).Must().BeAnyOf(false));
        Rejects(() => ((byte?)7).Must().BeAnyOf((byte)8));
        Rejects(() => ((char?)'k').Must().BeAnyOf('z'));
        Rejects(() => ((decimal?)19.99m).Must().BeAnyOf(20.99m));
        Rejects(() => ((double?)0.5d).Must().BeAnyOf(0.6d));
        Rejects(() => ((float?)1.5f).Must().BeAnyOf(2.5f));
        Rejects(() => ((int?)42).Must().BeAnyOf(43));
        Rejects(() => ((long?)9_000_000_000L).Must().BeAnyOf(9_000_000_001L));
        Rejects(() => ((sbyte?)-3).Must().BeAnyOf((sbyte)-4));
        Rejects(() => ((short?)2026).Must().BeAnyOf((short)2027));
        Rejects(() => ((uint?)42u).Must().BeAnyOf(43u));
        Rejects(() => ((ulong?)4_000_000_000UL).Must().BeAnyOf(4_000_000_001UL));
        Rejects(() => ((ushort?)8080).Must().BeAnyOf((ushort)8081));
        Rejects(() => ((DateTime?)Stamp).Must().BeAnyOf(Stamp.AddDays(1)));
        Rejects(() => ((Guid?)KnownId).Must().BeAnyOf(Guid.Empty));
        Rejects(() => ((StarWarsCharacter?)StarWarsCharacter.Yoda).Must().BeAnyOf(StarWarsCharacter.HanSolo));
    }

    [Fact]
    public void A_single_unexpected_value_passes_when_the_argument_is_something_else()
    {
        ((bool?)true).Must().NotBeAnyOf(false);
        ((byte?)7).Must().NotBeAnyOf((byte)8);
        ((char?)'k').Must().NotBeAnyOf('z');
        ((decimal?)19.99m).Must().NotBeAnyOf(20.99m);
        ((double?)0.5d).Must().NotBeAnyOf(0.6d);
        ((float?)1.5f).Must().NotBeAnyOf(2.5f);
        ((int?)42).Must().NotBeAnyOf(43);
        ((long?)9_000_000_000L).Must().NotBeAnyOf(9_000_000_001L);
        ((sbyte?)-3).Must().NotBeAnyOf((sbyte)-4);
        ((short?)2026).Must().NotBeAnyOf((short)2027);
        ((uint?)42u).Must().NotBeAnyOf(43u);
        ((ulong?)4_000_000_000UL).Must().NotBeAnyOf(4_000_000_001UL);
        ((ushort?)8080).Must().NotBeAnyOf((ushort)8081);
        ((DateTime?)Stamp).Must().NotBeAnyOf(Stamp.AddDays(1));
        ((Guid?)KnownId).Must().NotBeAnyOf(Guid.Empty);
        ((StarWarsCharacter?)StarWarsCharacter.Yoda).Must().NotBeAnyOf(StarWarsCharacter.HanSolo);

        // The generic pair on EqualityContract, which every reference-typed contract inherits.
        "draft".Must().NotBeAnyOf("published");
    }

    [Fact]
    public void A_single_unexpected_value_fails_when_the_argument_is_it()
    {
        Rejects(() => ((bool?)true).Must().NotBeAnyOf(true));
        Rejects(() => ((byte?)7).Must().NotBeAnyOf((byte)7));
        Rejects(() => ((char?)'k').Must().NotBeAnyOf('k'));
        Rejects(() => ((decimal?)19.99m).Must().NotBeAnyOf(19.99m));
        Rejects(() => ((double?)0.5d).Must().NotBeAnyOf(0.5d));
        Rejects(() => ((float?)1.5f).Must().NotBeAnyOf(1.5f));
        Rejects(() => ((int?)42).Must().NotBeAnyOf(42));
        Rejects(() => ((long?)9_000_000_000L).Must().NotBeAnyOf(9_000_000_000L));
        Rejects(() => ((sbyte?)-3).Must().NotBeAnyOf((sbyte)-3));
        Rejects(() => ((short?)2026).Must().NotBeAnyOf((short)2026));
        Rejects(() => ((uint?)42u).Must().NotBeAnyOf(42u));
        Rejects(() => ((ulong?)4_000_000_000UL).Must().NotBeAnyOf(4_000_000_000UL));
        Rejects(() => ((ushort?)8080).Must().NotBeAnyOf((ushort)8080));
        Rejects(() => ((DateTime?)Stamp).Must().NotBeAnyOf(Stamp));
        Rejects(() => ((Guid?)KnownId).Must().NotBeAnyOf(KnownId));
        Rejects(() => ((StarWarsCharacter?)StarWarsCharacter.Yoda).Must().NotBeAnyOf(StarWarsCharacter.Yoda));
    }

    /// <summary>
    /// The message-less overloads still carry the implicit not-null check the documented remarks
    /// promise, so a null argument fails as <c>NotBeNull</c> would rather than on an unwrap.
    /// </summary>
    [Fact]
    public void A_null_argument_is_rejected_before_the_single_value_is_compared()
    {
        int? myArgument = null;

        FluentActions
            .Invoking(() => myArgument.Must().BeAnyOf(42))
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(myArgument));

        FluentActions
            .Invoking(() => myArgument.Must().NotBeAnyOf(42))
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(myArgument));
    }

    /// <summary>
    /// The values are materialised before the argument is compared against them, because the failure
    /// message renders them a second time and the caller may have handed over a query that can only be
    /// walked once — the same treatment the containment checks give their expected elements.
    /// </summary>
    [Fact]
    public void The_expected_values_may_be_a_deferred_sequence()
    {
        const string myArgument = "b";

        FluentActions.Invoking(() => myArgument.Must().BeAnyOf(Deferred("a", "b"))).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotBeAnyOf(Deferred("a", "c"))).Should().NotThrow();

        FluentActions
            .Invoking(() => myArgument.Must().BeAnyOf(Deferred("a", "c")))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("*\"a\"*");

        FluentActions
            .Invoking(() => myArgument.Must().NotBeAnyOf(Deferred("a", "b")))
            .Should()
            .Throw<ArgumentException>();
    }

    /// <summary>
    /// The value-type contracts hand the sequence to the validator as it came, rather than copying it
    /// first as the reference-typed ones do, so the materialisation has to happen there too.
    /// </summary>
    [Fact]
    public void A_value_type_contract_takes_a_deferred_sequence_as_well()
    {
        const int myArgument = 2;

        FluentActions.Invoking(() => myArgument.Must().BeAnyOf(DeferredNumbers(1, 2))).Should().NotThrow();
        FluentActions.Invoking(() => myArgument.Must().NotBeAnyOf(DeferredNumbers(1, 3))).Should().NotThrow();

        FluentActions
            .Invoking(() => myArgument.Must().BeAnyOf(DeferredNumbers(1, 3)))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("*3*");

        FluentActions
            .Invoking(() => myArgument.Must().NotBeAnyOf(DeferredNumbers(1, 2)))
            .Should()
            .Throw<ArgumentException>();
    }

    private static IEnumerable<string> Deferred(params string[] values) => values.Select(v => v);

    private static IEnumerable<int> DeferredNumbers(params int[] values) => values.Select(v => v);

    private static readonly DateTime Stamp = new(2026, 6, 1, 12, 0, 0, DateTimeKind.Utc);
    private static readonly Guid KnownId = new("6f9619ff-8b86-d011-b42d-00cf4fc964ff");

    private static void Rejects(Action check) =>
        FluentActions.Invoking(check).Should().Throw<ArgumentException>();
}
