namespace FluentContracts.Infrastructure;

/// <summary>
/// Supplies the current moment to the date and time checks. Passing an implementation to
/// <c>Must()</c> is what lets a test decide what "now" means, instead of reading the system clock.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>The current date and time.</summary>
    DateTime Now { get; }

    /// <summary>The current date, with the time of day at midnight.</summary>
    DateTime Today { get; }
}
