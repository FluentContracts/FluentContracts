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
        var ex = Activator.CreateInstance<TException>();

        var internalFieldInfo =
            typeof(TException).GetField(
                "_message",
                BindingFlags.NonPublic | BindingFlags.Instance);

        if (internalFieldInfo != null)
            internalFieldInfo.SetValue(ex, message);

        throw ex;
    }
}