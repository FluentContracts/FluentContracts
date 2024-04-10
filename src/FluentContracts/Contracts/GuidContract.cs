using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class GuidContract(Guid? argumentValue, string? argumentName = null)
        : Contract<Guid?>(argumentValue, argumentName ?? DefaultFallbackName)
    {
        private const string DefaultFallbackName = "Guid argument";

        public Linker<Guid?> NotBeEmpty()
        {
            Validator.CheckNotForSpecificValue(Guid.Empty, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<Guid?> BeEmpty()
        {
            Validator.CheckForSpecificValue(Guid.Empty, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<Guid?> Be(Guid expectedValue)
        {
            Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<Guid?> NotBe(Guid expectedValue)
        {
            Validator.CheckNotForSpecificValue(expectedValue, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<Guid?> BeAnyOf(params Guid?[] expectedValues)
        {
            Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<Guid?> NotBeAnyOf(params Guid?[] expectedValues)
        {
            Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName);
            return Linker;
        }
    }
}