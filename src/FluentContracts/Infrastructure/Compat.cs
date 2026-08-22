using System.Net.Mail;

namespace FluentContracts.Infrastructure;

/// <summary>
/// Compatibility helpers that keep a single code path across all target frameworks.
/// On modern targets these forward straight to the BCL; on <c>netstandard2.0</c> they
/// provide behaviourally equivalent fallbacks for APIs that framework does not expose.
/// </summary>
internal static class Compat
{
    /// <summary>
    /// Determines whether <paramref name="value"/> is a valid ASCII character.
    /// </summary>
    public static bool IsAscii(char value)
    {
#if NETSTANDARD2_0
        return value <= '\x007f';
#else
        return char.IsAscii(value);
#endif
    }

    /// <summary>
    /// Determines whether <paramref name="source"/> contains <paramref name="value"/>
    /// using the specified <paramref name="comparison"/>.
    /// </summary>
    public static bool Contains(string source, string value, StringComparison comparison)
    {
#if NETSTANDARD2_0
        return source.IndexOf(value, comparison) >= 0;
#else
        return source.Contains(value, comparison);
#endif
    }

    /// <summary>
    /// Determines whether <paramref name="value"/> is a valid Base64 encoded string.
    /// </summary>
    public static bool IsBase64(string value)
    {
#if NETSTANDARD2_0
        try
        {
            Convert.FromBase64String(value);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
#else
        return Convert.TryFromBase64String(value, new Span<byte>(new byte[value.Length]), out _);
#endif
    }

    /// <summary>
    /// Determines whether <paramref name="value"/> is a valid e-mail address.
    /// </summary>
    public static bool IsMailAddress(string value)
    {
#if NETSTANDARD2_0
        try
        {
            _ = new MailAddress(value);
            return true;
        }
        catch (Exception e) when (e is FormatException or ArgumentException)
        {
            return false;
        }
#else
        return MailAddress.TryCreate(value, out _);
#endif
    }
}
