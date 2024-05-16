using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts.Struct;

public class NullableBoolContract(bool? argumentValue, string argumentName)
    : ComparableContract<bool?, NullableBoolContract>(argumentValue, argumentName)
{
}

public class BoolContract : ComparableContract<bool, BoolContract>
{
    private readonly Linker<BoolContract> _linker;

    public BoolContract(bool argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<BoolContract>(this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="bool"/> argument is "true".
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<BoolContract> BeTrue(string? message = null)
    {
        Validator.CheckForSpecificValue(true, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="bool"/> argument "false".
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<BoolContract> BeFalse(string? message = null)
    {
        Validator.CheckForSpecificValue(false, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}