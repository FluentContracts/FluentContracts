using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class BaseContract<T>(T argumentValue, string argumentName)
    {
        private Linker<T>? _linker;
        protected Linker<T> Linker => _linker ??= new Linker<T>(this);
        
        protected T ArgumentValue { get; } = argumentValue;
        protected string ArgumentName { get; } = argumentName;

        /// <summary>
        /// Checks if the specified argument is not null.
        /// </summary>
        /// <param name="message">The optional error message to include in the exception.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> NotBeNull(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the specified argument is null.
        /// </summary>
        /// <param name="message">The optional error message to include in the exception.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> BeNull(string? message = null)
        {
            Validator.CheckForNull(ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the specified argument is equal to the expected value.
        /// </summary>
        /// <param name="expectedValue">The expected value to compare against.</param>
        /// <param name="message">The optional error message to include in the exception.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> Be(T expectedValue, string? message = null)
        {
            Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the specified argument is not equal to the expected value.
        /// </summary>
        /// <param name="expectedValue">The value to compare the argument against.</param>
        /// <param name="message">The optional error message to include in the exception.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> NotBe(T expectedValue, string? message = null)
        {
            Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the specified argument is any of the expected values.
        /// </summary>
        /// <param name="expectedValues">Expected values among which the argument can be.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> BeAnyOf(params T[] expectedValues)
        {
            return BeAnyOf(null, expectedValues);
        }

        /// <summary>
        /// Checks if the specified argument is any of the expected values.
        /// </summary>
        /// <param name="message">The optional error message to include in the exception.</param>
        /// <param name="expectedValues">Expected values among which the argument can be.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> BeAnyOf(string? message, params T[] expectedValues)
        {
            Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the specified argument is not any of the expected values.
        /// </summary>
        /// <param name="expectedValues">The expected values that the argument must not be.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> NotBeAnyOf(params T[] expectedValues)
        {
            return NotBeAnyOf(null, expectedValues);
        }

        /// <summary>
        /// Checks if the specified argument is not any of the expected values.
        /// </summary>
        /// <param name="message">The optional error message to include in the exception.</param>
        /// <param name="expectedValues">The expected values that the argument must not be.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> NotBeAnyOf(string? message, params T[] expectedValues)
        {
            Validator.CheckForNotAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        /// <summary>
        /// Checks if the specified argument satisfies a custom condition.
        /// </summary>
        /// <param name="customCondition">The custom condition to check.</param>
        /// <param name="message">The optional error message to include in the exception.</param>
        /// <returns>Linker for chaining more checks</returns>
        [Pure]
        public Linker<T> Satisfy(Func<T, bool> customCondition, string? message = null)
        {
            Validator.CheckGenericCondition(customCondition, ArgumentValue, ArgumentName, message);
            return Linker;
        }
    }
}
