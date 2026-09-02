using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// Pins down the default failure messages: every check that fails without a caller-supplied message
/// says what was expected of which argument — <c>Expected {name} to {expectation}, but found
/// {value}.</c> — instead of the framework's generic text. Also pins the two supporting behaviours:
/// a caller's message still replaces the default entirely, and the library's own frames stay out of
/// the stack trace so a failure points at the caller's check, not the plumbing.
/// </summary>
[ContractTest("FailureMessages")]
public class FailureMessageTests
{
    [Fact]
    public void OrderingCheckNamesOperandAndActualValue()
    {
        const int number = 3;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => number.Must().BeGreaterThan(10));

        exception.Message.Should().Be("Expected number to be greater than 10, but found 3. (Parameter 'number')");
    }

    [Fact]
    public void BetweenCheckNamesBothBounds()
    {
        const int number = 42;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => number.Must().BeBetween(5, 10));

        exception.Message.Should().Be("Expected number to be between 5 and 10, but found 42. (Parameter 'number')");
    }

    [Fact]
    public void EqualityCheckQuotesStrings()
    {
        const string text = "actual";

        var exception = Assert.Throws<ArgumentException>(() => text.Must().Be("expected"));

        exception.Message.Should().Be("Expected text to be \"expected\", but found \"actual\". (Parameter 'text')");
    }

    [Fact]
    public void AnyOfCheckListsTheCandidates()
    {
        const int number = 9;

        var exception = Assert.Throws<ArgumentException>(() => number.Must().BeAnyOf([1, 2, 3]));

        exception.Message.Should().Be("Expected number to be any of [1, 2, 3], but found 9. (Parameter 'number')");
    }

    [Fact]
    public void LongCollectionsAreTruncatedToFiveItems()
    {
        const int number = 9;

        var exception = Assert.Throws<ArgumentException>(
            () => number.Must().BeAnyOf([1, 2, 3, 4, 5, 6, 7]));

        exception.Message.Should().Be(
            "Expected number to be any of [1, 2, 3, 4, 5, …], but found 9. (Parameter 'number')");
    }

    [Fact]
    public void LongStringsAreTruncated()
    {
        var text = new string('x', 80);

        var exception = Assert.Throws<ArgumentException>(() => text.Must().Be("expected"));

        exception.Message.Should().Be(
            $"Expected text to be \"expected\", but found \"{new string('x', 64)}…\". (Parameter 'text')");
    }

    [Fact]
    public void NullCheckSaysNotBeNull()
    {
        string? text = null;

        var exception = Assert.Throws<ArgumentNullException>(() => text.Must().NotBeNull());

        exception.Message.Should().Be("Expected text to not be null. (Parameter 'text')");
    }

    /// <summary>
    /// Checks with no dedicated validator get their expectation from the check's own name, so a
    /// message exists without any call site spelling one out.
    /// </summary>
    [Fact]
    public void CheckNameBecomesTheExpectation()
    {
        const int number = 3;

        var exception = Assert.Throws<ArgumentException>(() => number.Must().BeEven());

        exception.Message.Should().Be("Expected number to be even, but found 3. (Parameter 'number')");
    }

    [Fact]
    public void CheckNameHumanisesMultipleWords()
    {
        const char letter = 'a';

        var exception = Assert.Throws<ArgumentException>(() => letter.Must().NotBeLowercase());

        exception.Message.Should().Be("Expected letter to not be lowercase, but found 'a'. (Parameter 'letter')");
    }

    /// <summary>
    /// Checks whose operand only the call site knows spell the expectation out instead of relying
    /// on the check name, which would drop the operand from the message.
    /// </summary>
    [Fact]
    public void OperandChecksSpellOutTheExpectation()
    {
        const string text = "actual";

        var exception = Assert.Throws<ArgumentException>(() => text.Must().StartWith("abc"));

        exception.Message.Should().Be("Expected text to start with \"abc\", but found \"actual\". (Parameter 'text')");
    }

    [Fact]
    public void ParsingChecksNameTheFormat()
    {
        const string email = "not-an-email";

        var exception = Assert.Throws<ArgumentException>(() => email.Must().BeEmailAddress());

        exception.Message.Should().Be(
            "Expected email to be a valid email address, but found \"not-an-email\". (Parameter 'email')");
    }

    [Fact]
    public void UniquenessCheckNamesTheDuplicate()
    {
        var items = new List<int> { 1, 2, 2 };

        var exception = Assert.Throws<ArgumentException>(() => items.Must().HaveUniqueItems());

        exception.Message.Should().Be(
            "Expected items to contain only unique items, but 2 appears more than once. (Parameter 'items')");
    }

    [Fact]
    public void DictionaryCheckNamesTheKey()
    {
        var map = new Dictionary<string, int> { { "present", 1 } };

        var exception = Assert.Throws<ArgumentException>(() => map.Must().ContainKey("missing"));

        exception.Message.Should().Be("Expected map to contain the key \"missing\". (Parameter 'map')");
    }

    [Fact]
    public void SatisfyDescribesTheConditionAsGiven()
    {
        const int number = 3;

        var exception = Assert.Throws<ArgumentException>(() => number.Must().Satisfy<int?>(n => n > 10));

        exception.Message.Should().Be(
            "Expected number to satisfy the given condition, but found 3. (Parameter 'number')");
    }

    [Fact]
    public void TypeCheckNamesExpectedAndActualType()
    {
        object value = 42;

        var exception = Assert.Throws<ArgumentException>(() => value.Must().BeOfType<string>());

        exception.Message.Should().Be(
            "Expected value to be of type System.String, but found System.Int32. (Parameter 'value')");
    }

    [Fact]
    public void NullIsDescribedExplicitly()
    {
        string? text = null;

        var exception = Assert.Throws<ArgumentException>(() => text.Must().Be("expected"));

        exception.Message.Should().Be("Expected text to be \"expected\", but found <null>. (Parameter 'text')");
    }

    /// <summary>
    /// The caller's message must keep replacing the default entirely, not be appended to it.
    /// </summary>
    [Fact]
    public void CallerSuppliedMessageReplacesTheDefault()
    {
        const int number = 3;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => number.Must().BeGreaterThan(10, "Quantity is too small"));

        exception.Message.Should().Be("Quantity is too small (Parameter 'number')");
    }

    /// <summary>
    /// The library's own frames are hidden, so the trace starts at the check the caller wrote
    /// rather than inside the validators. Needs a net6+ runtime; the test project targets net8.0.
    /// </summary>
    [Fact]
    public void StackTraceStartsAtTheCallersCheck()
    {
        const int number = 3;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => number.Must().BeGreaterThan(10));

        exception.StackTrace.Should().NotContain("FluentContracts.Validators");
        exception.StackTrace.Should().NotContain("ThrowHelper");
    }
}
