using System.Text.RegularExpressions;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Text;

/// <summary>
/// The entry point for checks on a <see cref="string"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class StringContract(string? argumentValue, string argumentName)
    : StringContract<StringContract>(argumentValue, argumentName);

/// <summary>
/// The inheritable contract for a <see cref="string"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class StringContract<TContract> : EqualityContract<string?, TContract>
    where TContract : StringContract<TContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected StringContract(string? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeEmpty(string? message = null)
    {
        Validator.CheckForSpecificValue(string.Empty, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotSpecificValue(string.Empty, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is null or empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeNullOrEmpty(string? message = null)
    {
        Validator.CheckGenericCondition(string.IsNullOrEmpty, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not null or empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeNullOrEmpty(string? message = null)
    {
        Validator.CheckGenericCondition(a => !string.IsNullOrEmpty(a), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWhiteSpace(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(string.IsNullOrWhiteSpace, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWhiteSpace(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => !string.IsNullOrWhiteSpace(a), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is null or whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract BeNullOrWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(string.IsNullOrWhiteSpace, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not null or whitespace(s).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    public TContract NotBeNullOrWhiteSpace(string? message = null)
    {
        Validator.CheckGenericCondition(a => !string.IsNullOrWhiteSpace(a), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is uppercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeUppercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(ArgumentValue.ToUpperInvariant(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not uppercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeUppercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(ArgumentValue.ToUpperInvariant(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is lowercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLowercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(ArgumentValue.ToLowerInvariant(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not lowercase.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeLowercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(ArgumentValue.ToLowerInvariant(), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if <paramref name="containedString"/> is part of the value of the <see cref="string"/> argument.
    /// </summary>
    /// <param name="containedString">A string to check for being part of the argument</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.Ordinal"/> — case-sensitive; pass <see cref="StringComparison.OrdinalIgnoreCase"/> to ignore case.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract Contain(
        string containedString,
        StringComparison comparisonType = StringComparison.Ordinal,
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => Compat.Contains(a, containedString, comparisonType),
            ArgumentValue, ArgumentName, message,
            expectation: $"contain {Validator.Describe(containedString)}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if <paramref name="containedString"/> is not part of the value of the <see cref="string"/> argument.
    /// </summary>
    /// <param name="containedString">A string to check for being part of the argument</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.Ordinal"/> — case-sensitive; pass <see cref="StringComparison.OrdinalIgnoreCase"/> to ignore case.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotContain(
        string containedString,
        StringComparison comparisonType = StringComparison.Ordinal,
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => !Compat.Contains(a, containedString, comparisonType),
            ArgumentValue, ArgumentName, message,
            expectation: $"not contain {Validator.Describe(containedString)}");
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid email address.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeEmailAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForParsed(ParseOptions.EmailAddress, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid email address.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeEmailAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotParsed(ParseOptions.EmailAddress, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a match against a regex pattern
    /// </summary>
    /// <param name="pattern">The regex pattern to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeMatching(string pattern, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            a => Regex.IsMatch(a, pattern, RegexOptions.CultureInvariant),
            ArgumentValue,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"match the pattern {Validator.Describe(pattern)}");
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a match against a regex pattern
    /// </summary>
    /// <param name="unexpectedPattern">The regex pattern to NOT match the string</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeMatching(string unexpectedPattern, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            a => !Regex.IsMatch(a, unexpectedPattern, RegexOptions.CultureInvariant),
            ArgumentValue,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"not match the pattern {Validator.Describe(unexpectedPattern)}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is starting with a specific value
    /// </summary>
    /// <param name="startingWith">Value that the argument must start with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.Ordinal"/> — case-sensitive; pass <see cref="StringComparison.OrdinalIgnoreCase"/> to ignore case.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract StartWith(
        string startingWith, 
        StringComparison comparisonType = StringComparison.Ordinal, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            a => a.StartsWith(startingWith, comparisonType),
            ArgumentValue,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"start with {Validator.Describe(startingWith)}");
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not starting with a specific value
    /// </summary>
    /// <param name="startingWith">Value that the argument must not start with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.Ordinal"/> — case-sensitive; pass <see cref="StringComparison.OrdinalIgnoreCase"/> to ignore case.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotStartWith(
        string startingWith, 
        StringComparison comparisonType = StringComparison.Ordinal, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            a => !a.StartsWith(startingWith, comparisonType),
            ArgumentValue,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"not start with {Validator.Describe(startingWith)}");
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is ending with a specific value
    /// </summary>
    /// <param name="endingWith">Value that the argument must end with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.Ordinal"/> — case-sensitive; pass <see cref="StringComparison.OrdinalIgnoreCase"/> to ignore case.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract EndWith(
        string endingWith, 
        StringComparison comparisonType = StringComparison.Ordinal, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            a => a.EndsWith(endingWith, comparisonType),
            ArgumentValue,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"end with {Validator.Describe(endingWith)}");
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not ending with a specific value
    /// </summary>
    /// <param name="endingWith">Value that the argument must not end with</param>
    /// <param name="comparisonType">Comparison type to use. Default: <see cref="StringComparison.Ordinal"/> — case-sensitive; pass <see cref="StringComparison.OrdinalIgnoreCase"/> to ignore case.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotEndWith(
        string endingWith, 
        StringComparison comparisonType = StringComparison.Ordinal, 
        string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(
            a => !a.EndsWith(endingWith, comparisonType),
            ArgumentValue,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"not end with {Validator.Describe(endingWith)}");
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a palindrome (if you reverse it, the string is the same)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BePalindrome(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForPalindrome(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a palindrome (if you reverse it, the string is the same)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBePalindrome(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotPalindrome(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid URL
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeUrl(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForParsed(ParseOptions.Url, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid URL
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeUrl(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotParsed(ParseOptions.Url, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract HaveLengthEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(length, ArgumentValue.Length, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is not equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotHaveLengthEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(length, ArgumentValue.Length, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is greater than <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract HaveLengthGreaterThan(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterThan(length, ArgumentValue.Length, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is greater or equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract HaveLengthGreaterOrEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForGreaterOrEqualTo(length, ArgumentValue.Length, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is less than <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract HaveLengthLessThan(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessThan(length, ArgumentValue.Length, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is less or equal to <paramref name="length"/>
    /// </summary>
    /// <param name="length">Length to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract HaveLengthLessOrEqualTo(int length, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForLessOrEqualTo(length, ArgumentValue.Length, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the length of the <see cref="string"/> argument is inclusively between <paramref name="start"/> and <paramref name="end"/>
    /// </summary>
    /// <param name="start">Start of range</param>
    /// <param name="end">End of range</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract HaveLengthBetween(int start, int end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForBetween(start, end, ArgumentValue.Length, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is alphanumeric
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeAlphanumeric(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForAlphanumeric(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not alphanumeric
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeAlphanumeric(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotAlphanumeric(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid IP address
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeIpAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForParsed(ParseOptions.IpAddress, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid IP address
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeIpAddress(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotParsed(ParseOptions.IpAddress, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid GUID
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGuid(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForParsed(ParseOptions.Guid, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid GUID
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeGuid(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotParsed(ParseOptions.Guid, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeExistingFile(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(File.Exists, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeExistingFile(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => !File.Exists(a), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeExistingDirectory(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(Directory.Exists, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is an existing file path
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeExistingDirectory(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => !Directory.Exists(a), ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid base64
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeBase64(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForParsed(ParseOptions.Base64, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid base64
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeBase64(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotParsed(ParseOptions.Base64, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid hexadecimal (without 0x prefix)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeHexadecimal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForParsed(ParseOptions.Hexadecimal, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid hexadecimal (without 0x prefix)
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeHexadecimal(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotParsed(ParseOptions.Hexadecimal, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is a valid credit card number
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeCreditCardNumber(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForCreditCardNumber(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
    
    /// <summary>
    /// Checks if the value of the <see cref="string"/> argument is not a valid credit card number
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeCreditCardNumber(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotCreditCardNumber(ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }
}
