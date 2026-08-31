using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

/// <summary>
/// The root of the contract hierarchy. It holds the argument and its name, which is all every
/// check needs; the checks themselves are added by the contracts deriving from it, in the order
/// <see cref="NullableContract{TArgument,TContract}"/>, <see cref="ObjectContract{TArgument,TContract}"/>,
/// <see cref="EqualityContract{TArgument,TContract}"/> and then one contract per type.
/// </summary>
/// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public abstract class BaseContract<TArgument, TContract>
    where TContract : BaseContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected BaseContract(TArgument? argumentValue, string argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }

    /// <summary>The argument being checked, as it was handed to <c>Must()</c>.</summary>
    protected TArgument? ArgumentValue { get; }
    /// <summary>
    /// The argument's name, captured by <c>[CallerArgumentExpression]</c> at the call to <c>Must()</c>
    /// and reported as the parameter name when a check fails.
    /// </summary>
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
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T>(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(customCondition, typedValue, ArgumentName, message);
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
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T, TException>(ArgumentValue);
        Validator.CheckGenericCondition<T, TException>(customCondition, typedValue);
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
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T, TException>(ArgumentValue, message);
        Validator.CheckGenericCondition<T, TException>(customCondition, typedValue, message);
        return _linker;
    }
}

