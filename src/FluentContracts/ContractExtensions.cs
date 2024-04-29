using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using FluentContracts.Contracts;

namespace FluentContracts

{
    public static class ContractExtensions
    {
        private const string DefaultArgumentName = "argument";
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="T"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the GuidContract class.</returns>
        [Pure]
        public static BaseContract<T> Must<T>(
            this T argument,
            [CallerArgumentExpression("argument")]
            string argumentName = DefaultArgumentName)
        {
            return new BaseContract<T>(argument, argumentName);
        }

        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Guid"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the GuidContract class.</returns>
        [Pure]
        public static NullableGuidContract Must(
            this Guid? argument,
            [CallerArgumentExpression("argument")]
            string argumentName = DefaultArgumentName)
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
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="bool"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the GuidContract class.</returns>
        [Pure]
        public static NullableBoolContract Must(
            this bool? argument,
            [CallerArgumentExpression("argument")]
            string argumentName = DefaultArgumentName)
        {
            return new NullableBoolContract(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="bool"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the GuidContract class.</returns>
        [Pure]
        public static BoolContract Must(
            this bool argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new BoolContract(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="string"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static StringContract Must(
            this string? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new StringContract(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="char"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
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
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static CharContract Must(
            this char argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new CharContract(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="int"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<int> Must(
            this int argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<int>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="int"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<int?> Must(
            this int? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<int?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="decimal"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<decimal> Must(
            this decimal argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<decimal>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="decimal"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<decimal?> Must(
            this decimal? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<decimal?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="double"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<double> Must(
            this double argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<double>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="double"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<double?> Must(
            this double? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<double?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="short"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<short> Must(
            this short argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<short>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="short"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<short?> Must(
            this short? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<short?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="byte"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<byte> Must(
            this byte argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<byte>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="byte"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<byte?> Must(
            this byte? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<byte?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="sbyte"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<sbyte> Must(
            this sbyte argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<sbyte>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="sbyte"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<sbyte?> Must(
            this sbyte? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<sbyte?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="float"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<float> Must(
            this float argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<float>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="float"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<float?> Must(
            this float? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<float?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="long"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<long> Must(
            this long argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<long>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type nullable <see cref="long"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static ComparableContract<long?> Must(
            this long? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new ComparableContract<long?>(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for an argument of type <see cref="DateTime"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the StringContract class.</returns>
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
        /// <returns>A new instance of the StringContract class.</returns>
        [Pure]
        public static NullableDateTimeContract Must(
            this DateTime? argument,
            [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
        {
            return new NullableDateTimeContract(argument, argumentName);
        }
    }
}