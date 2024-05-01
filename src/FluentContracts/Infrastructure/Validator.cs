using System.Diagnostics.CodeAnalysis;

namespace FluentContracts.Infrastructure;

internal static class Validator
{
    public static void CheckForNotNull<T, TException>([NotNull] T value)
        where TException : Exception, new()
    {
        if (value is not null) return;

        ThrowHelper.ThrowUserDefinedException<TException>();
    }

    public static void CheckForNotNull<T, TException>([NotNull] T argumentValue, string message)
        where TException : Exception, new()
    {
        if (argumentValue is not null) return;

        ThrowHelper.ThrowUserDefinedException<TException>(message);
    }

    public static void CheckForNotNull<T>([NotNull] T value, string argumentName, string? message = null)
    {
        if (value is not null) return;

        ThrowHelper.ThrowArgumentNullException(argumentName, message);
    }

    public static void CheckForNull<T>(T? value, string argumentName, string? message = null)
    {
        if (value is null) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForContaining<T>(
        IEnumerable<T> containedElements, 
        IEnumerable<T> collection, 
        string argumentName,
        string? message = null)
    {
        var sourceHash = new HashSet<T>(collection);
        var containedHash = new HashSet<T>(containedElements);
        
        if (sourceHash.IsSupersetOf(containedHash)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForNotContaining<T>(
        IEnumerable<T> containedElements, 
        IEnumerable<T> collection, 
        string argumentName,
        string? message = null)
    {
        var sourceHash = new HashSet<T>(collection);
        var containedHash = new HashSet<T>(containedElements);
        
        if (!sourceHash.IsSupersetOf(containedHash)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckGenericCondition<T>(Func<T, bool> genericCondition, T argumentValue, string argumentName,
        string? message = null)
    {
        if (genericCondition(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForAnyOf<T>(IEnumerable<T> values, T argumentValue, string argumentName,
        string? message = null)
    {
        if (values.Any(v => v.IsEqualTo(argumentValue))) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForNotAnyOf<T>(IEnumerable<T> values, T argumentValue, string argumentName,
        string? message = null)
    {
        if (values.All(v => v.IsNotEqualTo(argumentValue))) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForSpecificValue<T>(T value, T argumentValue, string argumentName, string? message = null)
    {
        if (value.IsEqualTo(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForSpecificValue<T, TException>(T value, T argumentValue)
        where TException : Exception, new()
    {
        if (value.IsEqualTo(argumentValue)) return;

        ThrowHelper.ThrowUserDefinedException<TException>();
    }

    public static void CheckForSpecificValue<T, TException>(T value, T argumentValue, string message)
        where TException : Exception, new()
    {
        if (value.IsEqualTo(argumentValue)) return;

        ThrowHelper.ThrowUserDefinedException<TException>(message);
    }

    public static void CheckForNotSpecificValue<T>(T value, T argumentValue, string argumentName,
        string? message = null)
    {
        if (value.IsNotEqualTo(argumentValue)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForBetween<T>(T start, T end, T argumentValue, string argumentName, string? message = null)
    {
        if (argumentValue.IsGreaterOrEqualThan(start) && argumentValue.IsLessOrEqualThan(end)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForGreaterThan<T>(T value, T argumentValue, string argumentName, string? message = null)
    {
        if (argumentValue.IsGreaterThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForGreaterOrEqualThan<T>(T value, T argumentValue, string argumentName,
        string? message = null)
    {
        if (argumentValue.IsGreaterOrEqualThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForLessThan<T>(T value, T argumentValue, string argumentName, string? message = null)
    {
        if (argumentValue.IsLessThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForLessOrEqualThan<T>(T value, T argumentValue, string argumentName, string? message = null)
    {
        if (argumentValue.IsLessOrEqualThan(value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
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

    private static bool IsGreaterOrEqualThan<T>(this T a, T b)
    {
        var comparisonResult = Comparer<T>.Default.Compare(a, b);
        return comparisonResult >= 0;
    }

    private static bool IsLessOrEqualThan<T>(this T a, T b)
    {
        var comparisonResult = Comparer<T>.Default.Compare(a, b);
        return comparisonResult <= 0;
    }
}