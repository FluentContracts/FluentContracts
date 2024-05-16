using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts;

public abstract class BaseContract<TArgument, TContract>
    where TContract : BaseContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

    protected BaseContract(TArgument argumentValue, string argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }

    protected TArgument ArgumentValue { get; }
    protected string ArgumentName { get; }

    /// <summary>
    /// Checks if the specified argument satisfies a custom condition.
    /// </summary>
    /// <param name="customCondition">The custom condition to check.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Satisfy(Func<TArgument, bool> customCondition, string? message = null)
    {
        Validator.CheckGenericCondition(customCondition, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}

