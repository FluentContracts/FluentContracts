
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Streams;
using FluentContracts.Infrastructure;

namespace FluentContracts;

/// <summary>
/// The <c>Must()</c> entry points for <see cref="System.IO.Stream"/>.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Stream"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the StreamContract class.</returns>
    
    public static StreamContract Must(
        this Stream? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new StreamContract(argument, argumentName) { ChainMessage = message };
    }
}