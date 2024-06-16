using System.Collections;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

public abstract class CollectionContract<TElement, TArgument, TContract> : EqualityContract<TArgument, TContract>
    where TContract : CollectionContract<TElement, TArgument, TContract>
    where TArgument : ICollection<TElement>?
{
    private readonly Linker<TContract> _linker;

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
    /// Checks if the <see cref="ICollection"/> has <see cref="expectedCount"/> elements count.
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
    /// Checks if the <see cref="ICollection"/> has different than <see cref="notExpectedCount"/> elements count.
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
}