using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;


public class NullableContract<TArgument>(TArgument? argumentValue, string argumentName)
    : NullableContract<TArgument, NullableContract<TArgument>>(argumentValue, argumentName);

public abstract class NullableContract<TArgument, TContract> : BaseContract<TArgument, TContract>
    where TContract : NullableContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

    protected NullableContract(TArgument? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeNull(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeNull<TException>(string? message = null)
        where TException : Exception, new()
    {
        if (message != null)
            Validator.CheckForNotNull<TArgument, TException>(ArgumentValue, message);
        else
            Validator.CheckForNotNull<TArgument, TException>(ArgumentValue);

        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeNull(string? message = null)
    {
        Validator.CheckForNull(ArgumentValue, ArgumentName, message);
        return _linker;
    }
}