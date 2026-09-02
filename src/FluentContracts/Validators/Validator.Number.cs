#if NET8_0_OR_GREATER
using System.Numerics;
using FluentContracts.Infrastructure;

namespace FluentContracts.Validators;

// The NaN and infinity checks for any INumberBase<T>. The switch-based ones in Validator.cs only
// know double and float, which is all the hand-written contracts need; the generic number contract
// asks the type itself, so Half, NFloat and a user's own number type get the same policy.
internal static partial class Validator
{
    /// <summary>
    /// The NaN guard of the ordering checks for a generic number: an ordering comparison with NaN is
    /// never true, so an argument that is NaN fails before the comparison is made.
    /// </summary>
    public static void CheckForNumberNotNaN<T>(T argumentValue, string argumentName, string? message = null)
        where T : INumberBase<T>
    {
        if (!T.IsNaN(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            Custom(message, argumentName, argumentValue) ?? Expected(argumentName, "not be NaN"));
    }

    public static void CheckForNumberNaN<T>(T argumentValue, string argumentName, string? message = null)
        where T : INumberBase<T>
    {
        if (T.IsNaN(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            Custom(message, argumentName, argumentValue) ?? Expected(argumentName, "be NaN", argumentValue));
    }

    public static void CheckForNumberInfinity<T>(T argumentValue, string argumentName, string? message = null)
        where T : INumberBase<T>
    {
        if (T.IsInfinity(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            Custom(message, argumentName, argumentValue) ?? Expected(argumentName, "be infinity", argumentValue));
    }

    public static void CheckForNumberNotInfinity<T>(T argumentValue, string argumentName, string? message = null)
        where T : INumberBase<T>
    {
        if (!T.IsInfinity(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            Custom(message, argumentName, argumentValue) ?? Expected(argumentName, "not be infinity", argumentValue));
    }

    public static void CheckForNumberFinite<T>(T argumentValue, string argumentName, string? message = null)
        where T : INumberBase<T>
    {
        if (T.IsFinite(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            Custom(message, argumentName, argumentValue) ?? Expected(argumentName, "be finite", argumentValue));
    }
}
#endif
