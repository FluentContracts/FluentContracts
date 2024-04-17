namespace FluentContracts.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a condition on an argument is not satisfied.
    /// </summary>
    /// <remarks>
    /// This exception is thrown by the FluentContracts library when a condition specified in a contract is not satisfied.
    /// It is derived from the <see cref="ArgumentException"/> class.
    /// </remarks>
    public class ArgumentConditionNotSatisfiedException(string? paramName, string? message)
        : ArgumentException(message, paramName);
}
