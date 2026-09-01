using System;
using System.Collections.Generic;
using System.Linq;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

/// <summary>
/// Adds the checks that compare the argument against expected values: <c>Be</c>, <c>NotBe</c>,
/// <c>BeAnyOf</c> and <c>NotBeAnyOf</c>. Unlike the ordering comparisons further down the
/// hierarchy, these accept a null argument.
/// </summary>
/// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public abstract class EqualityContract<TArgument, TContract> : ObjectContract<TArgument, TContract>
    where TContract : EqualityContract<TArgument, TContract>
{
    const string ObsoleteMessageFirst =
        "Passing the message before the values binds wrongly when the argument is a string: "
        + "BeAnyOf(\"a\", \"b\") takes \"a\" as the message and checks only against \"b\". "
        + "Use BeAnyOf(IEnumerable<T> values, string? message) instead. This overload is removed in 4.0.0.";

    private readonly Linker<TContract> _linker;

    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected EqualityContract(TArgument argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Be(TArgument expectedValue, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBe(TArgument expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params TArgument[] expectedValues)
    {
        Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, null);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is the expected value.
    /// </summary>
    /// <param name="expectedValue">The only value the argument may be.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>
    /// Declared separately from the <c>params</c> overload so that a single <see cref="string"/> binds
    /// here. Without it, <c>BeAnyOf("a")</c> on a string argument matched the message overload below
    /// and checked against an empty set, which no argument can be a member of.
    /// </remarks>
    public Linker<TContract> BeAnyOf(TArgument expectedValue)
    {
        Validator.CheckForAnyOf([expectedValue], ArgumentValue, ArgumentName, null);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(IEnumerable<TArgument> expectedValues, string? message = null)
    {
        Validator.CheckForAnyOf(expectedValues.ToArray(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Obsolete(ObsoleteMessageFirst)]
    public Linker<TContract> BeAnyOf(string? message, params TArgument[] expectedValues)
    {
        Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params TArgument[] expectedValues)
    {
        Validator.CheckForNotAnyOf(expectedValues, ArgumentValue, ArgumentName, null);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not the given value.
    /// </summary>
    /// <param name="unexpectedValue">The value the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>
    /// Declared separately from the <c>params</c> overload so that a single <see cref="string"/> binds
    /// here. Without it, <c>NotBeAnyOf("a")</c> on a string argument matched the message overload below
    /// and checked against an empty set, which nothing is a member of, so the check always passed.
    /// </remarks>
    public Linker<TContract> NotBeAnyOf(TArgument unexpectedValue)
    {
        Validator.CheckForNotAnyOf([unexpectedValue], ArgumentValue, ArgumentName, null);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="unexpectedValues">The values the argument must not be.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(IEnumerable<TArgument> unexpectedValues, string? message = null)
    {
        Validator.CheckForNotAnyOf(unexpectedValues.ToArray(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Obsolete(ObsoleteMessageFirst)]
    public Linker<TContract> NotBeAnyOf(string? message, params TArgument[] expectedValues)
    {
        Validator.CheckForNotAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}
