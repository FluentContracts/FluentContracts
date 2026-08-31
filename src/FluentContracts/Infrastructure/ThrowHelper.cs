using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FluentContracts.Infrastructure;

internal static class ThrowHelper
{
    [DoesNotReturn]
    public static void ThrowArgumentOutOfRangeException(string argumentName, string? message = null)
    {
        throw new ArgumentOutOfRangeException(argumentName, message);
    }

    [DoesNotReturn]
    public static void ThrowArgumentNullException(string argumentName, string? message = null)
    {
        throw new ArgumentNullException(argumentName, message);
    }

    [DoesNotReturn]
    public static void ThrowUserDefinedException<TException>()
        where TException : Exception, new()
    {
        throw new TException();
    }

    [DoesNotReturn]
    public static void ThrowUserDefinedException<TException>(string message)
        where TException : Exception, new()
    {
        throw CreateUserDefinedException<TException>(message);
    }

    /// <summary>
    /// Builds <typeparamref name="TException"/> carrying <paramref name="message"/>, preferring the
    /// conventional <c>(string message)</c> constructor.
    /// </summary>
    /// <remarks>
    /// Constructing the exception properly matters: assigning the message to a field would skip the
    /// constructor, so anything it does — deriving an error code, validating, setting other members —
    /// would silently not happen, and an exception that stores its message itself rather than deferring
    /// to <see cref="Exception"/> would lose the message entirely.
    /// <para>
    /// When no such constructor exists there is nothing to call, so fall back to writing the private
    /// field on <see cref="Exception"/>, which is at least no worse than losing the message.
    /// </para>
    /// </remarks>
    private static TException CreateUserDefinedException<TException>(string message)
        where TException : Exception, new()
    {
        var messageConstructor = typeof(TException).GetConstructor([typeof(string)]);

        if (messageConstructor != null)
            return (TException)messageConstructor.Invoke([message]);

        var exception = new TException();

        var messageField =
            typeof(Exception).GetField(
                "_message",
                BindingFlags.NonPublic | BindingFlags.Instance);

        messageField?.SetValue(exception, message);

        return exception;
    }
}