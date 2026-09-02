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
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected EqualityContract(TArgument argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract Be(TArgument expectedValue, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is not equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBe(TArgument expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    } 
    
    /// <summary>
    /// Checks if the specified argument is the expected value.
    /// </summary>
    /// <param name="expectedValue">The only value the argument may be.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeAnyOf(TArgument expectedValue)
    {
        Validator.CheckForAnyOf([expectedValue], ArgumentValue, ArgumentName, null);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeAnyOf(IEnumerable<TArgument> expectedValues, string? message = null)
    {
        Validator.CheckForAnyOf(expectedValues.ToArray(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is not the given value.
    /// </summary>
    /// <param name="unexpectedValue">The value the argument must not be.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeAnyOf(TArgument unexpectedValue)
    {
        Validator.CheckForNotAnyOf([unexpectedValue], ArgumentValue, ArgumentName, null);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="unexpectedValues">The values the argument must not be.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeAnyOf(IEnumerable<TArgument> unexpectedValues, string? message = null)
    {
        Validator.CheckForNotAnyOf(unexpectedValues.ToArray(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

}
