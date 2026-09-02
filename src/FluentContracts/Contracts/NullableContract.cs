using System.Diagnostics.CodeAnalysis;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

/// <summary>
/// Adds the two checks that every argument has, whatever its type: <c>BeNull</c> and <c>NotBeNull</c>.
/// </summary>
/// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public abstract class NullableContract<TArgument, TContract> : BaseContract<TArgument, TContract>
    where TContract : NullableContract<TArgument, TContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected NullableContract(TArgument? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeNull(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeNull<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TException>(
        string? message = null)
        where TException : Exception, new()
    {
        if (message != null)
            Validator.CheckForNotNull<TArgument, TException>(ArgumentValue, message);
        else
            Validator.CheckForNotNull<TArgument, TException>(ArgumentValue);

        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument is null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeNull(string? message = null)
    {
        Validator.CheckForNull(ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }
}
