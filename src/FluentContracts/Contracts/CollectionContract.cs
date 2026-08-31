using System.Collections;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

/// <summary>
/// Adds the checks shared by every collection — counting, emptiness, uniqueness and the quantifiers —
/// so a list, an array and a dictionary all get them. Each check first requires the argument itself
/// not to be null.
/// </summary>
/// <typeparam name="TElement">The element type of the collection.</typeparam>
/// <typeparam name="TArgument">The collection type being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public abstract class CollectionContract<TElement, TArgument, TContract> : EqualityContract<TArgument, TContract>
    where TContract : CollectionContract<TElement, TArgument, TContract>
    where TArgument : ICollection<TElement>?
{
    private readonly Linker<TContract> _linker;

    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected CollectionContract(TArgument argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }
    
    /// <summary>
    /// Checks if the <see cref="ICollection"/> is empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(0, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> is not empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(0, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> has <paramref name="expectedCount"/> elements count.
    /// </summary>
    /// <param name="expectedCount">Expected count of the elements in the collection</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveCountEqualTo(int expectedCount, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(expectedCount, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> has different than <paramref name="notExpectedCount"/> elements count.
    /// </summary>
    /// <param name="notExpectedCount">Count which is not expected to be equal to the elements in the collection</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotHaveCountEqualTo(int notExpectedCount, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(notExpectedCount, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the count of the elements of the <see cref="ICollection"/> argument is greater than <paramref name="count"/>
    /// </summary>
    /// <param name="count">Count to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveCountGreaterThan(int count, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(count, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the count of the elements of the <see cref="ICollection"/> argument is greater than or equal to <paramref name="count"/>
    /// </summary>
    /// <param name="count">Count to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveCountGreaterOrEqualTo(int count, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(count, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the count of the elements of the <see cref="ICollection"/> argument is less than <paramref name="count"/>
    /// </summary>
    /// <param name="count">Count to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveCountLessThan(int count, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(count, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the count of the elements of the <see cref="ICollection"/> argument is less than or equal to <paramref name="count"/>
    /// </summary>
    /// <param name="count">Count to check against</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveCountLessOrEqualTo(int count, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(count, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the count of the elements of the <see cref="ICollection"/> argument is inclusively between <paramref name="start"/> and <paramref name="end"/>
    /// </summary>
    /// <param name="start">Start of range</param>
    /// <param name="end">End of range</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveCountBetween(int start, int end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForBetween(start, end, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> contains at least one of <paramref name="expectedElements"/>.
    /// </summary>
    /// <param name="expectedElements">One or more elements, at least one of which must be present.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> ContainAnyOf(params TElement[] expectedElements)
    {
        return ContainAnyOf(expectedElements, null);
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> contains at least one of <paramref name="expectedElements"/>.
    /// </summary>
    /// <param name="expectedElements">One or more elements, at least one of which must be present.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> ContainAnyOf(IEnumerable<TElement> expectedElements, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForContainingAny(expectedElements, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if every element of the <see cref="ICollection"/> appears exactly once.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveUniqueItems(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForUniqueItems(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if no element of the <see cref="ICollection"/> is null.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>
    /// Also checks for the argument to NOT be null. A collection of a non-nullable value type can never
    /// hold one, so the check always passes for such a collection.
    /// </remarks>
    public Linker<TContract> NotContainNull(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotContainingNull(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if every element of the <see cref="ICollection"/> satisfies <paramref name="condition"/>.
    /// </summary>
    /// <param name="condition">The condition every element has to satisfy.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null. An empty collection satisfies this check.</remarks>
    public Linker<TContract> AllSatisfy(Func<TElement, bool> condition, string? message = null)
    {
        Validator.CheckForNotNull(condition, nameof(condition));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForAllSatisfying(ArgumentValue, condition, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if at least one element of the <see cref="ICollection"/> satisfies <paramref name="condition"/>.
    /// </summary>
    /// <param name="condition">The condition at least one element has to satisfy.</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null. An empty collection never satisfies this check.</remarks>
    public Linker<TContract> AnySatisfy(Func<TElement, bool> condition, string? message = null)
    {
        Validator.CheckForNotNull(condition, nameof(condition));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForAnySatisfying(ArgumentValue, condition, ArgumentName, message);
        return _linker;
    }
}
