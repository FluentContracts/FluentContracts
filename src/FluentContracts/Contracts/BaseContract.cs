using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

public abstract class BaseContract<TArgument, TContract>
    where TContract : BaseContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

    protected BaseContract(TArgument? argumentValue, string argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }

    protected TArgument? ArgumentValue { get; }
    protected string ArgumentName { get; }

    /// <summary>
    /// Checks if the specified argument satisfies a custom condition.
    /// </summary>
    /// <param name="customCondition">The custom condition to check.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> Satisfy<T>(Func<T, bool> customCondition, string? message = null)
        where T : TArgument
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(customCondition, (T)ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument satisfies a custom condition.
    /// </summary>
    /// <param name="customCondition">The custom condition to check.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> Satisfy<T, TException>(Func<T, bool> customCondition)
        where TException : Exception, new()
        where T : TArgument
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckGenericCondition<T, TException>(customCondition, (T)ArgumentValue);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument satisfies a custom condition.
    /// </summary>
    /// <param name="customCondition">The custom condition to check.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> Satisfy<T, TException>(Func<T, bool> customCondition, string message)
        where TException : Exception, new()
        where T : TArgument
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckGenericCondition<T, TException>(customCondition, (T)ArgumentValue, message);
        return _linker;
    }

    public Linker<TContract> HasProperty(string propertyName, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.ContainsPropertyByName(ArgumentValue, propertyName, message);

        return _linker;
    }

    public Linker<TContract> HasPropertyWithValue(string propertyName, object value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.ContainsPropertyWithValue(ArgumentValue, propertyName, value, message);

        return _linker;
    }

    public Linker<TContract> HasMethod(string methodName, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.ContainsMethod(ArgumentValue, methodName, message);

        return _linker;
    }
}

