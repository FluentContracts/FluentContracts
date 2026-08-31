using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// A user-defined exception must be built the way the caller would build it. The message used to be
/// assigned to <see cref="Exception"/>'s private message field, which skipped the constructor entirely:
/// anything it did was silently not done, and an exception storing its own message lost it altogether.
/// </summary>
[ContractTest("UserDefinedException")]
public class UserDefinedExceptionTests
{
    [Fact]
    public void The_message_constructor_is_used_when_there_is_one()
    {
        string? argument = null;

        var thrown = FluentActions
            .Invoking(() => argument.Must().NotBeNull<ConstructorDoesWork>("boom"))
            .Should()
            .Throw<ConstructorDoesWork>()
            .Which;

        thrown.Message.Should().Be("boom");
        thrown.ConstructorRan.Should().BeTrue("the (string) constructor is how the caller would build it");
        thrown.Code.Should().Be("set-by-constructor", "work done by the constructor must not be skipped");
    }

    [Fact]
    public void An_exception_storing_its_own_message_keeps_it()
    {
        string? argument = null;

        FluentActions
            .Invoking(() => argument.Must().NotBeNull<StoresItsOwnMessage>("boom"))
            .Should()
            .Throw<StoresItsOwnMessage>()
            .WithMessage("boom");
    }

    [Fact]
    public void An_exception_without_a_message_constructor_still_carries_the_message()
    {
        string? argument = null;

        FluentActions
            .Invoking(() => argument.Must().NotBeNull<NoMessageConstructor>("boom"))
            .Should()
            .Throw<NoMessageConstructor>()
            .WithMessage("boom");
    }

    [Fact]
    public void An_exception_thrown_without_a_message_is_still_the_requested_type()
    {
        string? argument = null;

        FluentActions
            .Invoking(() => argument.Must().NotBeNull<ConstructorDoesWork>())
            .Should()
            .Throw<ConstructorDoesWork>();
    }

    public class ConstructorDoesWork : Exception
    {
        public ConstructorDoesWork()
        {
        }

        public ConstructorDoesWork(string message) : base(message)
        {
            ConstructorRan = true;
            Code = "set-by-constructor";
        }

        public bool ConstructorRan { get; }
        public string Code { get; } = "unset";
    }

    public class StoresItsOwnMessage : Exception
    {
        private readonly string? _message;

        public StoresItsOwnMessage()
        {
        }

        public StoresItsOwnMessage(string message) => _message = message;

        public override string Message => _message ?? "(no message)";
    }

    public class NoMessageConstructor : Exception;
}
