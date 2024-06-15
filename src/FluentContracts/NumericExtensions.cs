
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Infrastructure;

namespace FluentContracts;

public static class NumericExtensions
{
    #region int
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="int"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the IntContract class.</returns>
    
    public static IntContract Must(
        this int argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new IntContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="int"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableIntContract class.</returns>
    
    public static IntContract Must(
        this int? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new IntContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="uint"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the UintContract class.</returns>
    
    public static UintContract Must(
        this uint argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UintContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="uint"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableUintContract class.</returns>
    
    public static UintContract Must(
        this uint? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UintContract(argument, argumentName);
    }
    
    #endregion

    #region decimal

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="decimal"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the DecimalContract class.</returns>
    
    public static DecimalContract Must(
        this decimal argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DecimalContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="decimal"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableDecimalContract class.</returns>
    
    public static DecimalContract Must(
        this decimal? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DecimalContract(argument, argumentName);
    }

    #endregion

    #region double

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="double"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the DoubleContract class.</returns>
    
    public static DoubleContract Must(
        this double argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DoubleContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="double"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableDoubleContract class.</returns>
    
    public static DoubleContract Must(
        this double? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DoubleContract(argument, argumentName);
    }

    #endregion

    #region short

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="short"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ShortContract class.</returns>
    
    public static ShortContract Must(
        this short argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ShortContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="short"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableShortContract class.</returns>
    
    public static ShortContract Must(
        this short? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ShortContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="ushort"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the UshortContract class.</returns>
    
    public static UshortContract Must(
        this ushort argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UshortContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="ushort"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableUshortContract class.</returns>
    
    public static UshortContract Must(
        this ushort? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UshortContract(argument, argumentName);
    }

    #endregion

    #region byte

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="byte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ByteContract class.</returns>
    
    public static ByteContract Must(
        this byte argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ByteContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="byte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableByteContract class.</returns>
    
    public static ByteContract Must(
        this byte? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ByteContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="sbyte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the SbyteContract class.</returns>
    
    public static SbyteContract Must(
        this sbyte argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new SbyteContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="sbyte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableSbyteContract class.</returns>
    
    public static SbyteContract Must(
        this sbyte? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new SbyteContract(argument, argumentName);
    }

    #endregion

    #region float

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="float"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the FloatContract class.</returns>
    
    public static FloatContract Must(
        this float argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new FloatContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="float"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableFloatContract class.</returns>
    
    public static FloatContract Must(
        this float? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new FloatContract(argument, argumentName);
    }

    #endregion

    #region long

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="long"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the LongContract class.</returns>
    
    public static LongContract Must(
        this long argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new LongContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="long"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableLongContract class.</returns>
    
    public static LongContract Must(
        this long? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new LongContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="ulong"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the UlongContract class.</returns>
    
    public static UlongContract Must(
        this ulong argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UlongContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="ulong"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableUlongContract class.</returns>
    
    public static UlongContract Must(
        this ulong? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UlongContract(argument, argumentName);
    }

    #endregion
}