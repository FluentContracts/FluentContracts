using System.Diagnostics.CodeAnalysis;
using FluentContracts.Infrastructure;

namespace FluentContracts.Validators;

internal static partial class Validator
{
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

        ThrowHelper.ThrowArgumentNullException(argumentName, message);
    }

    public static void CheckForNull<T>(
        T? value,
        string argumentName,
        string? message = null)
    {
        if (value is null) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckGenericCondition<T>(
        Func<T, bool> genericCondition,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (genericCondition(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
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
        if (values.Any(v => v.IsEqualTo(argumentValue))) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForNotAnyOf<T>(
        IEnumerable<T> values,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (values.All(v => v.IsNotEqualTo(argumentValue))) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForSpecificValue<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (value.IsEqualTo(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForSpecificValue<T, TException>(
        T value,
        T argumentValue)
        where TException : Exception, new()
    {
        if (value.IsEqualTo(argumentValue)) return;

        ThrowHelper.ThrowUserDefinedException<TException>();
    }

    public static void CheckForSpecificValue<T, TException>(
        T value,
        T argumentValue,
        string message)
        where TException : Exception, new()
    {
        if (value.IsEqualTo(argumentValue)) return;

        ThrowHelper.ThrowUserDefinedException<TException>(message);
    }

    public static void CheckForNotSpecificValue<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (value.IsNotEqualTo(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForBetween<T>(
        T start,
        T end,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue.IsGreaterOrEqualTo(start) && argumentValue.IsLessOrEqualTo(end)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForGreaterThan<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue.IsGreaterThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForGreaterOrEqualTo<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue.IsGreaterOrEqualTo(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForLessThan<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue.IsLessThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForLessOrEqualTo<T>(
        T value,
        T argumentValue,
        string argumentName,
        string? message = null)
    {
        if (argumentValue.IsLessOrEqualTo(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    public static void ContainsPropertyByName(object element, string propertyName, string? message = null)
    {
        var property = element.GetType().GetProperty(propertyName);

        if (property is not null) return;

        ThrowHelper.ThrowArgumentNullException(propertyName, message);
    }

    public static void ContainsPropertyWithValue(object element, string propertyName, object value, string? message = null)
    {
        var property = element.GetType()
            .GetProperty(propertyName);

        var propertyValue = property!.GetValue(element);

        if (propertyValue == value) return;

        ThrowHelper.ThrowArgumentNullException(propertyName, message);
    }

    public static void ContainsMethod(object element, string methodName, string? message = null)
    {
        var method = element.GetType().GetMethod(methodName);

        if (method is not null) return;

        ThrowHelper.ThrowArgumentNullException(methodName, message);
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