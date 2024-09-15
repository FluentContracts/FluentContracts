using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

public class ObjectContract<TArgument>(TArgument? argumentValue, string argumentName)
    : ObjectContract<TArgument, ObjectContract<TArgument>>(argumentValue, argumentName);

public abstract class ObjectContract<TArgument, TContract> : NullableContract<TArgument, TContract>
    where TContract : ObjectContract<TArgument, TContract>
{
    private readonly Linker<TContract> _linker;

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