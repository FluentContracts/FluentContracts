using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Web;
using FluentContracts.Infrastructure;

namespace FluentContracts;

/// <summary>
/// The <c>Must()</c> entry points for <see cref="System.Uri"/>.
/// </summary>
public static class UriExtensions
{
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Uri"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the UriContract class.</returns>
    public static UriContract Must(
        this Uri? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UriContract(argument, argumentName) { ChainMessage = message };
    }
}
