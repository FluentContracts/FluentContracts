namespace FluentContracts.Infrastructure;

/// <summary>
/// The default <see cref="IDateTimeProvider"/>, reading the system clock. Used whenever a check is
/// not given a provider of its own.
/// </summary>
public class DotNetDateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime Now => DateTime.Now;

    /// <inheritdoc />
    public DateTime Today => DateTime.Today;
}
