using System.Runtime.CompilerServices;
using FluentContracts.Exceptions;

namespace FluentContracts.Infrastructure
{
    internal static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentOutOfRangeException(string argumentName, string? message = null)
        {
            throw new ArgumentOutOfRangeException(argumentName, message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowInvalidArgumentValueException(string argumentName, string? message = null)
        {
            throw new InvalidArgumentValueException(argumentName, message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowInvalidArgumentConditionNotSatisfiedException(string argumentName, string? message = null)
        {
            throw new ArgumentConditionNotSatisfiedException(argumentName, message);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentNullException(string argumentName, string? message = null)
        {
            throw new ArgumentNullException(argumentName, message);
        }
    }
}