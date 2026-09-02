using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Collections;

/// <summary>
/// The inheritable contract for an <see cref="System.Collections.Generic.IList{T}"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="T">The element type.</typeparam>
public class ListContract<T> : CollectionContract<T, IList<T>?, ListContract<T>>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    public ListContract(IList<T>? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }
    
    /// <summary>
    /// Checks if <paramref name="containedElements"/> subset is part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="containedElements">One or more elements to check for being part of the argument's values</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> Contain(params T[] containedElements)
    {
        return Contain(containedElements, null);
    }
    
    /// <summary>
    /// Checks if <paramref name="containedElements"/> subset is part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="containedElements">One or more elements to check for being part of the argument's values</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> Contain(IEnumerable<T> containedElements, string? message = null)
    {   
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForContaining(containedElements, ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if <paramref name="notContainedElements"/> subset is not part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="notContainedElements">One or more elements to check for not being part of the argument's values</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> NotContain(params T[] notContainedElements)
    {
        return NotContain(notContainedElements, null);
    }

    /// <summary>
    /// Checks if <paramref name="notContainedElements"/> subset is not part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="notContainedElements">One or more elements to check for not being part of the argument's values</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> NotContain(IEnumerable<T> notContainedElements, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotContaining(notContainedElements, ArgumentValue, ArgumentName, message);
        return this;
    }
    
    /// <summary>
    /// Checks if all the elements of the <see cref="IList{T}"/> argument are of type <typeparamref name="TElement"/>.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> HaveElementsOfType<TElement>(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForType<T, TElement>(ArgumentValue, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are in ascending order, comparing
    /// with <see cref="Comparer{T}.Default"/>. Non-strict: equal neighbours are in order, matching
    /// <c>List&lt;T&gt;.Sort</c>. An empty or single-element list is in order.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> BeInAscendingOrder(string? message = null)
    {
        return BeInAscendingOrder(Comparer<T>.Default, message);
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are in ascending order, comparing
    /// with <paramref name="comparer"/>. Non-strict: equal neighbours are in order, matching
    /// <c>List&lt;T&gt;.Sort</c>. An empty or single-element list is in order.
    /// </summary>
    /// <param name="comparer">The comparer that defines the order.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> BeInAscendingOrder(IComparer<T> comparer, string? message = null)
    {
        Validator.CheckForNotNull(comparer, nameof(comparer));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForAscendingOrder(ArgumentValue, comparer, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are not in ascending order,
    /// comparing with <see cref="Comparer{T}.Default"/> — at least one neighbour pair descends.
    /// An empty or single-element list is vacuously in every order, so it fails this check.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> NotBeInAscendingOrder(string? message = null)
    {
        return NotBeInAscendingOrder(Comparer<T>.Default, message);
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are not in ascending order,
    /// comparing with <paramref name="comparer"/> — at least one neighbour pair descends.
    /// An empty or single-element list is vacuously in every order, so it fails this check.
    /// </summary>
    /// <param name="comparer">The comparer that defines the order.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> NotBeInAscendingOrder(IComparer<T> comparer, string? message = null)
    {
        Validator.CheckForNotNull(comparer, nameof(comparer));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotAscendingOrder(ArgumentValue, comparer, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are in descending order, comparing
    /// with <see cref="Comparer{T}.Default"/>. Non-strict: equal neighbours are in order.
    /// An empty or single-element list is in order.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> BeInDescendingOrder(string? message = null)
    {
        return BeInDescendingOrder(Comparer<T>.Default, message);
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are in descending order, comparing
    /// with <paramref name="comparer"/>. Non-strict: equal neighbours are in order.
    /// An empty or single-element list is in order.
    /// </summary>
    /// <param name="comparer">The comparer that defines the order.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> BeInDescendingOrder(IComparer<T> comparer, string? message = null)
    {
        Validator.CheckForNotNull(comparer, nameof(comparer));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForDescendingOrder(ArgumentValue, comparer, ArgumentName, message);
        return this;
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are not in descending order,
    /// comparing with <see cref="Comparer{T}.Default"/> — at least one neighbour pair ascends.
    /// An empty or single-element list is vacuously in every order, so it fails this check.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> NotBeInDescendingOrder(string? message = null)
    {
        return NotBeInDescendingOrder(Comparer<T>.Default, message);
    }

    /// <summary>
    /// Checks if the elements of the <see cref="IList{T}"/> argument are not in descending order,
    /// comparing with <paramref name="comparer"/> — at least one neighbour pair ascends.
    /// An empty or single-element list is vacuously in every order, so it fails this check.
    /// </summary>
    /// <param name="comparer">The comparer that defines the order.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public ListContract<T> NotBeInDescendingOrder(IComparer<T> comparer, string? message = null)
    {
        Validator.CheckForNotNull(comparer, nameof(comparer));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotDescendingOrder(ArgumentValue, comparer, ArgumentName, message);
        return this;
    }
}
