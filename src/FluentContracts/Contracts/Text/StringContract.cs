using System.Text.RegularExpressions;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Text;

public class StringContract(string? argumentValue, string argumentName)
    : StringContract<StringContract>(argumentValue, argumentName);

public class StringContract<TContract> : ComparableContract<string?, TContract>
    where TContract : StringContract<TContract>
{
    private readonly Linker<TContract> _linker;

    protected StringContract(string? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeEmpty(string? message = null)
    {
        Validator.CheckForSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotSpecificValue(string.Empty, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is null or empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeNullOrEmpty(string? message = null)
    {
        Validator.CheckGenericCondition(string.IsNullOrEmpty, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not null or empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeNullOrEmpty(string? message = null)
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
    public Linker<TContract> BeWhiteSpace(string? message = null)
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
    public Linker<TContract> NotBeWhiteSpace(string? message = null)
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
    public Linker<TContract> BeNullOrWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(string.IsNullOrWhiteSpace, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not null or whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeNullOrWhiteSpace(string? message = null)
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
    public Linker<TContract> BeUppercase(string? message = null)
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
    public Linker<TContract> NotBeUppercase(string? message = null)
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
    public Linker<TContract> BeLowercase(string? message = null)
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
    public Linker<TContract> NotBeLowercase(string? message = null)
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
    public Linker<TContract> Contain(string containedString, string? message = null)
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
    public Linker<TContract> NotContain(string containedString, string? message = null)
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
    public Linker<TContract> BeEmailAddress(string? message = null)
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
    public Linker<TContract> NotBeEmailAddress(string? message = null)
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
    public Linker<TContract> BeMatching(string pattern, string? message = null)
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
    public Linker<TContract> NotBeMatching(string unexpectedPattern, string? message = null)
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
    public Linker<TContract> StartWith(
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
    public Linker<TContract> NotStartWith(
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
    public Linker<TContract> EndWith(
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
    public Linker<TContract> NotEndWith(
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
    public Linker<TContract> BePalindrome(string? message = null)
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
    public Linker<TContract> NotBePalindrome(string? message = null)
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
    public Linker<TContract> BeUrl(string? message = null)
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
    public Linker<TContract> NotBeUrl(string? message = null)
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
    public Linker<TContract> HaveLengthEqualTo(int length, string? message = null)
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
    public Linker<TContract> NotHaveLengthEqualTo(int length, string? message = null)
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
    public Linker<TContract> HaveLengthGreaterThan(int length, string? message = null)
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
    public Linker<TContract> HaveLengthGreaterOrEqualTo(int length, string? message = null)
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
    public Linker<TContract> HaveLengthLessThan(int length, string? message = null)
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
    public Linker<TContract> HaveLengthLessOrEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(length, ArgumentValue.Length, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is inclusively between <paramref name="start"/> and <paramref name="end"/>
    /// </summary>
    /// <param name="start">Start of range</param>
    /// <param name="end">End of range</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveLengthBetween(int start, int end, string? message = null)
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
    public Linker<TContract> BeAlphanumeric(string? message = null)
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
    public Linker<TContract> NotBeAlphanumeric(string? message = null)
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
    public Linker<TContract> BeIpAddress(string? message = null)
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
    public Linker<TContract> NotBeIpAddress(string? message = null)
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
    public Linker<TContract> BeGuid(string? message = null)
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
    public Linker<TContract> NotBeGuid(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotParsed(ParseOptions.Guid, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeExistingFile(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(File.Exists, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeExistingFile(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !File.Exists(a), ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeExistingDirectory(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(Directory.Exists, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeExistingDirectory(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !Directory.Exists(a), ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid base64
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeBase64(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForParsed(ParseOptions.Base64, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid base64
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeBase64(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotParsed(ParseOptions.Base64, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid hexadecimal (without 0x prefix)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeHexadecimal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForParsed(ParseOptions.Hexadecimal, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid hexadecimal (without 0x prefix)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeHexadecimal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotParsed(ParseOptions.Hexadecimal, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid credit card number
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeCreditCardNumber(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForCreditCardNumber(ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid credit card number
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeCreditCardNumber(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotCreditCardNumber(ArgumentValue, ArgumentName, message);
        return _linker;
    }
}
