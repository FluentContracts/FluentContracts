using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Numeric;

/// <summary>
/// The entry point for checks on a <see cref="double"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class DoubleContract(double? argumentValue, string argumentName)
    : DoubleContract<DoubleContract>(argumentValue, argumentName);

/// <summary>
/// The inheritable contract for a <see cref="double"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class DoubleContract<TContract> : ObjectContract<double?, TContract>
    where TContract : DoubleContract<TContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected DoubleContract(double? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }
    
    /// <summary>
    /// Checks if the value of the argument is greater than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BePositive(string? message = null)
    {
        Validator.CheckForGreaterThan(0d, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the argument is not greater than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBePositive(string? message = null)
    {
        Validator.CheckForLessOrEqualTo(0d, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the argument is less than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeNegative(string? message = null)
    {
        Validator.CheckForLessThan(0d, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the argument is not less than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeNegative(string? message = null)
    {
        Validator.CheckForGreaterOrEqualTo(0d, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract Be(double expectedValue, string? message = null)
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
    public TContract Be(double? expectedValue, string? message = null)
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
    public TContract NotBe(double expectedValue, string? message = null)
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
    public TContract NotBe(double? expectedValue, string? message = null)
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
    public TContract BeAnyOf(double expectedValue)
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
    public TContract BeAnyOf(IEnumerable<double> expectedValues, string? message = null)
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
    public TContract NotBeAnyOf(double unexpectedValue)
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
    public TContract NotBeAnyOf(IEnumerable<double> unexpectedValues, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotAnyOf(unexpectedValues, ArgumentValue.Value, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is inclusively between the values of <paramref name="start"/> and <paramref name="end"/>
    /// </summary>
    /// <param name="start">Value that must be less or equal to the argument</param>
    /// <param name="end">Value that must be greater or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeBetween(double start, double end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is inclusively between the values of <paramref name="start"/> and <paramref name="end"/>
    /// </summary>
    /// <param name="start">Value that must be less or equal to the argument</param>
    /// <param name="end">Value that must be greater or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeBetween(double? start, double? end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be less than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterThan(double value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be less than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterThan(double? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterOrEqualTo(double value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterOrEqualTo(double? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is less than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessThan(double value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is less than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessThan(double? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessOrEqualTo(double value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessOrEqualTo(double? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the argument is equal to zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeZero(string? message = null)
    {
        Validator.CheckForSpecificValue(0d, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the argument is not equal to zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeZero(string? message = null)
    {
        Validator.CheckForNotSpecificValue(0d, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is not a number.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeNaN(string? message = null)
    {
        Validator.CheckForNaN(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is a number.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeNaN(string? message = null)
    {
        Validator.CheckForNotNaN(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is positive or negative infinity.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeInfinity(string? message = null)
    {
        Validator.CheckForInfinity(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is neither positive nor negative infinity.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeInfinity(string? message = null)
    {
        Validator.CheckForNotInfinity(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is a finite number: neither NaN nor infinite.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>
    /// This is the check to reach for before an ordering comparison, which rejects NaN outright.
    /// </remarks>
    public TContract BeFinite(string? message = null)
    {
        Validator.CheckForFinite(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is not finite: either NaN or infinite.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeFinite(string? message = null)
    {
        Validator.CheckForNotFinite(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
}
