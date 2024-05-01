using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts;

public abstract class EqualityContract<TArgument, TContract> : NullableContract<TArgument, TContract>
    where TContract : EqualityContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

    protected EqualityContract(TArgument argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<TContract> Be(TArgument expectedValue, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<TContract> Be<TException>(TArgument expectedValue, string? message = null)
        where TException : Exception, new()
    {
        if (message != null)
            Validator.CheckForSpecificValue<TArgument, TException>(expectedValue, ArgumentValue, message);
        else
            Validator.CheckForSpecificValue<TArgument, TException>(expectedValue, ArgumentValue);

        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<TContract> NotBe(TArgument expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}