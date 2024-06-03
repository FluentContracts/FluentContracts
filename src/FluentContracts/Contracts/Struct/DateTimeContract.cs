using FluentContracts.Enums;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

public class NullableDateTimeContract(DateTime? argumentValue, string argumentName)
    : ComparableContract<DateTime?, NullableDateTimeContract>(argumentValue, argumentName) {}

public class DateTimeContract : ComparableContract<DateTime, DateTimeContract>
{
    private readonly Linker<DateTimeContract> _linker;

    public DateTimeContract(DateTime argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<DateTimeContract>(this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInDaylightSaving(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInDaylightSaving(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeLeapYear(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => DateTime.IsLeapYear(a.Year), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeLeapYear(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !DateTime.IsLeapYear(a.Year), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInJanuary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.January, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInJanuary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.January, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInFebruary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.February, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInFebruary(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.February, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInMarch(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.March, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInMarch(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.March, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInApril(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.April, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInApril(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.April, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInMay(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.May, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInMay(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.May, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInJune(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.June, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInJune(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.June, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInJuly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.July, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInJuly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.July, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInAugust(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.August, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInAugust(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.August, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInSeptember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.September, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInSeptember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.September, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInOctober(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.October, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInOctober(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.October, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInNovember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.November, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInNovember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.November, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInDecember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Month.December, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInDecember(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Month.December, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeUtc(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DateTimeKind.Utc, ArgumentValue.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeUtc(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DateTimeKind.Utc, ArgumentValue.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeLocal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DateTimeKind.Local, ArgumentValue.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeLocal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DateTimeKind.Local, ArgumentValue.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeMonday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Monday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeMonday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Monday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Tuesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeTuesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Tuesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeTuesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Tuesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeWednesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Wednesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeWednesday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Wednesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeThursday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Thursday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeThursday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Thursday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeFriday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Friday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeFriday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Friday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeSaturday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Saturday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeSaturday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Saturday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeSunday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(DayOfWeek.Sunday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeSunday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(DayOfWeek.Sunday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is the same date as <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Specific date to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeOnDate(DateTime referenceDate, string? message = null)
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
    public Linker<DateTimeContract> BeOnDate(int year, int month, int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForSpecificValue(year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not the same date as <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Specific date to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeOnDate(DateTime referenceDate, string? message = null)
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
    public Linker<DateTimeContract> NotBeOnDate(int year, int month, int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForNotSpecificValue(year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForNotSpecificValue(month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForNotSpecificValue(day, ArgumentValue.Day, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the past from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInThePast(string? message = null)
    {
        return BeInThePast(DateTime.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the past from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInThePast(DateTime referenceDate, string? message = null)
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
    public Linker<DateTimeContract> NotBeInThePast(string? message = null)
    {
        return NotBeInThePast(DateTime.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the past from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInThePast(DateTime referenceDate, string? message = null)
    {
        return BeInTheFuture(referenceDate, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the future from <see cref="DateTime.Now"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInTheFuture(string? message = null)
    {
        return BeInTheFuture(DateTime.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in the future from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInTheFuture(DateTime referenceDate, string? message = null)
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
    public Linker<DateTimeContract> NotBeInTheFuture(string? message = null)
    {
        return BeInTheFuture(DateTime.Now, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in the future from <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInTheFuture(DateTime referenceDate, string? message = null)
    {
        return BeInThePast(referenceDate, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the same date as <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var today = DateTime.Today;
        
        Validator.CheckForSpecificValue(today.Year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(today.Month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(today.Day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the same date as <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var today = DateTime.Today;
        
        Validator.CheckForNotSpecificValue(today.Year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForNotSpecificValue(today.Month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForNotSpecificValue(today.Day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the next day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeTomorrow(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var tomorrow = DateTime.Today.AddDays(1);
        
        Validator.CheckForSpecificValue(tomorrow.Year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(tomorrow.Month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(tomorrow.Day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the next day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeTomorrow(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var tomorrow = DateTime.Today.AddDays(1);
        
        Validator.CheckForNotSpecificValue(tomorrow.Year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForNotSpecificValue(tomorrow.Month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForNotSpecificValue(tomorrow.Day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on the previous day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeYesterday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var yesterday = DateTime.Today.AddDays(-1);
        
        Validator.CheckForSpecificValue(yesterday.Year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(yesterday.Month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(yesterday.Day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on the previous day from <see cref="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeYesterday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);

        var yesterday = DateTime.Today.AddDays(-1);
        
        Validator.CheckForNotSpecificValue(yesterday.Year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForNotSpecificValue(yesterday.Month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForNotSpecificValue(yesterday.Day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="month"/>
    /// </summary>
    /// <param name="month">Specific month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInMonth(int month, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        month.Must().BeBetween(1, 12);

        Validator.CheckForSpecificValue(month, ArgumentValue.Month, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="month"/>
    /// </summary>
    /// <param name="month">Specific month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInMonth(int month, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        month.Must().BeBetween(1, 12);

        Validator.CheckForNotSpecificValue(month, ArgumentValue.Month, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="day"/>
    /// </summary>
    /// <param name="day">Specific day to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeOnDay(int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        day.Must().BeBetween(1, 31);

        Validator.CheckForSpecificValue(day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="day"/>
    /// </summary>
    /// <param name="day">Specific day to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeOnDay(int day, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        day.Must().BeBetween(1, 31);

        Validator.CheckForNotSpecificValue(day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInYear(int year, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);

        Validator.CheckForSpecificValue(year, ArgumentValue.Year, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInYear(int year, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        year.Must().BeBetween(1, 9999);

        Validator.CheckForNotSpecificValue(year, ArgumentValue.Year, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same day as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeOnCurrentDay(string? message = null)
    {
        return BeOnDay(DateTime.Today.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same day as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeOnCurrentDay(string? message = null)
    {
        return NotBeOnDay(DateTime.Today.Day, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same month as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInCurrentMonth(string? message = null)
    {
        return BeInMonth(DateTime.Today.Month, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same month as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInCurrentMonth(string? message = null)
    {
        return NotBeInMonth(DateTime.Today.Month, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with same year as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeInCurrentYear(string? message = null)
    {
        return BeInYear(DateTime.Today.Year, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with same year as <see name="DateTime.Today"/>
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeInCurrentYear(string? message = null)
    {
        return NotBeInYear(DateTime.Today.Year, message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with the same day of year as <paramref name="dayOfYear"/>
    /// </summary>
    /// <param name="dayOfYear">Specific day of year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeOnDayOfYear(int dayOfYear, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        dayOfYear.Must().BeBetween(1, 366);

        Validator.CheckForSpecificValue(dayOfYear, ArgumentValue.DayOfYear, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with the same day of year as <paramref name="dayOfYear"/>
    /// </summary>
    /// <param name="dayOfYear">Specific day of year to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> NotBeOnDayOfYear(int dayOfYear, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        
        dayOfYear.Must().BeBetween(1, 366);

        Validator.CheckForNotSpecificValue(dayOfYear, ArgumentValue.DayOfYear, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date is during the weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeWeekend(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            date => 
                date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday, 
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
    public Linker<DateTimeContract> NotBeWeekend(string? message = null)
    {
        return BeWeekday(message);
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date during the weekday (Monday through Friday)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DateTimeContract> BeWeekday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            date => 
                date.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday, 
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
    public Linker<DateTimeContract> NotBeWeekday(string? message = null)
    {
        return BeWeekend(message);
    }
}