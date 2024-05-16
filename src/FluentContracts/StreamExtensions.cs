
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Streams;
using FluentContracts.Infrastructure;

namespace FluentContracts;

public static class StreamExtensions
{
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Stream"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the StreamContract class.</returns>
    
    public static StreamContract<Stream> Must(
        this Stream argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new StreamContract<Stream>(argument, argumentName);
    }
}