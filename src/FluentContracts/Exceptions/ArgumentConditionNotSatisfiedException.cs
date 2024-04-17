namespace FluentContracts.Exceptions
{
    public class ArgumentConditionNotSatisfiedException(string? paramName, string? message)
        : ArgumentException(message, paramName);
}
