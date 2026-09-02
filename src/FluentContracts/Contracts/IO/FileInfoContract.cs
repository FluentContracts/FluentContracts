using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.IO;

/// <summary>
/// The inheritable contract for a <see cref="System.IO.FileInfo"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
public class FileInfoContract : NullableContract<FileInfo, FileInfoContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    public FileInfoContract(FileInfo? argumentValue, string argumentName) 
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the file, described by the argument, exists
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract Exist(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Exists, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, does not exist
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract NotExist(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Exists, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the <see cref="FileInfo"/> argument has a specified extension
    /// </summary>
    /// <param name="extension">Extension to match</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract HaveExtension(string extension, string? message = null)
    {
        var extensionWithDot =
            extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                ? extension
                : "." + extension;
            
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Extension.Equals(extensionWithDot, StringComparison.OrdinalIgnoreCase), ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the <see cref="FileInfo"/> argument does not have a specified extension
    /// </summary>
    /// <param name="extension">Extension to match</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract NotHaveExtension(string extension, string? message = null)
    {
        var extensionWithDot =
            extension.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                ? extension
                : "." + extension;
            
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Extension.Equals(extensionWithDot, StringComparison.OrdinalIgnoreCase), ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is read-only
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract BeReadOnly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.IsReadOnly, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not read-only
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract NotBeReadOnly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.IsReadOnly, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is hidden
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract BeHidden(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Attributes.HasFlag(FileAttributes.Hidden), ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not hidden
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract NotBeHidden(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Attributes.HasFlag(FileAttributes.Hidden), ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is empty
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract BeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Length == 0, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not empty
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Length != 0, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, has size in bytes, equal to <paramref name="byteSize"/>
    /// </summary>
    /// <param name="byteSize">Size in bytes to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract HaveSizeEqualTo(long byteSize, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(byteSize, ArgumentValue.Length, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, has size in bytes, not equal to <paramref name="byteSize"/>
    /// </summary>
    /// <param name="byteSize">Size in bytes to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract NotHaveSizeEqualTo(long byteSize, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(byteSize, ArgumentValue.Length, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, has size in bytes, less than <paramref name="byteSize"/>
    /// </summary>
    /// <param name="byteSize">Size in bytes to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract HaveSizeLessThan(long byteSize, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(byteSize, ArgumentValue.Length, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, has size in bytes, less than or equal to <paramref name="byteSize"/>
    /// </summary>
    /// <param name="byteSize">Size in bytes to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract HaveSizeLessOrEqualTo(long byteSize, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(byteSize, ArgumentValue.Length, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, has size in bytes, greater than <paramref name="byteSize"/>
    /// </summary>
    /// <param name="byteSize">Size in bytes to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract HaveSizeGreaterThan(long byteSize, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(byteSize, ArgumentValue.Length, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, has size in bytes, greater than or equal to <paramref name="byteSize"/>
    /// </summary>
    /// <param name="byteSize">Size in bytes to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public FileInfoContract HaveSizeGreaterOrEqualTo(long byteSize, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(byteSize, ArgumentValue.Length, ArgumentName, message);
        return this;
    }
}
