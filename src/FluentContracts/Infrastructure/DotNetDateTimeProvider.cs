namespace FluentContracts.Infrastructure;

public class DotNetDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime Today => DateTime.Today;
}