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
        if (!CollectionContains(collection, containedElements)) return;

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

    private static bool DictionaryContainsKeyValuePair<TKey, TValue>(
        TKey key,
        TValue value,
        IDictionary<TKey, TValue> dictionary) =>
        dictionary.TryGetValue(key, out var foundValue) && foundValue != null && foundValue.Equals(value);
}