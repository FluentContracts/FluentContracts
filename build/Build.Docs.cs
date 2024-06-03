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
    readonly struct ContractInfo(string name, string extends, List<string> contracts)
    {
        public string Name { get; } = name;
        public string Extends { get; } = extends;
        public List<string> Contracts { get; } = contracts;
    }
    
    AbsolutePath SupportedContractsFile => RootDirectory / "docs" / "SupportedContracts.md";
    AbsolutePath FluentContractsAssembly => PublishDirectory / "FluentContracts" / "FluentContracts.dll";
    const string RootNamespace = "FluentContracts.Contracts";

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

                if (!c.Extends.Equals("object", StringComparison.OrdinalIgnoreCase))
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
                ExtractMethods(methods)
                )
            ).Where(c => c.Contracts.Count > 0).ToList();

        return TopologicalSort(contracts);
    }

    List<string> ExtractMethods(MethodInfo[] methods)
    {
        return methods
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
    
    string GetNameWithoutGenericArity(Type t)
    {
        var name = t.Name;
        var index = name.IndexOf('`');
        var className = index == -1 ? name : name[..index];

        return className.Replace("Contract", "");
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
            throw new InvalidOperationException("Cyclic dependency detected");
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