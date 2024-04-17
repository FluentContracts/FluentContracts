using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using FluentContracts.Contracts;

namespace FluentContracts

{
    public static class ContractExtensions
    {
        private const string DefaultArgumentName = "argument";

        /// <summary>
        /// Indicates a start in the fluent chain of validations for a <see cref="Guid"/>
        /// </summary>
        /// <param name="argument">Argument to be validated</param>
        /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
        /// <returns>A new instance of the GuidContract class.</returns>
        [Pure]
        public static GuidContract Must(
            this Guid? argument,
            [CallerArgumentExpression("argument")]
            string argumentName = DefaultArgumentName)
        {
            return new GuidContract(argument, argumentName);
        }
        
        /// <summary>
        /// Indicates a start in the fluent chain of validations for a <see cref="Guid"/>
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
        /// Indicates a start in the fluent chain of validations for a <see cref="string"/>
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
    }
}