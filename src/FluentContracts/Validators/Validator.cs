using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using FluentContracts.Infrastructure;

namespace FluentContracts.Validators;

// Hidden from stack traces and the debugger for the same reason as ThrowHelper: a contract
// failure should surface at the check the caller wrote. The attributes cover every partial.
[StackTraceHidden]
[DebuggerStepThrough]
internal static partial class Validator
{
    public static void CheckForBeType<TArgument, TCheck>(
        TArgument argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue is TCheck) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be of type {typeof(TCheck)}", argumentValue?.GetType()));
    }

    public static void CheckForBeType<TArgument>(
        Type type,
        TArgument argumentValue,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);

        if (argumentValue.GetType() == type) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be of type {type}", argumentValue.GetType()));
    }

    public static void CheckForNotBeType<TArgument, TCheck>(
        TArgument argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue is not TCheck) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not be of type {typeof(TCheck)}", argumentValue.GetType()));
    }

    public static void CheckForNotBeType<TArgument>(
        Type type,
        TArgument argumentValue,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);

        if (argumentValue.GetType() != type) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not be of type {type}"));
    }

    public static void CheckForBeAssignableTo<TArgument>(
        [NotNull] TArgument argumentValue,
        Type targetType,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);

        if (targetType.IsAssignableFrom(argumentValue.GetType())) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be assignable to {targetType}", argumentValue.GetType()));
    }

    public static void CheckForNotBeAssignableTo<TArgument>(
        [NotNull] TArgument argumentValue,
        Type targetType,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);

        if (!targetType.IsAssignableFrom(argumentValue.GetType())) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not be assignable to {targetType}", argumentValue.GetType()));
    }
    
    /// <summary>
    /// Returns <paramref name="argumentValue"/> as <typeparamref name="T"/>, failing the contract when
    /// it is something else. Casting directly would surface an <see cref="InvalidCastException"/> from
    /// inside the library rather than a contract failure naming the argument.
    /// </summary>
    public static T CheckForTypeAndConvert<TArgument, T>(
        TArgument argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue is T typedValue) return typedValue;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be of type {typeof(T)}", argumentValue?.GetType()));
        return default!;
    }

    /// <inheritdoc cref="CheckForTypeAndConvert{TArgument,T}"/>
    public static T CheckForTypeAndConvert<TArgument, T, TException>(
        TArgument argumentValue,
        string? message = null)
        where TException : Exception, new()
    {
        if (argumentValue is T typedValue) return typedValue;

        if (message is null)
            ThrowHelper.ThrowUserDefinedException<TException>();

        ThrowHelper.ThrowUserDefinedException<TException>(message);
        return default!;
    }

    public static void CheckForNotNull<T, TException>([NotNull] T? value)
        where TException : Exception, new()
    {
        if (value is not null) return;

        ThrowHelper.ThrowUserDefinedException<TException>();
    }

    public static void CheckForNotNull<T, TException>(
        [NotNull] T? argumentValue, 
        string message)
        where TException : Exception, new()
    {
        if (argumentValue is not null) return;

        ThrowHelper.ThrowUserDefinedException<TException>(message);
    }

    public static void CheckForNotNull<T>(
        [NotNull] T value,
        string argumentName,
        string? message = null)
    {
        if (value is not null) return;

        ThrowHelper.ThrowArgumentNullException(
            argumentName,
            message ?? Expected(argumentName, "not be null"));
    }

    public static void CheckForNull<T>(
        T? value,
        string argumentName,
        string? message = null)
    {
        if (value is null) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "be null", value));
    }

    /// <summary>
    /// The catch-all check for conditions with no dedicated validator. The default failure message
    /// names the expectation: <paramref name="expectation"/> when the check has operands only the
    /// call site knows (<c>start with "abc"</c>), otherwise the check's own name humanised —
    /// <c>NotBeUppercase</c> fails as <c>Expected x to not be uppercase</c> — via
    /// <paramref name="checkName"/>, which the compiler fills with the calling check's name.
    /// </summary>
    public static void CheckGenericCondition<T>(
        Func<T, bool> genericCondition,
        T argumentValue,
        string argumentName,
        string? message = null,
        string? expectation = null,
        [CallerMemberName] string checkName = "")
    {
        if (genericCondition(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, expectation ?? HumaniseCheckName(checkName), argumentValue));
    }

    public static void CheckGenericCondition<T, TException>(
        Func<T, bool> genericCondition, 
        [NotNull] T argumentValue)
        where TException : Exception, new()
    {
        CheckForNotNull<T, TException>(argumentValue);
        
        if (genericCondition(argumentValue)) return;

        ThrowHelper.ThrowUserDefinedException<TException>();
    }

    public static void CheckGenericCondition<T, TException>(
        Func<T, bool> genericCondition, 
        [NotNull] T argumentValue, 
        string message)
        where TException : Exception, new()
    {
        CheckForNotNull<T, TException>(argumentValue, message);
        
        if (genericCondition(argumentValue)) return;

        ThrowHelper.ThrowUserDefinedException<TException>(message);
    }

    public static void CheckForAnyOf<T>(
        IEnumerable<T> values,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        // Materialised because the failure message enumerates it a second time, and the caller may
        // have handed over a one-shot enumerable.
        var candidates = values as IReadOnlyCollection<T> ?? values.ToArray();

        if (candidates.Any(v => v.IsEqualTo(argumentValue))) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be any of {Describe(candidates)}", argumentValue));
    }

    public static void CheckForNotAnyOf<T>(
        IEnumerable<T> values,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        var candidates = values as IReadOnlyCollection<T> ?? values.ToArray();

        if (candidates.All(v => v.IsNotEqualTo(argumentValue))) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not be any of {Describe(candidates)}", argumentValue));
    }

    public static void CheckForSpecificValue<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (value.IsEqualTo(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be {Describe(value)}", argumentValue));
    }

    public static void CheckForNotSpecificValue<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (value.IsNotEqualTo(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not be {Describe(value)}"));
    }

    /// <summary>
    /// Ordering comparisons reject <see cref="double.NaN"/> the way they reject null.
    /// </summary>
    /// <remarks>
    /// <see cref="Comparer{T}"/> is a total order and sorts NaN below every other value, so a check
    /// asking whether the argument is less than something was satisfied by NaN. IEEE says every
    /// ordering comparison with NaN is false, so no ordering check can be satisfied by one.
    /// </remarks>
    public static void CheckForNotNaN<T>(T argumentValue, string argumentName, string? message = null)
    {
        if (!IsNaN(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "not be NaN"));
    }

    public static void CheckForNaN<T>(T argumentValue, string argumentName, string? message = null)
    {
        if (IsNaN(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "be NaN", argumentValue));
    }

    public static void CheckForInfinity<T>(T argumentValue, string argumentName, string? message = null)
    {
        if (IsInfinity(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "be infinity", argumentValue));
    }

    public static void CheckForNotInfinity<T>(T argumentValue, string argumentName, string? message = null)
    {
        if (!IsInfinity(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "not be infinity", argumentValue));
    }

    public static void CheckForFinite<T>(T argumentValue, string argumentName, string? message = null)
    {
        if (!IsNaN(argumentValue) && !IsInfinity(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "be finite", argumentValue));
    }

    public static void CheckForNotFinite<T>(T argumentValue, string argumentName, string? message = null)
    {
        if (IsNaN(argumentValue) || IsInfinity(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "not be finite", argumentValue));
    }

    // A contract holds its value as a nullable, so the pattern sees the boxed underlying type.
    private static bool IsNaN<T>(T value) =>
        value switch
        {
            double d => double.IsNaN(d),
            float f => float.IsNaN(f),
            _ => false
        };

    private static bool IsInfinity<T>(T value) =>
        value switch
        {
            double d => double.IsInfinity(d),
            float f => float.IsInfinity(f),
            _ => false
        };

    public static void CheckForBetween<T>(
        T start, 
        T end, 
        [NotNull] T argumentValue, 
        string argumentName, 
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);
        CheckForNotNaN(argumentValue, argumentName, message);

        if (argumentValue.IsGreaterOrEqualTo(start) && argumentValue.IsLessOrEqualTo(end)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be between {Describe(start)} and {Describe(end)}", argumentValue));
    }

    public static void CheckForGreaterThan<T>(
        T value,
        [NotNull] T argumentValue,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);
        CheckForNotNaN(argumentValue, argumentName, message);

        if (argumentValue.IsGreaterThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be greater than {Describe(value)}", argumentValue));
    }

    public static void CheckForGreaterOrEqualTo<T>(
        T value,
        [NotNull] T argumentValue,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);
        CheckForNotNaN(argumentValue, argumentName, message);

        if (argumentValue.IsGreaterOrEqualTo(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be greater than or equal to {Describe(value)}", argumentValue));
    }

    public static void CheckForLessThan<T>(
        T value,
        [NotNull] T argumentValue,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);
        CheckForNotNaN(argumentValue, argumentName, message);

        if (argumentValue.IsLessThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be less than {Describe(value)}", argumentValue));
    }

    public static void CheckForLessOrEqualTo<T>(
        T value,
        [NotNull] T argumentValue,
        string argumentName,
        string? message = null)
    {
        CheckForNotNull(argumentValue, argumentName, message);
        CheckForNotNaN(argumentValue, argumentName, message);

        if (argumentValue.IsLessOrEqualTo(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"be less than or equal to {Describe(value)}", argumentValue));
    }

    private static bool IsEqualTo<T>(this T a, T b)
    {
        return EqualityComparer<T>.Default.Equals(a, b);
    }

    private static bool IsNotEqualTo<T>(this T a, T b)
    {
        return !EqualityComparer<T>.Default.Equals(a, b);
    }

    private static bool IsGreaterThan<T>(this T a, T b)
    {
        var comparisonResult = Comparer<T>.Default.Compare(a, b);
        return comparisonResult > 0;

    }

    private static bool IsLessThan<T>(this T a, T b)
    {
        var comparisonResult = Comparer<T>.Default.Compare(a, b);
        return comparisonResult < 0;
    }

    private static bool IsGreaterOrEqualTo<T>(this T a, T b)
    {
        var comparisonResult = Comparer<T>.Default.Compare(a, b);
        return comparisonResult >= 0;
    }

    private static bool IsLessOrEqualTo<T>(this T a, T b)
    {
        var comparisonResult = Comparer<T>.Default.Compare(a, b);
        return comparisonResult <= 0;
    }
}