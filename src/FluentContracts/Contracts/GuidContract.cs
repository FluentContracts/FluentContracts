using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class GuidContract(Guid? argumentValue, string? argumentName = null)
        : Contract<Guid?>(argumentValue, argumentName ?? DefaultFallbackName)
    {
        private const string DefaultFallbackName = "Guid argument";
        
        public Linker<Guid?> BeEmpty(string? message = null)
        {
            Validator.CheckForSpecificValue(Guid.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }
        
        public Linker<Guid?> NotBeEmpty(string? message = null)
        {
            Validator.CheckForNotSpecificValue(Guid.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }
    }
}