namespace FluentContracts.Infrastructure;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime Today { get; }
}