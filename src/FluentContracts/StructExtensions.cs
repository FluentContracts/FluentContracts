
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
    
    public static GuidContract Must(
        this Guid? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new GuidContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Guid"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the GuidContract class.</returns>
    
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
    
    public static BoolContract Must(
        this bool? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new BoolContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="bool"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the BoolContract class.</returns>
    
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
    /// <param name="dateTimeProvider">DateTime provider override to use for getting DateTime.Now and DateTime.Today</param>
    /// <returns>A new instance of the DateTimeContract class.</returns>
    
    public static DateTimeContract Must(
        this DateTime argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName,
        IDateTimeProvider? dateTimeProvider = null)
    {
        return new DateTimeContract(argument, argumentName, dateTimeProvider);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="DateTime"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <param name="dateTimeProvider">DateTime provider override to use for getting DateTime.Now and DateTime.Today</param>
    /// <returns>A new instance of the NullableDateTimeContract class.</returns>
    
    public static DateTimeContract Must(
        this DateTime? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName, 
        IDateTimeProvider? dateTimeProvider = null)
    {
        return new DateTimeContract(argument, argumentName, dateTimeProvider);
    }

    #endregion
    
    #region Enum
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Enum"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the EnumContract class.</returns>
    
    public static EnumContract<TEnum> Must<TEnum>(
        this TEnum argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
        where TEnum : struct, Enum
    {
        return new EnumContract<TEnum>(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Enum"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the EnumContract class.</returns>
    
    public static EnumContract<TEnum> Must<TEnum>(
        this TEnum? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
        where TEnum : struct, Enum
    {
        return new EnumContract<TEnum>(argument, argumentName);
    }

    #endregion
    
    #region TimeSpan
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="DateTime"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableDateTimeContract class.</returns>
    
    public static TimeSpanContract Must(
        this TimeSpan argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new TimeSpanContract(argument, argumentName);
    }
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="DateTime"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableDateTimeContract class.</returns>
    
    public static TimeSpanContract Must(
        this TimeSpan? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new TimeSpanContract(argument, argumentName);
    }

    #endregion
}