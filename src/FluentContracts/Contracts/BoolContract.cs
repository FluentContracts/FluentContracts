using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class NullableBoolContract(bool? argumentValue, string argumentName)
        : BaseContract<bool?>(argumentValue, argumentName) {}
    
    public class BoolContract(bool argumentValue, string argumentName)
        : BaseContract<bool>(argumentValue, argumentName)
    {
        /// <summary>
        /// Checks if the value of the <see cref="bool"/> argument is "true".
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<bool> BeTrue(string? message = null)
        {
            Validator.CheckForSpecificValue(true, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the value of the <see cref="bool"/> argument "false".
        /// </summary>
        /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<bool> BeFalse(string? message = null)
        {
            Validator.CheckForSpecificValue(false, ArgumentValue, ArgumentName, message);
            return Linker;
        }
    }
}