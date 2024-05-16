using System.Collections;

using FluentContracts.Infrastructure;
namespace FluentContracts.Contracts;

public abstract class CollectionContract<TElement, TArgument, TContract> : EqualityContract<TArgument, TContract>
    where TContract : CollectionContract<TElement, TArgument, TContract>
    where TArgument : ICollection<TElement>
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
    public Linker<TContract> BeEmpty(string? message = null)
    {
        Validator.CheckForSpecificValue(0, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> is not empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotSpecificValue(0, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> has <see cref="expectedCount"/> elements.
    /// </summary>
    /// <param name="expectedCount">Expected count of the elements in the collection</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeWithCount(int expectedCount, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedCount, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the <see cref="ICollection"/> has different than <see cref="notExpectedCount"/> elements.
    /// </summary>
    /// <param name="notExpectedCount">Count which is not expected to be equal to the elements in the collection</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeWithCount(int notExpectedCount, string? message = null)
    {
        Validator.CheckForNotSpecificValue(notExpectedCount, ArgumentValue.Count, ArgumentName, message);
        return _linker;
    }
}