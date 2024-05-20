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

    public static void CheckForType<TElements, TCheck>(
        IEnumerable<TElements> collection,
        string argumentName,
        string? message = null)
    {
        if (collection.All(e => e is TCheck)) return;

        ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
    }
}