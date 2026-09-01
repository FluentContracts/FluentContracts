using FluentContracts.Infrastructure;

namespace FluentContracts.Validators;

internal static partial class Validator
{
    public static void CheckForContaining<T>(
        IEnumerable<T> containedElements,
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        // Materialised because the failure message renders it a second time, and the caller may have
        // handed over a one-shot enumerable. The argument itself is never enumerated twice — the
        // message deliberately renders the expected elements, not the collection.
        var elements = containedElements as IReadOnlyCollection<T> ?? containedElements.ToArray();

        if (CollectionContains(collection, elements)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"contain {Describe(elements)}"));
    }

    public static void CheckForNotContaining<T>(
        IEnumerable<T> containedElements,
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        var elements = containedElements as IReadOnlyCollection<T> ?? containedElements.ToArray();

        // None of them may be present. Asking whether the collection contains them *all* would let a
        // collection holding some of them through, which is the opposite of what the check promises.
        if (!CollectionContainsAny(collection, elements)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not contain any of {Describe(elements)}"));
    }

    public static void CheckForContainingAny<T>(
        IEnumerable<T> containedElements,
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        var elements = containedElements as IReadOnlyCollection<T> ?? containedElements.ToArray();

        if (CollectionContainsAny(collection, elements)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"contain at least one of {Describe(elements)}"));
    }

    public static void CheckForUniqueItems<T>(
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        var seen = new HashSet<T>();

        // A single pass, so the failure can name the duplicate without enumerating the caller's
        // collection again — it may be a one-shot enumerable.
        foreach (var item in collection)
        {
            if (seen.Add(item)) continue;

            ThrowHelper.ThrowArgumentOutOfRangeException(
                argumentName,
                message ?? Expected(argumentName, $"contain only unique items, but {Describe(item)} appears more than once"));
        }
    }

    public static void CheckForNotContainingNull<T>(
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        if (collection.All(x => x is not null)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "not contain null"));
    }

    public static void CheckForAllSatisfying<T>(
        IEnumerable<T> collection,
        Func<T, bool> condition,
        string argumentName,
        string? message = null)
    {
        // A single pass, so the failure can name the offending item without enumerating the caller's
        // collection again — it may be a one-shot enumerable.
        foreach (var item in collection)
        {
            if (condition(item)) continue;

            ThrowHelper.ThrowArgumentOutOfRangeException(
                argumentName,
                message ?? Expected(argumentName, $"have every item satisfy the condition, but {Describe(item)} does not"));
        }
    }

    public static void CheckForAnySatisfying<T>(
        IEnumerable<T> collection,
        Func<T, bool> condition,
        string argumentName,
        string? message = null)
    {
        if (collection.Any(condition)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, "have at least one item satisfying the condition"));
    }

    public static void CheckForType<TElements, TCheck>(
        IEnumerable<TElements> collection,
        string argumentName,
        string? message = null)
    {
        // A single pass, so the failure can name the offending item's type without enumerating the
        // caller's collection again — it may be a one-shot enumerable.
        foreach (var element in collection)
        {
            if (element is TCheck) continue;

            ThrowHelper.ThrowArgumentOutOfRangeException(
                argumentName,
                message ?? Expected(argumentName, $"contain only items of type {typeof(TCheck)}", element?.GetType()));
        }
    }
    
    public static void CheckForContainingKey<TKey, TValue>(
        TKey key, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {
        if (dictionary.ContainsKey(key)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"contain the key {Describe(key)}"));
    }
    
    public static void CheckForNotContainingKey<TKey, TValue>(
        TKey key, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {
        if (!dictionary.ContainsKey(key)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not contain the key {Describe(key)}"));
    }
    
    public static void CheckForContainingValue<TKey, TValue>(
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {
        if (CollectionContains(dictionary.Values, value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"contain the value {Describe(value)}"));
    }
    
    public static void CheckForNotContainingValue<TKey, TValue>(
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {
        if (!CollectionContains(dictionary.Values, value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not contain the value {Describe(value)}"));
    }
    
    public static void CheckForContainingKeyValuePair<TKey, TValue>(
        TKey key,
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {
        if (DictionaryContainsKeyValuePair(key, value, dictionary)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"contain the key {Describe(key)} with the value {Describe(value)}"));
    }
    
    public static void CheckForNotContainingKeyValuePair<TKey, TValue>(
        TKey key,
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {
        if (!DictionaryContainsKeyValuePair(key, value, dictionary)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(
            argumentName,
            message ?? Expected(argumentName, $"not contain the key {Describe(key)} with the value {Describe(value)}"));
    }

    private static bool CollectionContains<T>(IEnumerable<T> collection, IEnumerable<T> containedElements) =>
        CollectionContains(collection, containedElements.ToArray());

    private static bool CollectionContains<T>(IEnumerable<T> collection, params T[] containedElements)
    {
        if (containedElements.Length == 1)
        {
            return collection.Contains(containedElements[0]);
        }
        
        var sourceHash = new HashSet<T>(collection);
        var containedHash = new HashSet<T>(containedElements);

        return sourceHash.IsSupersetOf(containedHash);
    }

    /// <summary>
    /// Whether at least one of <paramref name="elements"/> appears in <paramref name="collection"/>.
    /// An empty <paramref name="elements"/> is vacuously false, mirroring the vacuously true superset
    /// check above.
    /// </summary>
    private static bool CollectionContainsAny<T>(IEnumerable<T> collection, IEnumerable<T> elements)
    {
        var candidates = new HashSet<T>(elements);

        return candidates.Count != 0 && collection.Any(candidates.Contains);
    }

    private static bool DictionaryContainsKeyValuePair<TKey, TValue>(
        TKey key,
        TValue value,
        IDictionary<TKey, TValue> dictionary) =>
        dictionary.TryGetValue(key, out var foundValue) && foundValue != null && foundValue.Equals(value);
}