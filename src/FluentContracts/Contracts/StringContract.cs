using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts
{
    public class StringContract(string? argumentValue, string argumentName)
        : Contract<string?>(argumentValue, argumentName)
    {

        public Linker<string?> BeEmpty(string? message = null)
        {
            Validator.CheckForSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        public Linker<string?> NotBeEmpty(string? message = null)
        {
            Validator.CheckForNotSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
            return Linker;
        }
    }
}