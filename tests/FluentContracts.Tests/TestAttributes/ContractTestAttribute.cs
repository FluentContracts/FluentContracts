using System;
using Xunit.Sdk;

namespace FluentContracts.Tests.TestAttributes;

[TraitDiscoverer(Constants.DiscovererTypeName, Constants.AssemblyName)]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class ContractTestAttribute(string contractName) : Attribute, ITraitAttribute
{
    public string ContractName { get; private set; } = contractName;
}