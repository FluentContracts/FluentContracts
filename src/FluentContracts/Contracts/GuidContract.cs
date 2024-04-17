using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class GuidContract(Guid? argumentValue, string argumentName)
        : Contract<Guid?>(argumentValue, argumentName)
    {
        /// <summary>
        /// Checks if the value of the <see cref="Guid"/> argument is empty.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<Guid?> BeEmpty(string? message = null)
        {
            Validator.CheckForSpecificValue(Guid.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the value of the <see cref="Guid"/> argument is not empty.
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<Guid?> NotBeEmpty(string? message = null)
        {
            Validator.CheckForNotSpecificValue(Guid.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }
    }
}