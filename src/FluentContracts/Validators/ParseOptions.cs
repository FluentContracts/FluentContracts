namespace FluentContracts.Validators;

/// <summary>
/// The formats a string can be asked to parse as.
/// </summary>
public enum ParseOptions
{
    /// <summary>An address that <see cref="System.Net.Mail.MailAddress"/> accepts.</summary>
    EmailAddress,

    /// <summary>An absolute URL.</summary>
    Url,

    /// <summary>An IPv4 or IPv6 address.</summary>
    IpAddress,

    /// <summary>A <see cref="System.Guid"/> in any of the formats it round-trips.</summary>
    Guid,

    /// <summary>Base64 text, as <see cref="System.Convert"/> decodes it.</summary>
    Base64,

    /// <summary>Hexadecimal digits, with no prefix and no separators.</summary>
    Hexadecimal
}
