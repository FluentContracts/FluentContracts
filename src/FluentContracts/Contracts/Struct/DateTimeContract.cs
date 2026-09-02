using FluentContracts.Enums;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

/// <summary>
/// The entry point for checks on a <see cref="System.DateTime"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
/// <param name="dateTimeProvider">Supplies the current moment to the checks that need one. Defaults to the system clock.</param>
public class DateTimeContract(DateTime? argumentValue, string argumentName, IDateTimeProvider? dateTimeProvider = null)
    : DateTimeContract<DateTimeContract>(argumentValue, argumentName, dateTimeProvider);

/// <summary>
/// The inheritable contract for a <see cref="System.DateTime"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class DateTimeContract<TContract> : BaseContract<DateTime?, TContract>
    where TContract : DateTimeContract<TContract>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    /// <param name="dateTimeProvider">Supplies the current moment to the checks that need one. Defaults to the system clock.</param>
    protected DateTimeContract(
        DateTime? argumentValue,
        string argumentName,
        IDateTimeProvider? dateTimeProvider = null) : base(argumentValue, argumentName)
    {
        _dateTimeProvider = dateTimeProvider ?? new DotNetDateTimeProvider();
    }

    /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeNull(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeNull(string? message = null)
    {
        Validator.CheckForNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract Be(DateTime expectedValue, string? message = null)
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
    public TContract Be(DateTime? expectedValue, string? message = null)
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
    public TContract NotBe(DateTime expectedValue, string? message = null)
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
    public TContract NotBe(DateTime? expectedValue, string? message = null)
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
    public TContract BeAnyOf(DateTime expectedValue)
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
    public TContract BeAnyOf(IEnumerable<DateTime> expectedValues, string? message = null)
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
    public TContract NotBeAnyOf(DateTime unexpectedValue)
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
    public TContract NotBeAnyOf(IEnumerable<DateTime> unexpectedValues, string? message = null)
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
    public TContract BeBetween(DateTime start, DateTime end, string? message = null)
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
    public TContract BeBetween(DateTime? start, DateTime? end, string? message = null)
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
    public TContract BeGreaterThan(DateTime value, string? message = null)
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
    public TContract BeGreaterThan(DateTime? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be lower or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterOrEqualTo(DateTime value, string? message = null)
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
    public TContract BeGreaterOrEqualTo(DateTime? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is lower than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessThan(DateTime value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is lower than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessThan(DateTime? value, string? message = null)
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
    public TContract BeLessOrEqualTo(DateTime value, string? message = null)
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
    public TContract BeLessOrEqualTo(DateTime? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInDaylightSaving(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => a!.Value.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInDaylightSaving(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => !a!.Value.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLeapYear(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => DateTime.IsLeapYear(a!.Value.Year), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeLeapYear(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => !DateTime.IsLeapYear(a!.Value.Year), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInJanuary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.January, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInJanuary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.January, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInFebruary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.February, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInFebruary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.February, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInMarch(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.March, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInMarch(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.March, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInApril(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.April, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInApril(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.April, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInMay(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.May, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInMay(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.May, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInJune(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.June, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInJune(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.June, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInJuly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.July, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInJuly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.July, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInAugust(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.August, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInAugust(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.August, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInSeptember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.September, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInSeptember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.September, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInOctober(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.October, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInOctober(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.October, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInNovember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.November, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeInNovember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.November, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInDecember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(Month.December, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInDecember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(Month.December, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeUtc(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DateTimeKind.Utc, ArgumentValue.Value.Kind, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeUtc(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DateTimeKind.Utc, ArgumentValue.Value.Kind, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLocal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DateTimeKind.Local, ArgumentValue.Value.Kind, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeLocal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DateTimeKind.Local, ArgumentValue.Value.Kind, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeMonday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DayOfWeek.Monday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeMonday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DayOfWeek.Monday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Tuesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeTuesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DayOfWeek.Tuesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeTuesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DayOfWeek.Tuesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWednesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DayOfWeek.Wednesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWednesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DayOfWeek.Wednesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeThursday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DayOfWeek.Thursday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeThursday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DayOfWeek.Thursday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeFriday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DayOfWeek.Friday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeFriday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DayOfWeek.Friday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeSaturday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DayOfWeek.Saturday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeSaturday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DayOfWeek.Saturday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeSunday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(DayOfWeek.Sunday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeSunday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(DayOfWeek.Sunday, ArgumentValue.Value.DayOfWeek, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    } 
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is the same date as <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Specific date to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeOnDate(DateTime referenceDate, string? message = null)
    {
        referenceDate.Must().NotBeNull();
        
        return BeOnDate(referenceDate.Year, referenceDate.Month, referenceDate.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="day"/>, <paramref name="month"/> and <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="month">Specific month to match against</param>
    /// <param name="day">Specific day of the month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeOnDate(int year, int month, int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not the same date as <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Specific date to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeOnDate(DateTime referenceDate, string? message = null)
    {
        referenceDate.Must().NotBeNull();
        
        return NotBeOnDate(referenceDate.Year, referenceDate.Month, referenceDate.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="day"/>, <paramref name="month"/> and <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="month">Specific month to match against</param>
    /// <param name="day">Specific day of the month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeOnDate(int year, int month, int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForNotSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the past from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInThePast(string? message = null)
    {
        return BeInThePast(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the past from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInThePast(DateTime referenceDate, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessThan(referenceDate, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the past from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInThePast(string? message = null)
    {
        return BeInTheFuture(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the past from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInThePast(DateTime referenceDate, string? message = null)
    {
        return BeInTheFuture(referenceDate, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the future from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInTheFuture(string? message = null)
    {
        return BeInTheFuture(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the future from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInTheFuture(DateTime referenceDate, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterThan(referenceDate, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the future from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInTheFuture(string? message = null)
    {
        return BeInThePast(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the future from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInTheFuture(DateTime referenceDate, string? message = null)
    {
        return BeInThePast(referenceDate, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the same date as <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);

        var today = _dateTimeProvider.Today;
        
        Validator.CheckForSpecificValue(today.Year, ArgumentValue.Value.Year, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(today.Month, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(today.Day, ArgumentValue.Value.Day, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the same date as <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);

        var today = _dateTimeProvider.Today;
        
        Validator.CheckGenericCondition(
            a => 
                a.Day != today.Day 
                || a.Month != today.Month 
                || a.Year != today.Year,
            ArgumentValue.Value,
            ArgumentName,
            message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the next day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeTomorrow(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);

        var tomorrow = _dateTimeProvider.Today.AddDays(1);
        
        Validator.CheckForSpecificValue(tomorrow.Year, ArgumentValue.Value.Year, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(tomorrow.Month, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(tomorrow.Day, ArgumentValue.Value.Day, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the next day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeTomorrow(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);

        var tomorrow = _dateTimeProvider.Today.AddDays(1);
        
        Validator.CheckGenericCondition(
            a => 
                a.Day != tomorrow.Day 
                || a.Month != tomorrow.Month 
                || a.Year != tomorrow.Year,
            ArgumentValue.Value,
            ArgumentName,
            message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the previous day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeYesterday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);

        var yesterday = _dateTimeProvider.Today.AddDays(-1);
        
        Validator.CheckForSpecificValue(yesterday.Year, ArgumentValue.Value.Year, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(yesterday.Month, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(yesterday.Day, ArgumentValue.Value.Day, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the previous day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeYesterday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);

        var yesterday = _dateTimeProvider.Today.AddDays(-1);
        
        Validator.CheckGenericCondition(
            a => 
                a.Day != yesterday.Day 
                || a.Month != yesterday.Month 
                || a.Year != yesterday.Year,
            ArgumentValue.Value,
            ArgumentName,
            message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="month"/>
    /// </summary>
    /// <param name="month">Specific month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInMonth(int month, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        month.Must().BeBetween(1, 12);

        Validator.CheckForSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="month"/>
    /// </summary>
    /// <param name="month">Specific month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInMonth(int month, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        month.Must().BeBetween(1, 12);

        Validator.CheckForNotSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="day"/>
    /// </summary>
    /// <param name="day">Specific day to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeOnDay(int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        day.Must().BeBetween(1, 31);

        Validator.CheckForSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="day"/>
    /// </summary>
    /// <param name="day">Specific day to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeOnDay(int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        day.Must().BeBetween(1, 31);

        Validator.CheckForNotSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInYear(int year, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        year.Must().BeBetween(1, 9999);

        Validator.CheckForSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInYear(int year, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        year.Must().BeBetween(1, 9999);

        Validator.CheckForNotSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same day as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeOnCurrentDay(string? message = null)
    {
        return BeOnDay(_dateTimeProvider.Today.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same day as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeOnCurrentDay(string? message = null)
    {
        return NotBeOnDay(_dateTimeProvider.Today.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same month as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInCurrentMonth(string? message = null)
    {
        return BeInMonth(_dateTimeProvider.Today.Month, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same month as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInCurrentMonth(string? message = null)
    {
        return NotBeInMonth(_dateTimeProvider.Today.Month, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same year as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInCurrentYear(string? message = null)
    {
        return BeInYear(_dateTimeProvider.Today.Year, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same year as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInCurrentYear(string? message = null)
    {
        return NotBeInYear(_dateTimeProvider.Today.Year, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with the same day of year as <paramref name="dayOfYear"/>
    /// </summary>
    /// <param name="dayOfYear">Specific day of year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeOnDayOfYear(int dayOfYear, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        dayOfYear.Must().BeBetween(1, 366);

        Validator.CheckForSpecificValue(dayOfYear, ArgumentValue.Value.DayOfYear, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with the same day of year as <paramref name="dayOfYear"/>
    /// </summary>
    /// <param name="dayOfYear">Specific day of year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeOnDayOfYear(int dayOfYear, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        
        dayOfYear.Must().BeBetween(1, 366);

        Validator.CheckForNotSpecificValue(dayOfYear, ArgumentValue.Value.DayOfYear, ArgumentName, message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date is during the weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWeekend(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            date => 
                date!.Value.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday, 
            ArgumentValue, 
            ArgumentName, 
            message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date is during the weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWeekend(string? message = null)
    {
        return BeWeekday(message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date during the weekday (Monday through Friday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWeekday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            date => 
                date!.Value.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday, 
            ArgumentValue, 
            ArgumentName, 
            message ?? ChainMessage);
        
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date during the weekday (Monday through Friday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWeekday(string? message = null)
    {
        return BeWeekend(message);
    }
}
