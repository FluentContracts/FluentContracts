using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.IO;

public class FileInfoContract : NullableContract<FileInfo, FileInfoContract>
{
    private readonly Linker<FileInfoContract> _linker;
    
    public FileInfoContract(FileInfo? argumentValue, string argumentName) 
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<FileInfoContract>(this);
    }

    /// <summary>
    /// Checks if the file, described by the argument, exists
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> Exist(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Exists, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the file, described by the argument, does not exist
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> NotExist(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Exists, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="FileInfo"/> argument has a specified extension
    /// </summary>
    /// <param name="extension">Extension to match</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> HaveExtension(string extension, string? message = null)
    {
        var extensionWithDot =
            extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                ? extension
                : "." + extension;
            
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Extension.Equals(extensionWithDot, StringComparison.OrdinalIgnoreCase), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="FileInfo"/> argument does not have a specified extension
    /// </summary>
    /// <param name="extension">Extension to match</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> NotHaveExtension(string extension, string? message = null)
    {
        var extensionWithDot =
            extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                ? extension
                : "." + extension;
            
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Extension.Equals(extensionWithDot, StringComparison.OrdinalIgnoreCase), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is read-only
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> BeReadOnly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.IsReadOnly, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not read-only
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> NotBeReadOnly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.IsReadOnly, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is hidden
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> BeHidden(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Attributes.HasFlag(FileAttributes.Hidden), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not hidden
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> NotBeHidden(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Attributes.HasFlag(FileAttributes.Hidden), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is empty
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> BeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Length == 0, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not empty
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<FileInfoContract> NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Length != 0, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}