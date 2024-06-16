using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using FluentContracts.Infrastructure;

namespace FluentContracts.Validators;

internal static partial class Validator
{
    
    private static readonly Regex _hexadecimalRegex = new Regex(@"^[0-9A-Fa-f]+$", RegexOptions.Compiled);
    
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
            ParseOptions.Hexadecimal => _hexadecimalRegex.IsMatch(value),
            ParseOptions.Base64 => 
                Convert.TryFromBase64String(
                    value, 
                    new Span<byte>(new byte[value.Length]), 
                    out _),
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

    public static void CheckForCreditCardNumber(
        string argumentValue,
        string argumentName,
        string? message = null)
    {
        if (IsValidCreditCardNumber(argumentValue)) return;
        
        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForNotCreditCardNumber(
        string argumentValue,
        string argumentName,
        string? message = null)
    {
        if (!IsValidCreditCardNumber(argumentValue)) return;
        
        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    // Luhn algorithm code adapted from System.ComponentModel.DataAnnotations.CreditCardAttribute
    // Source: https://github.com/dotnet/runtime/blob/0588f245dba6418349c22894b40337afc34f1cfc/src/libraries/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/CreditCardAttribute.cs
    private static bool IsValidCreditCardNumber(string value)
    {
        int checksum = 0;
        bool evenDigit = false;

        for (int i = value.Length - 1; i >= 0; i--)
        {
            char digit = value[i];
            if (!char.IsDigit(digit))
            {
                // Skipping separators
                if (digit is '-' or ' ')
                {
                    continue;
                }

                return false;
            }

            int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
            evenDigit = !evenDigit;

            while (digitValue > 0)
            {
                checksum += digitValue % 10;
                digitValue /= 10;
            }
        }

        return (checksum % 10) == 0;
    }
}