using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Streams;

/// <summary>
/// The entry point for checks on a <see cref="System.IO.Stream"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class StreamContract(Stream? argumentValue, string argumentName)
    : StreamContract<Stream?, StreamContract>(argumentValue, argumentName);

/// <summary>
/// The inheritable contract for a <see cref="System.IO.Stream"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TStream">The stream type being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public abstract class StreamContract<TStream, TContract> : ObjectContract<TStream, TContract>
    where TStream : Stream?
    where TContract : StreamContract<TStream, TContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected StreamContract(TStream argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument supports seeking
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeSeekable(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.CanSeek, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument does not support seeking
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeSeekable(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a!.CanSeek, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument supports reading
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeReadable(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.CanRead, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument does not support reading
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeReadable(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a!.CanRead, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument supports timeout
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeAbleToTimeout(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.CanTimeout, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument does not support timeout
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeAbleToTimeout(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a!.CanTimeout, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument supports writing
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWriteable(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.CanWrite, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument does not support writing
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWriteable(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a!.CanWrite, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument is at specific position.
    /// </summary>
    /// <param name="expectedPosition">Expected position for the stream to be at</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeAtPosition(long expectedPosition, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(expectedPosition, ArgumentValue.Position, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument is not at specific position.
    /// </summary>
    /// <param name="unexpectedPosition">Position which the stream should not be at</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeAtPosition(long unexpectedPosition, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(unexpectedPosition, ArgumentValue.Position, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument is with a specific length.
    /// </summary>
    /// <param name="expectedLength">Expected length for the stream to be</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWithLength(long expectedLength, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(expectedLength, ArgumentValue.Length, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Stream"/> argument is not with a specific length.
    /// </summary>
    /// <param name="unexpectedLength">Expected length for the stream to not be</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWithLength(long unexpectedLength, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(unexpectedLength, ArgumentValue.Length, ArgumentName, message);
        return (TContract)this;
    }
}
