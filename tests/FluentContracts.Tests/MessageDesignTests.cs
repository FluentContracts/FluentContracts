using System;
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
}
