using System.Diagnostics.Contracts;
using FluentContracts.Enums;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts;

public class NullableDateTimeContract(DateTime? argumentValue, string argumentName)
    : ComparableContract<DateTime?>(argumentValue, argumentName) {}

public class DateTimeContract(DateTime argumentValue, string argumentName)
    : ComparableContract<DateTime>(argumentValue, argumentName)
{
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInDaylightSaving(string? message = null)
    {
        Validator.CheckGenericCondition(a => a.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in daylight saving time for its time zone.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInDaylightSaving(string? message = null)
    {
        Validator.CheckGenericCondition(a => !a.IsDaylightSavingTime(), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeLeapYear(string? message = null)
    {
        Validator.CheckGenericCondition(a => DateTime.IsLeapYear(a.Year), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during a leap year.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeLeapYear(string? message = null)
    {
        Validator.CheckGenericCondition(a => !DateTime.IsLeapYear(a.Year), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInJanuary(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.January, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during January.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInJanuary(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.January, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInFebruary(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.February, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during February.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInFebruary(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.February, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
       
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInMarch(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.March, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during March.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInMarch(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.March, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
        
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInApril(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.April, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during April.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInApril(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.April, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
        
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInMay(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.May, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during May.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInMay(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.May, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
        
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInJune(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.June, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during June.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInJune(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.June, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
            
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInJuly(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.July, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during July.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInJuly(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.July, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
                
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInAugust(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.August, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during August.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInAugust(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.August, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
                
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInSeptember(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.September, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during September.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInSeptember(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.September, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
                
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInOctober(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.October, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during October.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInOctober(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.October, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
               
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInNovember(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.November, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during November.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInNovember(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.November, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
               
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeInDecember(string? message = null)
    {
        Validator.CheckForSpecificValue(Month.December, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not during December.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeInDecember(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Month.December, ArgumentValue.Month, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeUtc(string? message = null)
    {
        Validator.CheckForSpecificValue(DateTimeKind.Utc, ArgumentValue.Kind, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeUtc(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DateTimeKind.Utc, ArgumentValue.Kind, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeLocal(string? message = null)
    {
        Validator.CheckForSpecificValue(DateTimeKind.Local, ArgumentValue.Kind, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not in `Local` date time kind.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeLocal(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DateTimeKind.Local, ArgumentValue.Kind, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeMonday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Monday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeMonday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Monday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Tuesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeTuesday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Tuesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Monday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeTuesday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Tuesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeWednesday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Wednesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Wednesday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeWednesday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Wednesday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeThursday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Thursday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Thursday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeThursday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Thursday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeFriday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Friday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Friday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeFriday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Friday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeSaturday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Saturday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Saturday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeSaturday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Saturday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> BeSunday(string? message = null)
    {
        Validator.CheckForSpecificValue(DayOfWeek.Sunday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="DateTime"/> is not on Sunday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<DateTime> NotBeSunday(string? message = null)
    {
        Validator.CheckForNotSpecificValue(DayOfWeek.Sunday, ArgumentValue.DayOfWeek, ArgumentName, message);
        return Linker;
    }
}