using System.Runtime.CompilerServices;
using FluentContracts.Contracts.IO;
using FluentContracts.Infrastructure;

namespace FluentContracts;

// ReSharper disable once InconsistentNaming
public static class IOExtensions
{
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="FileInfo"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the StreamContract class.</returns>
    /// 
    public static FileInfoContract Must(
        this FileInfo? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new FileInfoContract(argument, argumentName);
    }
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="DirectoryInfo"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the StreamContract class.</returns>
    /// 
    public static DirectoryInfoContract Must(
        this DirectoryInfo? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DirectoryInfoContract(argument, argumentName);
    }
}