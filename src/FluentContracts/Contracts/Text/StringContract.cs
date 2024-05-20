using System.Text.RegularExpressions;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Text;

public class StringContract : ComparableContract<string?, StringContract>
{
    private readonly Linker<StringContract> _linker;

    public StringContract(string argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<StringContract>(this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<StringContract> BeEmpty(string? message = null)
    {
        Validator.CheckForSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<StringContract> NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is null or empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<StringContract> BeNullOrEmpty(string? message = null)
    {
        Validator.CheckGenericCondition(string.IsNullOrEmpty, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not null or empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<StringContract> NotBeNullOrEmpty(string? message = null)
    {
        Validator.CheckGenericCondition(a => !string.IsNullOrEmpty(a), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeWhiteSpace(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(string.IsNullOrWhiteSpace, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeWhiteSpace(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !string.IsNullOrWhiteSpace(a), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is null or whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<StringContract> BeNullOrWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(string.IsNullOrWhiteSpace, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not null or whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<StringContract> NotBeNullOrWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(a => !string.IsNullOrWhiteSpace(a), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is uppercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeUppercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(ArgumentValue.ToUpperInvariant(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not uppercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeUppercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(ArgumentValue.ToUpperInvariant(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is lowercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeLowercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(ArgumentValue.ToLowerInvariant(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not lowercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeLowercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(ArgumentValue.ToLowerInvariant(), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if <see cref="containedString"/> is part of the value of the <see cref="string"/> argument.
    /// </summary>
    /// <param name="containedString">A string to check for being part of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> Contain(string containedString, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.Contains(containedString, StringComparison.OrdinalIgnoreCase),
            ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if <see cref="containedString"/> is not part of the value of the <see cref="string"/> argument.
    /// </summary>
    /// <param name="containedString">A string to check for being part of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotContain(string containedString, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.Contains(containedString, StringComparison.OrdinalIgnoreCase),
            ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid email address.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeEmailAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForParsed(ParseOptions.EmailAddress, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid email address.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeEmailAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotParsed(ParseOptions.EmailAddress, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a match against a regex pattern
    /// </summary>
    /// <param name="pattern">The regex pattern to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeMatching(string pattern, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            a => Regex.IsMatch(a, pattern, RegexOptions.CultureInvariant), 
            ArgumentValue,
            ArgumentName, 
            message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a match against a regex pattern
    /// </summary>
    /// <param name="unexpectedPattern">The regex pattern to NOT match the string</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeMatching(string unexpectedPattern, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            a => !Regex.IsMatch(a, unexpectedPattern, RegexOptions.CultureInvariant), 
            ArgumentValue,
            ArgumentName, 
            message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is starting with a specific value
    /// </summary>
    /// <param name="startingWith">Value that the argument must start with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.OrdinalIgnoreCase"/></param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> StartWith(
        string startingWith, 
        StringComparison comparisonType = StringComparison.OrdinalIgnoreCase, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            a => a.StartsWith(startingWith, comparisonType), 
            ArgumentValue,
            ArgumentName, 
            message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not starting with a specific value
    /// </summary>
    /// <param name="startingWith">Value that the argument must not start with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.OrdinalIgnoreCase"/></param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotStartWith(
        string startingWith, 
        StringComparison comparisonType = StringComparison.OrdinalIgnoreCase, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            a => !a.StartsWith(startingWith, comparisonType), 
            ArgumentValue,
            ArgumentName, 
            message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is ending with a specific value
    /// </summary>
    /// <param name="endingWith">Value that the argument must end with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.OrdinalIgnoreCase"/></param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> EndWith(
        string endingWith, 
        StringComparison comparisonType = StringComparison.OrdinalIgnoreCase, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            a => a.EndsWith(endingWith, comparisonType), 
            ArgumentValue,
            ArgumentName, 
            message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not ending with a specific value
    /// </summary>
    /// <param name="endingWith">Value that the argument must not end with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.OrdinalIgnoreCase"/></param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotEndWith(
        string endingWith, 
        StringComparison comparisonType = StringComparison.OrdinalIgnoreCase, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(
            a => !a.EndsWith(endingWith, comparisonType), 
            ArgumentValue,
            ArgumentName, 
            message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a palindrome (if you reverse it, the string is the same)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BePalindrome(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForPalindrome(ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a palindrome (if you reverse it, the string is the same)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBePalindrome(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotPalindrome(ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid URL
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeUrl(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForParsed(ParseOptions.Url, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid URL
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeUrl(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotParsed(ParseOptions.Url, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> HaveLengthEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(length, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is not equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotHaveLengthEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(length, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is greater than <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> HaveLengthGreaterThan(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(length, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is greater or equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> HaveLengthGreaterOrEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(length, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is less than <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> HaveLengthLessThan(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(length, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is less or equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> HaveLengthLessOrEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(length, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is inclusively between <paramref name="start"/> and <paramref name="end"/>
    /// </summary>
    /// <param name="start">Start of range</param>
    /// <param name="end">end of range</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> HaveLengthBetween(int start, int end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForBetween(start, end, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is alphanumeric
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeAlphanumeric(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForAlphanumeric(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not alphanumeric
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeAlphanumeric(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotAlphanumeric(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid IP address
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeIpAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForParsed(ParseOptions.IpAddress, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid IP address
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeIpAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotParsed(ParseOptions.IpAddress, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid GUID
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> BeGuid(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForParsed(ParseOptions.Guid, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid GUID
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<StringContract> NotBeGuid(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotParsed(ParseOptions.Guid, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}
