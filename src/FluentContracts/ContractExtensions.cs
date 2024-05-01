using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using FluentContracts.Contracts;
using FluentContracts.Contracts.Struct;
using FluentContracts.Contracts.Text;

namespace FluentContracts;

public static partial class ContractExtensions
{
    private const string DefaultArgumentName = "argument";

    #region Generic

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="T"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableContract class.</returns>
    [Pure]
    public static NullableContract<T> Must<T>(
        this T argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new NullableContract<T>(argument, argumentName);
    }

    #endregion

    #region Guid

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Guid"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableGuidContract class.</returns>
    [Pure]
    public static NullableGuidContract Must(
        this Guid? argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new NullableGuidContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Guid"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the GuidContract class.</returns>
    [Pure]
    public static GuidContract Must(
        this Guid argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new GuidContract(argument, argumentName);
    }

    #endregion

    #region bool

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="bool"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableBoolContract class.</returns>
    [Pure]
    public static NullableBoolContract Must(
        this bool? argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new NullableBoolContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="bool"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the BoolContract class.</returns>
    [Pure]
    public static BoolContract Must(
        this bool argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new BoolContract(argument, argumentName);
    }

    #endregion

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
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
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
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
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
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new CharContract(argument, argumentName);
    }

    #endregion

    #region DateTime

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="DateTime"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the DateTimeContract class.</returns>
    [Pure]
    public static DateTimeContract Must(
        this DateTime argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new DateTimeContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="DateTime"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableDateTimeContract class.</returns>
    [Pure]
    public static NullableDateTimeContract Must(
        this DateTime? argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new NullableDateTimeContract(argument, argumentName);
    }

    #endregion
}
