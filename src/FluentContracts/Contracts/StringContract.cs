using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class StringContract(string? argumentValue, string argumentName)
        : Contract<string?>(argumentValue, argumentName)
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
    }
}