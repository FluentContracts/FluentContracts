using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;

// ReSharper disable InconsistentNaming
partial class Build
{
    [DebuggerDisplay("{Name}:{Extends} => {Contracts.Count}")]
    readonly struct ContractInfo(string name, string extends, string area, List<string> contracts)
    {
        public string Name { get; } = name;
        public string Extends { get; } = extends;

        /// <summary>
        /// The contract's namespace below <see cref="RootNamespace"/> — <c>Numeric</c>, <c>Text</c>,
        /// and so on, or <see cref="CoreArea"/> for the ones in the root. It groups the skill
        /// cheatsheet's catalogue, so a contract added to a new folder lands in the right section
        /// with nothing to maintain by hand.
        /// </summary>
        public string Area { get; } = area;

        public List<string> Contracts { get; } = contracts;
    }
    
    AbsolutePath SupportedContractsFile => RootDirectory / "docs" / "SupportedContracts.md";
    AbsolutePath FluentContractsAssembly => PublishDirectory / "FluentContracts" / "FluentContracts.dll";
    const string RootNamespace = "FluentContracts.Contracts";

    /// <summary>The area for the contracts sitting directly in <see cref="RootNamespace"/>.</summary>
    const string CoreArea = "Core";

    [UsedImplicitly]
    Target GenerateSupportedContracts => _ => _
        .TriggeredBy(Test)
        .After(Test)
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            var builder = new StringBuilder();
            builder.AppendLine("# Supported Contracts");
            builder.AppendLine();
            builder.AppendLine(
                "> Note: Check the [CHANGELOG](../CHANGELOG.md) to see which of the methods below are released and which ones are still in the making.");
            
            var contracts = ExtractClasses();

            contracts.ForEach(c =>
            {
                builder.AppendLine();
                builder.Append($"## `{c.Name}`");

                if (c.Extends != null)
                {
                    builder.Append($" (extends `{c.Extends}`)");
                }

                builder.AppendLine();
                builder.AppendLine();
                
                c.Contracts.ForEach(m => builder.AppendLine($"- `{m}`"));
            });

            SupportedContractsFile.WriteAllText(builder.ToString());
        });

    List<ContractInfo> ExtractClasses()
    {
        var assembly = Assembly.LoadFrom(FluentContractsAssembly);

        var classes = assembly.GetTypes()
            .Where(t => 
                t.IsClass 
                && t.Namespace != null 
                && t.Namespace.StartsWith(RootNamespace));

        var contracts = (
            from classType 
                in classes 
            let methods = 
                classType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) 
            select new ContractInfo(
                GetNameWithoutGenericArity(classType), 
                GetNameWithoutGenericArity(classType.BaseType),
                AreaOf(classType),
                ExtractMethods(methods)
                )
            ).Where(c => c.Contracts.Count > 0).ToList();

        return TopologicalSort(contracts);
    }

    List<string> ExtractMethods(MethodInfo[] methods)
    {
        return methods
            // Property getters and operators are compiler-generated members, not checks. `get_And`
            // used to be listed as one, which is noise in the docs and a name an agent could try to
            // call in the skill's catalogue.
            .Where(m => !m.IsSpecialName)
            .Select(m => m.Name)
            .Distinct()
            .GroupBy(name => name.StartsWith("Not") ? name[3..] : name)
            .Select(group =>
            {
                var methodName = group.Key;
                return group.Count() > 1 ? $"(Not){methodName}" : group.First();
            })
            .OrderBy(c => c)
            .ToList();
    }
    
    /// <summary>
    /// The area a contract belongs to: its namespace below <see cref="RootNamespace"/>, or
    /// <see cref="CoreArea"/> for the contracts that sit directly in it.
    /// </summary>
    static string AreaOf(Type type)
    {
        var suffix = type.Namespace?.Length > RootNamespace.Length
            ? type.Namespace[(RootNamespace.Length + 1)..]
            : null;

        return string.IsNullOrEmpty(suffix) ? CoreArea : suffix;
    }

    string GetNameWithoutGenericArity(Type t)
    {
        var name = t.Name;
        var index = name.IndexOf('`');
        var className = index == -1 ? name : name[..index];

        return !className.EndsWith("Contract") 
            ? null 
            : className.Replace("Contract", "");
    }
    
    List<ContractInfo> TopologicalSort(List<ContractInfo> contracts)
    {
        var sorted = new List<ContractInfo>();
        var visited = new HashSet<string>();
        var tempMarks = new HashSet<string>();

        var classDict = contracts.ToDictionary(c => c.Name, c => c);

        foreach (var contract in contracts.Where(c => !visited.Contains(c.Name)))
        {
            Visit(contract, classDict, sorted, visited, tempMarks);
        }

        return sorted;
    }

    void Visit(
        ContractInfo contractInfo,
        Dictionary<string, ContractInfo> classDict,
        List<ContractInfo> sorted,
        HashSet<string> visited,
        HashSet<string> tempMarks)
    {
        if (tempMarks.Contains(contractInfo.Name))
        {
            throw new InvalidOperationException($"Cyclic dependency detected: {contractInfo.Name}");
        }

        if (visited.Contains(contractInfo.Name)) return;
        
        tempMarks.Add(contractInfo.Name);

        if (contractInfo.Extends != null 
            && classDict.TryGetValue(contractInfo.Extends, out var value))
        {
            Visit(value, classDict, sorted, visited, tempMarks);
        }

        tempMarks.Remove(contractInfo.Name);
        visited.Add(contractInfo.Name);
        sorted.Add(contractInfo);
    }
}