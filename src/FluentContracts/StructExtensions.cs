using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Struct;
using FluentContracts.Infrastructure;

namespace FluentContracts;

public static class StructExtensions
{
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
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
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
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
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
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
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
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new BoolContract(argument, argumentName);
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
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
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
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new NullableDateTimeContract(argument, argumentName);
    }

    #endregion
    
    #region Enum
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Enum"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the EnumContract class.</returns>
    [Pure]
    public static NullableEnumContract<TEnum?> Must<TEnum>(
        this TEnum? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
        where TEnum : struct, Enum
    {
        return new NullableEnumContract<TEnum?>(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Enum"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the EnumContract class.</returns>
    [Pure]
    public static EnumContract<TEnum> Must<TEnum>(
        this TEnum argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
        where TEnum : struct, Enum
    {
        return new EnumContract<TEnum>(argument, argumentName);
    }

    #endregion
}