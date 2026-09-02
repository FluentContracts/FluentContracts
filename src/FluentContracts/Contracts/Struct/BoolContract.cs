using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

/// <summary>
/// The entry point for checks on a <see cref="bool"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class BoolContract(bool? argumentValue, string argumentName)
    : BoolContract<BoolContract>(argumentValue, argumentName);

/// <summary>
/// The inheritable contract for a <see cref="bool"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class BoolContract<TContract> : ObjectContract<bool?, TContract>
    where TContract : BoolContract<TContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected BoolContract(bool? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract Be(bool expectedValue, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract Be(bool? expectedValue, string? message = null)
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
    public TContract NotBe(bool expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    } 

    /// <summary>
    /// Checks if the specified argument is not equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBe(bool? expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    } 
    
    /// <summary>
    /// Checks if the specified argument is the expected value.
    /// </summary>
    /// <param name="expectedValue">The only value the argument may be.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeAnyOf(bool expectedValue)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForAnyOf([expectedValue], ArgumentValue.Value, ArgumentName, null);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeAnyOf(IEnumerable<bool> expectedValues, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForAnyOf(expectedValues, ArgumentValue.Value, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is not the given value.
    /// </summary>
    /// <param name="unexpectedValue">The value the argument must not be.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeAnyOf(bool unexpectedValue)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotAnyOf([unexpectedValue], ArgumentValue.Value, ArgumentName, null);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the given values.
    /// </summary>
    /// <param name="unexpectedValues">The values the argument must not be.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeAnyOf(IEnumerable<bool> unexpectedValues, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotAnyOf(unexpectedValues, ArgumentValue.Value, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="bool"/> argument is "true".
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeTrue(string? message = null)
    {
        Validator.CheckForSpecificValue(true, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="bool"/> argument "false".
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeFalse(string? message = null)
    {
        Validator.CheckForSpecificValue(false, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
}
