using FluentContracts.Contracts;

namespace FluentContracts.Infrastructure
{
    /// <summary>
    /// The Linker class is used for chaining contract checks and adding more checks to existing contracts.
    /// </summary>
    /// <typeparam name="T">The type of the contract</typeparam>
    public class Linker<T>
    {
        public Linker(BaseContract<T> linked)
        {
            Validator.CheckForNotNull(linked, nameof(linked), "Argument linked cannot be null");

            And = linked;
        }

        /// <summary>
        /// Link back to the contract so more checks can be chained in a fluent manner
        /// </summary>
        public BaseContract<T> And { get; }
    }
}