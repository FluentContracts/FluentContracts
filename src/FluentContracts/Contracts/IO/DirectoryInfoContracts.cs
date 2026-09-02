using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.IO;

/// <summary>
/// The inheritable contract for a <see cref="System.IO.DirectoryInfo"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
public class DirectoryInfoContract : NullableContract<DirectoryInfo, DirectoryInfoContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    public DirectoryInfoContract(DirectoryInfo? argumentValue, string argumentName) 
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the file, described by the argument, exists
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public DirectoryInfoContract Exist(string? message = null)
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
    public DirectoryInfoContract NotExist(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Exists, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is read-only
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public DirectoryInfoContract BeReadOnly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Attributes.HasFlag(FileAttributes.ReadOnly), ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not read-only
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public DirectoryInfoContract NotBeReadOnly(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Attributes.HasFlag(FileAttributes.ReadOnly), ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is hidden
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public DirectoryInfoContract BeHidden(string? message = null)
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
    public DirectoryInfoContract NotBeHidden(string? message = null)
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
    public DirectoryInfoContract BeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.GetFiles().Length + a.GetDirectories().Length == 0, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the file, described by the argument, is not empty
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public DirectoryInfoContract NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.GetFiles().Length + a.GetDirectories().Length > 0, ArgumentValue, ArgumentName, message);
        return this;
    }
}
