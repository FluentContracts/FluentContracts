
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Collections;
using FluentContracts.Infrastructure;

namespace FluentContracts;

public static class CollectionExtensions
{
    #region List
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="IList{T}"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ListContract class.</returns>
    
    public static ListContract<T> Must<T>(
        this IList<T>? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ListContract<T>(argument, argumentName);
    }
    
    #endregion
    
    #region Array
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Array"/> of {T}
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ListContract class.</returns>
    
    public static ListContract<T> Must<T>(
        this T[]? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new ListContract<T>(argument, argumentName);
    }
    
    #endregion
    
    #region Dictionary
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="IDictionary{TKey, TValue}"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ListContract class.</returns>
    
    public static DictionaryContract<TKey, TValue> Must<TKey, TValue>(
        this IDictionary<TKey, TValue>? argument,
        [CallerArgumentExpression("argument")] string argumentName = Constants.DefaultArgumentName)
    {
        return new DictionaryContract<TKey, TValue>(argument, argumentName);
    }
    
    #endregion
}