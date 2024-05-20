using System.Net;
using System.Net.Mail;
using FluentContracts.Infrastructure;

namespace FluentContracts.Validators;

internal static partial class Validator
{
    public static void CheckForParsed(
        ParseOptions option,
        string argumentValue,
        string argumentName,
        string? message = null)
    {
        if (TryParse(option, argumentValue)) return;
        
        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForNotParsed(
        ParseOptions option,
        string argumentValue,
        string argumentName,
        string? message = null)
    {
        if (!TryParse(option, argumentValue)) return;
        
        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    private static bool TryParse(ParseOptions options, string value)
    {
        return options switch
        {
            ParseOptions.EmailAddress => MailAddress.TryCreate(value, out _),
            ParseOptions.Url => Uri.TryCreate(value, UriKind.Absolute, out _),
            ParseOptions.IpAddress => IPAddress.TryParse(value, out _),
            ParseOptions.Guid => Guid.TryParse(value, out _),
            _ => throw new ArgumentOutOfRangeException(nameof(options), options, null)
        };
    }
    
    public static void CheckForPalindrome(
        string argumentValue, 
        string argumentName, 
        string? message = null)
    {
        var lower = argumentValue.ToLower();
        var start = 0;
        var end = argumentValue.Length - 1;
        
        while (start < end)
        {
            if (!lower[start].Equals(lower[end]))
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
            }

            start++;
            end--;
        }
    }
    
    public static void CheckForNotPalindrome(
        string argumentValue, 
        string argumentName, 
        string? message = null)
    {
        var lower = argumentValue.ToLower();
        var start = 0;
        var end = argumentValue.Length - 1;
        
        while (start < end)
        {
            if (!lower[start].Equals(lower[end]))
            {
                return;
            }

            start++;
            end--;
        }
        
        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForAlphanumeric(
        string argumentValue, 
        string argumentName, 
        string? message = null)
    {
        if (argumentValue.All(char.IsLetterOrDigit)) return;
        
        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForNotAlphanumeric(
        string argumentValue, 
        string argumentName, 
        string? message = null)
    {
        if (argumentValue.Any(a => !char.IsLetterOrDigit(a))) return;
        
        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
}