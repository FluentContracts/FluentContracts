using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// <c>Satisfy&lt;T&gt;</c> casts the argument to <c>T</c> before handing it to the condition. When the
/// runtime value is something else that cast used to surface an <see cref="InvalidCastException"/> from
/// inside the library, rather than a contract failure naming the argument.
/// </summary>
[ContractTest("Satisfy")]
public class SatisfyTypeMismatchTests
{
    [Fact]
    public void A_mismatched_type_fails_the_contract_and_names_the_argument()
    {
        object myArgument = 42;

        FluentActions
            .Invoking(() => myArgument.Must().Satisfy<string>(s => s.Length > 0))
            .Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(myArgument));
    }

    [Fact]
    public void A_mismatched_type_reports_the_supplied_message()
    {
        object myArgument = 42;

        FluentActions
            .Invoking(() => myArgument.Must().Satisfy<string>(s => s.Length > 0, "must be a non-empty string"))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("must be a non-empty string*");
    }

    [Fact]
    public void A_mismatched_type_throws_the_requested_exception()
    {
        object myArgument = 42;

        FluentActions
            .Invoking(() => myArgument.Must().Satisfy<string, InvalidOperationException>(s => s.Length > 0))
            .Should()
            .Throw<InvalidOperationException>();
    }

    [Fact]
    public void A_mismatched_type_throws_the_requested_exception_with_the_message()
    {
        object myArgument = 42;

        FluentActions
            .Invoking(() =>
                myArgument.Must().Satisfy<string, InvalidOperationException>(s => s.Length > 0, "wrong type"))
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("wrong type");
    }

    [Fact]
    public void A_matching_type_is_still_evaluated_by_the_condition()
    {
        object myArgument = "hello";

        FluentActions
            .Invoking(() => myArgument.Must().Satisfy<string>(s => s.Length > 0))
            .Should()
            .NotThrow();

        FluentActions
            .Invoking(() => myArgument.Must().Satisfy<string>(s => s.Length > 100))
            .Should()
            .Throw<ArgumentException>("the type matches, so the condition decides");
    }

    [Fact]
    public void A_null_argument_is_still_rejected_before_the_type_is_considered()
    {
        object? myArgument = null;

        FluentActions
            .Invoking(() => myArgument.Must().Satisfy<string>(s => s.Length > 0))
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(myArgument));
    }
}
