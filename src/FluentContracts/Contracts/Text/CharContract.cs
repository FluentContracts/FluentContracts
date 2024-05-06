using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts.Text;

public class NullableCharContract(char? argumentValue, string argumentName)
    : ComparableContract<char?, NullableCharContract>(argumentValue, argumentName) {}

public class CharContract : ComparableContract<char, CharContract>
{
    private readonly Linker<CharContract> _linker;
    
    public CharContract(char argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<CharContract>(this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a digit
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> BeDigit(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsDigit, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not a digit
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> NotBeDigit(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsDigit(v), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a letter
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> BeLetter(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsLetter, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not a letter
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> NotBeLetter(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsLetter(v), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is lowercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> BeLowercase(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsLower, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not lowercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> NotBeLowercase(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsLower(v), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is uppercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> BeUppercase(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsUpper, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not uppercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> NotBeUppercase(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsUpper(v), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a whitespace
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> BeWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsWhiteSpace, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not a whitespace
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> NotBeWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsWhiteSpace(v), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is an ASCII character
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> BeAscii(string? message = null)
    {
        Validator.CheckGenericCondition(char.IsAscii, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not an ASCII character
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    [Pure]
    public Linker<CharContract> NotBeAscii(string? message = null)
    {
        Validator.CheckGenericCondition(v => !char.IsAscii(v), ArgumentValue, ArgumentName, message);
        return _linker;
    }
}