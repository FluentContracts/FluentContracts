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
        if (CollectionContains(collection, containedElements)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForNotContaining<T>(
        IEnumerable<T> containedElements, 
        IEnumerable<T> collection, 
        string argumentName,
        string? message = null)
    {
        // None of them may be present. Asking whether the collection contains them *all* would let a
        // collection holding some of them through, which is the opposite of what the check promises.
        if (!CollectionContainsAny(collection, containedElements)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForContainingAny<T>(
        IEnumerable<T> containedElements,
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        if (CollectionContainsAny(collection, containedElements)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForUniqueItems<T>(
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        var seen = new HashSet<T>();

        if (collection.All(seen.Add)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForNotContainingNull<T>(
        IEnumerable<T> collection,
        string argumentName,
        string? message = null)
    {
        if (collection.All(x => x is not null)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForAllSatisfying<T>(
        IEnumerable<T> collection,
        Func<T, bool> condition,
        string argumentName,
        string? message = null)
    {
        if (collection.All(condition)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForAnySatisfying<T>(
        IEnumerable<T> collection,
        Func<T, bool> condition,
        string argumentName,
        string? message = null)
    {
        if (collection.Any(condition)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }

    public static void CheckForType<TElements, TCheck>(
        IEnumerable<TElements> collection,
        string argumentName,
        string? message = null)
    {
        if (collection.All(e => e is TCheck)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForContainingKey<TKey, TValue>(
        TKey key, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {   
        if (dictionary.ContainsKey(key)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForNotContainingKey<TKey, TValue>(
        TKey key, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {   
        if (!dictionary.ContainsKey(key)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForContainingValue<TKey, TValue>(
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {   
        if (CollectionContains(dictionary.Values, value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForNotContainingValue<TKey, TValue>(
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {   
        if (!CollectionContains(dictionary.Values, value)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForContainingKeyValuePair<TKey, TValue>(
        TKey key,
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {   
        if (DictionaryContainsKeyValuePair(key, value, dictionary)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
    
    public static void CheckForNotContainingKeyValuePair<TKey, TValue>(
        TKey key,
        TValue value, 
        IDictionary<TKey, TValue> dictionary, 
        string argumentName,
        string? message = null)
    {   
        if (!DictionaryContainsKeyValuePair(key, value, dictionary)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
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