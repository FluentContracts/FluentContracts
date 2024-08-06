using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Collections;

public class DictionaryContract<TKey, TValue>
    : CollectionContract<KeyValuePair<TKey, TValue>, IDictionary<TKey, TValue>?, DictionaryContract<TKey, TValue>>
{
    
    private readonly Linker<DictionaryContract<TKey, TValue>> _linker;

    public DictionaryContract(IDictionary<TKey, TValue>? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<DictionaryContract<TKey, TValue>>(this);
    } 
    
    /// <summary>
    /// Checks if <paramref name="key"/> is part of the keys collection of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="key">Key to check against the keys of the dictionary</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> ContainKey(TKey key, string? message = null)
    {
        Validator.CheckForNotNull(key, nameof(key));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForContainingKey(key, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if <paramref name="key"/> is not part of the keys collection of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="key">Key to check against the keys of the dictionary</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> NotContainKey(TKey key, string? message = null)
    {
        Validator.CheckForNotNull(key, nameof(key));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotContainingKey(key, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if <paramref name="value"/> is part of the values collection of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="value">Value to check against the values of the dictionary</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> ContainValue(TValue value, string? message = null)
    {
        Validator.CheckForNotNull(value, nameof(value));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForContainingValue(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if <paramref name="value"/> is not part of the values collection of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="value">Value to check against the values of the dictionary</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> NotContainValue(TValue value, string? message = null)
    {
        Validator.CheckForNotNull(value, nameof(value));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotContainingValue(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if <paramref name="pair"/> is part of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="pair">KeyValuePair to check against the dictionary</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> ContainKeyValuePair(KeyValuePair<TKey, TValue> pair, string? message = null)
    {
        return ContainKeyValuePair(pair.Key, pair.Value, message);
    }
    
    /// <summary>
    /// Checks if <paramref name="key"/> and <paramref name="value"/> pair is part of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="key">Key to check against the pair</param>
    /// <param name="value">Value to check against the pair</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> ContainKeyValuePair(TKey key, TValue value, string? message = null)
    {
        Validator.CheckForNotNull(key, nameof(key));
        Validator.CheckForNotNull(value, nameof(value));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForContainingKeyValuePair(key, value, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if <paramref name="pair"/> is not part of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="pair">KeyValuePair to check against the dictionary</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> NotContainKeyValuePair(KeyValuePair<TKey, TValue> pair, string? message = null)
    {
        return NotContainKeyValuePair(pair.Key, pair.Value, message);
    }
    
    /// <summary>
    /// Checks if <paramref name="key"/> and <paramref name="value"/> pair is not part of the <see cref="IDictionary{TKey, TValue}"/> argument.
    /// </summary>
    /// <param name="key">Key to check against the pair</param>
    /// <param name="value">Value to check against the pair</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<DictionaryContract<TKey, TValue>> NotContainKeyValuePair(TKey key, TValue value, string? message = null)
    {
        Validator.CheckForNotNull(key, nameof(key));
        Validator.CheckForNotNull(value, nameof(value));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotContainingKeyValuePair(key, value, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}