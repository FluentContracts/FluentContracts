using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Text;
using FluentContracts.Infrastructure;

namespace FluentContracts;

public static class TextExtensions
{
    #region string

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="string"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the StringContract class.</returns>
    [Pure]
    public static StringContract Must(
        this string argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new StringContract(argument, argumentName);
    }

    #endregion

    #region char

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="char"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableCharContract class.</returns>
    [Pure]
    public static NullableCharContract Must(
        this char? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new NullableCharContract(argument, argumentName);
    }
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="char"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the CharContract class.</returns>
    [Pure]
    public static CharContract Must(
        this char argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new CharContract(argument, argumentName);
    }

    #endregion
}
