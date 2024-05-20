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
    public Linker<DateTimeContract> BeInDaylightSaving(string? message = null)
    {
        Validator.CheckGenericCondition(a => a.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInDaylightSaving(string? message = null)
    {
        Validator.CheckGenericCondition(a => !a.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeLeapYear(string? message = null)
    {
        Validator.CheckGenericCondition(a => DateTime.IsLeapYear(a.Year), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeLeapYear(string? message = null)
    {
        Validator.CheckGenericCondition(a => !DateTime.IsLeapYear(a.Year), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInJanuary(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.January, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInJanuary(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.January, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInFebruary(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.February, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInFebruary(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.February, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInMarch(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.March, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInMarch(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.March, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInApril(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.April, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInApril(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.April, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInMay(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.May, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInMay(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.May, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInJune(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.June, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInJune(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.June, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInJuly(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.July, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInJuly(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.July, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInAugust(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.August, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInAugust(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.August, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInSeptember(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.September, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInSeptember(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.September, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInOctober(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.October, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInOctober(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.October, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInNovember(string? message = null)
    {
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
        Validator.CheckForNotSpecificValue(Month.November, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeInDecember(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.December, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeInDecember(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.December, ArgumentValue.Month, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeUtc(string? message = null)
    {
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
        Validator.CheckForNotSpecificValue(DateTimeKind.Utc, ArgumentValue.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeLocal(string? message = null)
    {
        Validator.CheckForSpecificValue(DateTimeKind.Local, ArgumentValue.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeLocal(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DateTimeKind.Local, ArgumentValue.Kind, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeMonday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Monday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeMonday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Monday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Tuesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeTuesday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Tuesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeTuesday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Tuesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeWednesday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Wednesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeWednesday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Wednesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeThursday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Thursday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeThursday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Thursday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeFriday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Friday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeFriday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Friday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeSaturday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Saturday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeSaturday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Saturday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeSunday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Sunday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeSunday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Sunday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on date with specific <paramref name="day"/>, <paramref name="month"/> and <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="month">Specific month to match against</param>
    /// <param name="day">Specific day of the month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> BeOnDate(int year, int month, int day, string? message = null)
    {
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForSpecificValue(year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForSpecificValue(month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForSpecificValue(day, ArgumentValue.Day, ArgumentName, message);
        
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on date with specific <paramref name="day"/>, <paramref name="month"/> and <paramref name="year"/>
    /// </summary>
    /// <param name="year">Specific year to match against</param>
    /// <param name="month">Specific month to match against</param>
    /// <param name="day">Specific day of the month to match against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<DateTimeContract> NotBeOnDate(int year, int month, int day, string? message = null)
    {
        year.Must().BeBetween(1, 9999);
        month.Must().BeBetween(1, 12);
        day.Must().BeBetween(1, 31);

        Validator.CheckForNotSpecificValue(year, ArgumentValue.Year, ArgumentName, message);
        Validator.CheckForNotSpecificValue(month, ArgumentValue.Month, ArgumentName, message);
        Validator.CheckForNotSpecificValue(day, ArgumentValue.Day, ArgumentName, message);
        return _linker;
    }
}