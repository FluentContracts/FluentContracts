using FluentContracts.Enums;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

public class DateTimeContract(DateTime? argumentValue, string argumentName, IDateTimeProvider? dateTimeProvider = null)
    : DateTimeContract<DateTimeContract>(argumentValue, argumentName, dateTimeProvider);

public class DateTimeContract<TContract> : BaseContract<DateTime?, TContract>
    where TContract : DateTimeContract<TContract>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Linker<TContract> _linker;

    protected DateTimeContract(
        DateTime? argumentValue,
        string argumentName,
        IDateTimeProvider? dateTimeProvider = null) : base(argumentValue, argumentName)
    {
        _dateTimeProvider = dateTimeProvider ?? new DotNetDateTimeProvider();
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeNull(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeNull(string? message = null)
    {
        Validator.CheckForNull(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Be(DateTime expectedValue, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Be(DateTime? expectedValue, string? message = null)
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
    public Linker<TContract> NotBe(DateTime expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 

    /// <summary>
    /// Checks if the specified argument is not equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBe(DateTime? expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params DateTime[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params DateTime?[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeAnyOf(string? message, params DateTime[] expectedValues)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForAnyOf(expectedValues, ArgumentValue.Value, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(string? message, params DateTime?[] expectedValues)
    {
        Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params DateTime[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params DateTime?[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeAnyOf(string? message, params DateTime[] expectedValues)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotAnyOf(expectedValues, ArgumentValue.Value, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(string? message, params DateTime?[] expectedValues)
    {
        Validator.CheckForNotAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is inclusively between the values of <see cref="start"/> and <see cref="end"/>
    /// </summary>
    /// <param name="start">Value that must be less or equal to the argument</param>
    /// <param name="end">Value that must be greater or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeBetween(DateTime start, DateTime end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the value of the argument is inclusively between the values of <see cref="start"/> and <see cref="end"/>
    /// </summary>
    /// <param name="start">Value that must be less or equal to the argument</param>
    /// <param name="end">Value that must be greater or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeBetween(DateTime? start, DateTime? end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterThan(DateTime value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterThan(DateTime? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be lower or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterOrEqualTo(DateTime value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterOrEqualTo(DateTime? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessThan(DateTime value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessThan(DateTime? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessOrEqualTo(DateTime value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessOrEqualTo(DateTime? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInDaylightSaving(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInDaylightSaving(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a!.Value.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLeapYear(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => DateTime.IsLeapYear(a!.Value.Year), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeLeapYear(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !DateTime.IsLeapYear(a!.Value.Year), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInJanuary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.January, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInJanuary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.January, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInFebruary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.February, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInFebruary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.February, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInMarch(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.March, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInMarch(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.March, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInApril(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.April, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInApril(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.April, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInMay(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.May, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInMay(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.May, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInJune(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.June, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInJune(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.June, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInJuly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.July, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInJuly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.July, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInAugust(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.August, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInAugust(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.August, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInSeptember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.September, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInSeptember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.September, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInOctober(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.October, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInOctober(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.October, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInNovember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.November, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeInNovember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.November, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInDecember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.December, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInDecember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.December, ArgumentValue.Value.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeUtc(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DateTimeKind.Utc, ArgumentValue.Value.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeUtc(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DateTimeKind.Utc, ArgumentValue.Value.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLocal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DateTimeKind.Local, ArgumentValue.Value.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeLocal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DateTimeKind.Local, ArgumentValue.Value.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeMonday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Monday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeMonday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Monday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Tuesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeTuesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Tuesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeTuesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Tuesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeWednesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Wednesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeWednesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Wednesday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeThursday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Thursday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeThursday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Thursday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeFriday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Friday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeFriday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Friday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeSaturday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Saturday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeSaturday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Saturday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeSunday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Sunday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeSunday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Sunday, ArgumentValue.Value.DayOfWeek, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is the same date as <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Specific date to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeOnDate(DateTime referenceDate, string? message = null)
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
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeOnDate(int year, int month, int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message);
        
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not the same date as <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Specific date to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeOnDate(DateTime referenceDate, string? message = null)
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
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeOnDate(int year, int month, int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForNotSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message);
        Validator.CheckForNotSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message);
        Validator.CheckForNotSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the past from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInThePast(string? message = null)
    {
        return BeInThePast(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the past from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInThePast(DateTime referenceDate, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(referenceDate, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the past from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInThePast(string? message = null)
    {
        return BeInTheFuture(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the past from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInThePast(DateTime referenceDate, string? message = null)
    {
        return BeInTheFuture(referenceDate, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the future from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInTheFuture(string? message = null)
    {
        return BeInTheFuture(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the future from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInTheFuture(DateTime referenceDate, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(referenceDate, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the future from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInTheFuture(string? message = null)
    {
        return BeInThePast(_dateTimeProvider.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the future from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInTheFuture(DateTime referenceDate, string? message = null)
    {
        return BeInThePast(referenceDate, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the same date as <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var today = _dateTimeProvider.Today;
        
        Validator.CheckForSpecificValue(today.Year, ArgumentValue.Value.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(today.Month, ArgumentValue.Value.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(today.Day, ArgumentValue.Value.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the same date as <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var today = _dateTimeProvider.Today;
        
        Validator.CheckGenericCondition(
            a => 
                a.Day != today.Day 
                || a.Month != today.Month 
                || a.Year != today.Year,
            ArgumentValue.Value,
            ArgumentName,
            message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the next day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeTomorrow(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var tomorrow = _dateTimeProvider.Today.AddDays(1);
        
        Validator.CheckForSpecificValue(tomorrow.Year, ArgumentValue.Value.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(tomorrow.Month, ArgumentValue.Value.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(tomorrow.Day, ArgumentValue.Value.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the next day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeTomorrow(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var tomorrow = _dateTimeProvider.Today.AddDays(1);
        
        Validator.CheckGenericCondition(
            a => 
                a.Day != tomorrow.Day 
                || a.Month != tomorrow.Month 
                || a.Year != tomorrow.Year,
            ArgumentValue.Value,
            ArgumentName,
            message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the previous day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeYesterday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var yesterday = _dateTimeProvider.Today.AddDays(-1);
        
        Validator.CheckForSpecificValue(yesterday.Year, ArgumentValue.Value.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(yesterday.Month, ArgumentValue.Value.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(yesterday.Day, ArgumentValue.Value.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the previous day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeYesterday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var yesterday = _dateTimeProvider.Today.AddDays(-1);
        
        Validator.CheckGenericCondition(
            a => 
                a.Day != yesterday.Day 
                || a.Month != yesterday.Month 
                || a.Year != yesterday.Year,
            ArgumentValue.Value,
            ArgumentName,
            message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="month"/>
    /// </summary>
    /// <param name="month">Specific month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInMonth(int month, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        month.Must().BeBetween(1, 12);

        Validator.CheckForSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="month"/>
    /// </summary>
    /// <param name="month">Specific month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInMonth(int month, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        month.Must().BeBetween(1, 12);

        Validator.CheckForNotSpecificValue(month, ArgumentValue.Value.Month, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="day"/>
    /// </summary>
    /// <param name="day">Specific day to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeOnDay(int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        day.Must().BeBetween(1, 31);

        Validator.CheckForSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="day"/>
    /// </summary>
    /// <param name="day">Specific day to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeOnDay(int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        day.Must().BeBetween(1, 31);

        Validator.CheckForNotSpecificValue(day, ArgumentValue.Value.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInYear(int year, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);

        Validator.CheckForSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInYear(int year, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);

        Validator.CheckForNotSpecificValue(year, ArgumentValue.Value.Year, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same day as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeOnCurrentDay(string? message = null)
    {
        return BeOnDay(_dateTimeProvider.Today.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same day as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeOnCurrentDay(string? message = null)
    {
        return NotBeOnDay(_dateTimeProvider.Today.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same month as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInCurrentMonth(string? message = null)
    {
        return BeInMonth(_dateTimeProvider.Today.Month, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same month as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInCurrentMonth(string? message = null)
    {
        return NotBeInMonth(_dateTimeProvider.Today.Month, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same year as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeInCurrentYear(string? message = null)
    {
        return BeInYear(_dateTimeProvider.Today.Year, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same year as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeInCurrentYear(string? message = null)
    {
        return NotBeInYear(_dateTimeProvider.Today.Year, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with the same day of year as <paramref name="dayOfYear"/>
    /// </summary>
    /// <param name="dayOfYear">Specific day of year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeOnDayOfYear(int dayOfYear, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        dayOfYear.Must().BeBetween(1, 366);

        Validator.CheckForSpecificValue(dayOfYear, ArgumentValue.Value.DayOfYear, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with the same day of year as <paramref name="dayOfYear"/>
    /// </summary>
    /// <param name="dayOfYear">Specific day of year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeOnDayOfYear(int dayOfYear, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        dayOfYear.Must().BeBetween(1, 366);

        Validator.CheckForNotSpecificValue(dayOfYear, ArgumentValue.Value.DayOfYear, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date is during the weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeWeekend(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            date => 
                date!.Value.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday, 
            ArgumentValue, 
            ArgumentName, 
            message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date is during the weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeWeekend(string? message = null)
    {
        return BeWeekday(message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date during the weekday (Monday through Friday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeWeekday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            date => 
                date!.Value.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday, 
            ArgumentValue, 
            ArgumentName, 
            message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date during the weekday (Monday through Friday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeWeekday(string? message = null)
    {
        return BeWeekend(message);
    }
}