using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class StringContract(string? argumentValue, string argumentName)
        : BaseContract<string?>(argumentValue, argumentName)
    {
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is empty.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<string?> BeEmpty(string? message = null)
        {
            Validator.CheckForSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is not empty.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<string?> NotBeEmpty(string? message = null)
        {
            Validator.CheckForNotSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is null or empty.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<string?> BeNullOrEmpty(string? message = null)
        {
            Validator.CheckGenericCondition(string.IsNullOrEmpty, ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is not null or empty.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<string?> NotBeNullOrEmpty(string? message = null)
        {
            Validator.CheckGenericCondition(a => !string.IsNullOrEmpty(a), ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is whitespace(s).
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> BeWhitespace(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckGenericCondition(string.IsNullOrWhiteSpace, ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is not whitespace(s).
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> NotBeWhitespace(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckGenericCondition(a => !string.IsNullOrWhiteSpace(a), ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is null or whitespace(s).
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<string?> BeNullOrWhitespace(string? message = null)
        {
            Validator.CheckGenericCondition(string.IsNullOrWhiteSpace, ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is not null or whitespace(s).
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<string?> NotBeNullOrWhitespace(string? message = null)
        {
            Validator.CheckGenericCondition(a => !string.IsNullOrWhiteSpace(a), ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is uppercase.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> BeUppercase(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckForSpecificValue(ArgumentValue!.ToUpperInvariant(), ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is not uppercase.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> NotBeUppercase(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckForNotSpecificValue(ArgumentValue!.ToUpperInvariant(), ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is lowercase.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> BeLowercase(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckForSpecificValue(ArgumentValue!.ToLowerInvariant(), ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        /// <summary>
        /// Checks if the value of the <see cref="string"/> argument is not lowercase.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> NotBeLowercase(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckForNotSpecificValue(ArgumentValue!.ToLowerInvariant(), ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if <see cref="containedString"/> is part of the value of the <see cref="string"/> argument.
        /// </summary>
        /// <param name="containedString">A string to check for being part of the argument</param>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> Contain(string containedString, string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckGenericCondition(a => a!.Contains(containedString, StringComparison.OrdinalIgnoreCase), ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if <see cref="containedString"/> is not part of the value of the <see cref="string"/> argument.
        /// </summary>
        /// <param name="containedString">A string to check for being part of the argument</param>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        /// <remarks>Also checks for the argument to NOT be null</remarks>
        [Pure]
        public Linker<string?> NotContain(string containedString, string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            Validator.CheckGenericCondition(a => !a!.Contains(containedString, StringComparison.OrdinalIgnoreCase), ArgumentValue, ArgumentName, message);
            return Linker;
        }
    }
}