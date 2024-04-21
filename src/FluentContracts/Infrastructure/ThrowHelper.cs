using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FluentContracts.Infrastructure
{
    internal static class ThrowHelper
    {
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentOutOfRangeException(string argumentName, string? message = null)
        {
            throw new ArgumentOutOfRangeException(argumentName, message);
        }

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentNullException(string argumentName, string? message = null)
        {
            throw new ArgumentNullException(argumentName, message);
        }
        
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowUserDefinedException<TException>()
            where TException : Exception, new()
        {
            throw new TException();
        }
        
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowUserDefinedException<TException>([NotNull] string message)
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
        
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowUserDefinedException<TException>([NotNull] string message, [NotNull] Func<string, TException> exceptionFactory)
            where TException : Exception, new()
        {
            throw exceptionFactory(message);
        }
    }
}