
using System.Runtime.CompilerServices;
using FluentContracts.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts;

/// <summary>
/// The <c>Must()</c> entry points for any object, plus the generic checks every contract inherits.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="object"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the <see cref="ObjectContract{TArgument}"/> class.</returns>
    
    public static ObjectContract<object> Must(
        this object? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ObjectContract<object>(argument, argumentName) { ChainMessage = message };
    }
}
