using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

/// <summary>
/// The entry point for checks on an argument of any type, obtained by calling <c>Must()</c> on it.
/// </summary>
/// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class ObjectContract<TArgument>(TArgument? argumentValue, string argumentName)
    : ObjectContract<TArgument, ObjectContract<TArgument>>(argumentValue, argumentName);

/// <summary>
/// Adds the checks that need nothing of the argument but its runtime type — <c>BeOfType</c>,
/// <c>BeAssignableTo</c> and their negations — along with <c>Satisfy</c> for an arbitrary condition.
/// </summary>
/// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public abstract class ObjectContract<TArgument, TContract> : NullableContract<TArgument, TContract>
    where TContract : ObjectContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected ObjectContract(TArgument? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the specified argument is of type <typeparamref name="T"/>
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeOfType<T>(string? message = null)
    {
        Validator.CheckForBeType<object?, T>(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is of type <paramref name="type"/>
    /// </summary>
    /// <param name="type">Type to check against</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeOfType(Type type, string? message = null)
    {
        Validator.CheckForBeType<object?>(type, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not of type <typeparamref name="T"/>
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeOfType<T>(string? message = null)
    {
        Validator.CheckForNotBeType<object?, T>(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not of type <paramref name="type"/>
    /// </summary>
    /// <param name="type">Type to check against</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeOfType(Type type, string? message = null)
    {
        Validator.CheckForNotBeType<object?>(type, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is assignable to type <typeparamref name="T"/>
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAssignableTo<T>(string? message = null)
    {
        Validator.CheckForBeAssignableTo(ArgumentValue, typeof(T), ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not assignable to type <typeparamref name="T"/>
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAssignableTo<T>(string? message = null)
    {
        Validator.CheckForNotBeAssignableTo(ArgumentValue, typeof(T), ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is assignable to type <paramref name="targetType"/>
    /// </summary>
    /// <param name="targetType">Type to check against</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAssignableTo(Type targetType, string? message = null)
    {
        Validator.CheckForBeAssignableTo(ArgumentValue, targetType, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not assignable to type <paramref name="targetType"/>
    /// </summary>
    /// <param name="targetType">Type to check against</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAssignableTo(Type targetType, string? message = null)
    {
        Validator.CheckForNotBeAssignableTo(ArgumentValue, targetType, ArgumentName, message);
        return _linker;
    }
}
