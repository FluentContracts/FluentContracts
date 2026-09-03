using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// The 4.0.0 message design (#62 §5): a caller's message may carry <c>{argument}</c> and
/// <c>{value}</c>, filled on the failure path; and <c>Must()</c> takes a message for every check in
/// the chain, which a check's own message still overrides. Nothing new to learn beyond the
/// message parameter that already existed.
/// </summary>
[ContractTest("MessageDesign")]
public class MessageDesignTests
{
    [Fact]
    public void Tokens_name_the_argument_and_render_its_value()
    {
        const int port = 70000;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => port.Must().BeBetween(1, 65535, "{argument} must be a usable port, got {value}"));

        exception.Message.Should().Be("port must be a usable port, got 70000 (Parameter 'port')");
    }

    [Fact]
    public void Value_token_uses_the_same_rendering_as_default_messages()
    {
        const string text = "actual";

        var exception = Assert.Throws<ArgumentException>(
            () => text.Must().Be("expected", "wanted expected, not {value}"));

        exception.Message.Should().Be("wanted expected, not \"actual\" (Parameter 'text')");
    }

    [Fact]
    public void Tokens_work_on_null_checks_too()
    {
        string? name = null;

        var exception = Assert.Throws<ArgumentNullException>(() => name.Must().NotBeNull("{argument} is required"));

        exception.Message.Should().Be("name is required (Parameter 'name')");
    }

    [Fact]
    public void A_message_without_tokens_is_untouched()
    {
        const int number = 3;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => number.Must().BeGreaterThan(10, "Quantity is too small"));

        exception.Message.Should().Be("Quantity is too small (Parameter 'number')");
    }

    [Fact]
    public void The_chain_message_covers_every_check()
    {
        const string environment = "test";

        FluentActions
            .Invoking(() => environment.Must("This should be prod").NotBe("test").NotBeEmpty())
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("This should be prod*")
            .WithParameterName(nameof(environment));

        FluentActions
            .Invoking(() => "".Must("This should be prod").NotBe("test").NotBeEmpty())
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("This should be prod*");
    }

    [Fact]
    public void A_checks_own_message_wins_over_the_chain_message()
    {
        const string environment = "test";

        FluentActions
            .Invoking(() => environment.Must("This should be prod").NotBe("test", "Not the test slot"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Not the test slot*");
    }

    [Fact]
    public void The_chain_message_takes_tokens_and_keeps_the_captured_argument_name()
    {
        int? retries = null;

        var exception = Assert.Throws<ArgumentNullException>(
            () => retries.Must("{argument} must be configured").NotBeNull());

        exception.Message.Should().Be("retries must be configured (Parameter 'retries')");
    }

    /// <summary>
    /// A chain-wide message is stored once on the contract, and every check has to hand it on itself
    /// as <c>message ?? ChainMessage</c>. A check that forgets still fails, just with the generated
    /// sentence — the caller's message disappears without a trace. These walk the families where that
    /// forwarding is written out per check rather than inherited from a shared base, which is where
    /// one can be missed.
    /// </summary>
    [Fact]
    public void The_chain_message_reaches_the_non_finite_checks()
    {
        const string message = "Not a usable measurement";

        FailsWith(() => 1.0.Must(message).BeNaN(), message);
        FailsWith(() => double.NaN.Must(message).NotBeNaN(), message);
        FailsWith(() => 1.0.Must(message).BeInfinity(), message);
        FailsWith(() => double.PositiveInfinity.Must(message).NotBeInfinity(), message);
        FailsWith(() => double.NaN.Must(message).BeFinite(), message);
        FailsWith(() => 1.0.Must(message).NotBeFinite(), message);

        FailsWith(() => 1f.Must(message).BeNaN(), message);
        FailsWith(() => float.NaN.Must(message).NotBeNaN(), message);
        FailsWith(() => 1f.Must(message).BeInfinity(), message);
        FailsWith(() => float.PositiveInfinity.Must(message).NotBeInfinity(), message);
        FailsWith(() => float.NaN.Must(message).BeFinite(), message);
        FailsWith(() => 1f.Must(message).NotBeFinite(), message);
    }

    [Fact]
    public void A_checks_own_message_wins_on_the_non_finite_checks()
    {
        const string chain = "Not a usable measurement";
        const string own = "This one has to be a number";

        FailsWith(() => 1.0.Must(chain).BeNaN(own), own);
        FailsWith(() => double.NaN.Must(chain).NotBeNaN(own), own);
        FailsWith(() => 1.0.Must(chain).BeInfinity(own), own);
        FailsWith(() => double.PositiveInfinity.Must(chain).NotBeInfinity(own), own);
        FailsWith(() => double.NaN.Must(chain).BeFinite(own), own);
        FailsWith(() => 1.0.Must(chain).NotBeFinite(own), own);

        FailsWith(() => 1f.Must(chain).BeNaN(own), own);
        FailsWith(() => float.NaN.Must(chain).NotBeNaN(own), own);
        FailsWith(() => 1f.Must(chain).BeInfinity(own), own);
        FailsWith(() => float.PositiveInfinity.Must(chain).NotBeInfinity(own), own);
        FailsWith(() => float.NaN.Must(chain).BeFinite(own), own);
        FailsWith(() => 1f.Must(chain).NotBeFinite(own), own);
    }

    [Fact]
    public void The_chain_message_reaches_the_generic_number_checks()
    {
        const string message = "Quantity is out of range";

        FailsWith(() => ((Int128)5).Must(message).NotBe((Int128)5), message);
        FailsWith(() => ((Int128)5).Must(message).BeBetween((Int128)10, (Int128)20), message);
        FailsWith(() => ((Int128)5).Must(message).BeGreaterThan((Int128)10), message);
        FailsWith(() => ((Int128)5).Must(message).BeGreaterOrEqualTo((Int128)10), message);
        FailsWith(() => ((Int128)25).Must(message).BeLessThan((Int128)10), message);
        FailsWith(() => ((Int128)25).Must(message).BeLessOrEqualTo((Int128)10), message);
    }

    /// <summary>
    /// Each comparison has a nullable-operand twin, which is a second copy of the same three
    /// forwarding lines. A message given to the check itself has to win there as well.
    /// </summary>
    [Fact]
    public void A_checks_own_message_wins_on_the_generic_number_checks()
    {
        const string chain = "Quantity is out of range";
        const string own = "Quantity must sit inside the batch size";
        Int128? ten = 10;
        Int128? twenty = 20;

        FailsWith(() => ((Int128)10).Must(chain).NotBe(ten, own), own);
        FailsWith(() => ((Int128)5).Must(chain).NotBe((Int128)5, own), own);
        FailsWith(() => ((Int128)5).Must(chain).BeBetween(ten, twenty, own), own);
        FailsWith(() => ((Int128)5).Must(chain).BeGreaterThan(ten, own), own);
        FailsWith(() => ((Int128)5).Must(chain).BeGreaterOrEqualTo(ten, own), own);
        FailsWith(() => ((Int128)25).Must(chain).BeLessThan(ten, own), own);
        FailsWith(() => ((Int128)25).Must(chain).BeLessOrEqualTo(ten, own), own);
    }

    [Fact]
    public void The_chain_message_reaches_the_list_order_checks()
    {
        const string message = "Pages are not in the expected order";
        IList<int> ascending = [1, 2, 3];
        IList<int> descending = [3, 2, 1];

        FailsWith(() => ascending.Must(message).NotBeInAscendingOrder(), message);
        FailsWith(() => ascending.Must(message).BeInDescendingOrder(), message);
        FailsWith(() => descending.Must(message).NotBeInDescendingOrder(), message);
        FailsWith(() => ascending.Must(message).HaveElementsOfType<string>(), message);
    }

    [Fact]
    public void A_checks_own_message_wins_on_the_list_order_checks()
    {
        const string chain = "Pages are not in the expected order";
        const string own = "Pages must be shuffled";
        IList<int> ascending = [1, 2, 3];
        IList<int> descending = [3, 2, 1];

        FailsWith(() => ascending.Must(chain).NotBeInAscendingOrder(own), own);
        FailsWith(() => ascending.Must(chain).BeInDescendingOrder(own), own);
        FailsWith(() => descending.Must(chain).NotBeInDescendingOrder(own), own);
        FailsWith(() => ascending.Must(chain).HaveElementsOfType<string>(own), own);
    }

    /// <summary>
    /// <c>BeWhiteSpace</c> is the one string check that runs its own not-null check first, so it has
    /// two places to forward the chain message from rather than one.
    /// </summary>
    [Fact]
    public void The_chain_message_reaches_the_whitespace_check()
    {
        const string message = "The padding column must be blank";

        FailsWith(() => "text".Must(message).BeWhiteSpace(), message);
    }

    private static void FailsWith(Action check, string message) =>
        FluentActions
            .Invoking(check)
            .Should()
            .Throw<ArgumentException>()
            .WithMessage(message + "*");
}
