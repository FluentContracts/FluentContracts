using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace FluentContracts.Tests.TestAttributes;

public abstract class ContractDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var contractName = traitAttribute.GetNamedArgument<string>("ContractName");

        if (!string.IsNullOrWhiteSpace(contractName))
            yield return new KeyValuePair<string, string>("Contract", contractName);
    }
}