using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts.Numeric;

public abstract class SignedNumberContract<TArgument, TContract> : NumberContract<TArgument, TContract>
    where TContract : SignedNumberContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

    protected SignedNumberContract(TArgument argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract) this);
    }
    
    /// <summary>
    /// Checks if the value of the argument is greater than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<TContract> BePositive(string? message = null)
    {
        Validator.CheckForGreaterThan(Zero, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument is not greater than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<TContract> NotBePositive(string? message = null)
    {
        Validator.CheckForLessOrEqualThan(Zero, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument is less than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<TContract> BeNegative(string? message = null)
    {
        Validator.CheckForLessThan(Zero, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument is not less than zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<TContract> NotBeNegative(string? message = null)
    {
        Validator.CheckForGreaterOrEqualThan(Zero, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}