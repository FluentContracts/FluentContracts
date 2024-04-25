using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts;

public class NullableCharContract(char? argumentValue, string argumentName)
    : BaseContract<char?>(argumentValue, argumentName) {}

public class CharContract(char argumentValue, string argumentName)
    : NumberContract<char>(argumentValue, argumentName)
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
    /// Checks if the value of the <see cref="char"/> argument is not a digit
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
    /// Checks if the value of the <see cref="char"/> argument is not a letter
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> NotBeLetter(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsLetter(v), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is lowercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> BeLowercase(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsLower, ArgumentValue, ArgumentName, message);
        return Linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not lowercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> NotBeLowercase(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsLower(v), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is uppercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> BeUppercase(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsUpper, ArgumentValue, ArgumentName, message);
        return Linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not uppercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> NotBeUppercase(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsUpper(v), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a whitespace
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> BeWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsWhiteSpace, ArgumentValue, ArgumentName, message);
        return Linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not a whitespace
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> NotBeWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsWhiteSpace(v), ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is an ASCII character
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> BeAscii(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsAscii, ArgumentValue, ArgumentName, message);
        return Linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not an ASCII character
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<char> NotBeAscii(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsAscii(v), ArgumentValue, ArgumentName, message);
        return Linker;
    }
}