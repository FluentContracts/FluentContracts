using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class StringContract(string? argumentValue, string? argumentName = null)
        : Contract<string?>(argumentValue, argumentName ?? DefaultFallbackName)
    {
        private const string DefaultFallbackName = "String argument";

        public Linker<string?> BeEmpty()
        {
            Validator.CheckForSpecificValue(string.Empty, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<string?> NotBeNullOrEmpty()
        {
            Validator.CheckNotForSpecificValue(string.Empty, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<string?> BeNullOrEmpty()
        {
            BeEmpty();
            BeNull();
            return Linker;
        }

        public Linker<string?> Be(string expectedValue)
        {
            Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<string?> NotBe(string expectedValue)
        {
            Validator.CheckNotForSpecificValue(expectedValue, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<string?> BeAnyOf(params string[] expectedValues)
        {
            Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<string?> NotBeAnyOf(params string[] expectedValues)
        {
            Validator.CheckNotForAnyOf(expectedValues, ArgumentValue, ArgumentName);
            return Linker;
        }
    }
}