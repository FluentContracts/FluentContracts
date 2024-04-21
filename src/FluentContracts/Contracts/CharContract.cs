using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts;

public class NullableCharContract(char? argumentValue, string argumentName)
    : BaseContract<char?>(argumentValue, argumentName) {}

public class CharContract(char argumentValue, string argumentName)
    : BaseContract<char>(argumentValue, argumentName)
{
    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a digit
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> BeDigit(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsDigit, ArgumentValue, ArgumentName, message);
        return Linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a digit
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> NotBeDigit(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsDigit(v), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a letter
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> BeLetter(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsLetter, ArgumentValue, ArgumentName, message);
        return Linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a letter
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> NotBeLetter(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsLetter(v), ArgumentValue, ArgumentName, message);
        return Linker;
    }
}