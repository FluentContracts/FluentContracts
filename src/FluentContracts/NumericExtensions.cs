
#if NET8_0_OR_GREATER
using System.Numerics;
#endif
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Infrastructure;

namespace FluentContracts;

/// <summary>
/// The <c>Must()</c> entry points for the numeric types.
/// </summary>
public static class NumericExtensions
{
    #region int
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="int"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the IntContract class.</returns>
    
    public static IntContract Must(
        this int argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new IntContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="int"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableIntContract class.</returns>
    
    public static IntContract Must(
        this int? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new IntContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="uint"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the UintContract class.</returns>
    
    public static UintContract Must(
        this uint argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UintContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="uint"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableUintContract class.</returns>
    
    public static UintContract Must(
        this uint? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UintContract(argument, argumentName) { ChainMessage = message };
    }
    
    #endregion

    #region decimal

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="decimal"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the DecimalContract class.</returns>
    
    public static DecimalContract Must(
        this decimal argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DecimalContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="decimal"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableDecimalContract class.</returns>
    
    public static DecimalContract Must(
        this decimal? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DecimalContract(argument, argumentName) { ChainMessage = message };
    }

    #endregion

    #region double

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="double"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the DoubleContract class.</returns>
    
    public static DoubleContract Must(
        this double argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DoubleContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="double"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableDoubleContract class.</returns>
    
    public static DoubleContract Must(
        this double? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DoubleContract(argument, argumentName) { ChainMessage = message };
    }

    #endregion

    #region short

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="short"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ShortContract class.</returns>
    
    public static ShortContract Must(
        this short argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ShortContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="short"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableShortContract class.</returns>
    
    public static ShortContract Must(
        this short? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ShortContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="ushort"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the UshortContract class.</returns>
    
    public static UshortContract Must(
        this ushort argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UshortContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="ushort"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableUshortContract class.</returns>
    
    public static UshortContract Must(
        this ushort? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UshortContract(argument, argumentName) { ChainMessage = message };
    }

    #endregion

    #region byte

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="byte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ByteContract class.</returns>
    
    public static ByteContract Must(
        this byte argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ByteContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="byte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableByteContract class.</returns>
    
    public static ByteContract Must(
        this byte? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ByteContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="sbyte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the SbyteContract class.</returns>
    
    public static SbyteContract Must(
        this sbyte argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new SbyteContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="sbyte"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableSbyteContract class.</returns>
    
    public static SbyteContract Must(
        this sbyte? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new SbyteContract(argument, argumentName) { ChainMessage = message };
    }

    #endregion

    #region float

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="float"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the FloatContract class.</returns>
    
    public static FloatContract Must(
        this float argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new FloatContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="float"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableFloatContract class.</returns>
    
    public static FloatContract Must(
        this float? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new FloatContract(argument, argumentName) { ChainMessage = message };
    }

    #endregion

    #region long

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="long"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the LongContract class.</returns>
    
    public static LongContract Must(
        this long argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new LongContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="long"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableLongContract class.</returns>
    
    public static LongContract Must(
        this long? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new LongContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="ulong"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the UlongContract class.</returns>
    
    public static UlongContract Must(
        this ulong argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UlongContract(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="ulong"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the NullableUlongContract class.</returns>
    
    public static UlongContract Must(
        this ulong? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new UlongContract(argument, argumentName) { ChainMessage = message };
    }

    #endregion

#if NET8_0_OR_GREATER

    #region INumber<T>

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of any number type — one
    /// implementing <see cref="INumber{TSelf}"/> — that has no hand-written contract: <c>Half</c>,
    /// <c>Int128</c>, <c>BigInteger</c>, <c>nint</c>, a user's own number type. The types with a
    /// hand-written contract keep binding to it; a non-generic overload wins the tie.
    /// </summary>
    /// <typeparam name="T">The number type.</typeparam>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the <see cref="NumberContract{T}"/> class.</returns>
    public static NumberContract<T> Must<T>(
        this T argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
        where T : struct, INumber<T>
    {
        return new NumberContract<T>(argument, argumentName) { ChainMessage = message };
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for a nullable argument of any number
    /// type — one implementing <see cref="INumber{TSelf}"/> — that has no hand-written contract.
    /// </summary>
    /// <typeparam name="T">The number type.</typeparam>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="message">Optional message for every check in the chain; a check's own message still wins.</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the <see cref="NumberContract{T}"/> class.</returns>
    public static NumberContract<T> Must<T>(
        this T? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
        where T : struct, INumber<T>
    {
        return new NumberContract<T>(argument, argumentName) { ChainMessage = message };
    }

    #endregion

#endif
}
