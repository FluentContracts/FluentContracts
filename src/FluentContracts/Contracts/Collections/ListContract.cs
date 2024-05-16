using FluentContracts.Infrastructure;
using Microsoft.VisualBasic;

namespace FluentContracts.Contracts.Collections;

public class ListContract<T> : CollectionContract<T, IList<T>, ListContract<T>>
{
    private readonly Linker<ListContract<T>> _linker;

    public ListContract(IList<T> argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<ListContract<T>>(this);
    }
    
    /// <summary>
    /// Checks if <see cref="containedElements"/> subset is part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="containedElements">One or more elements to check for being part of the argument's values</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<ListContract<T>> Contain(params T[] containedElements)
    {
        return Contain(containedElements, null);
    }
    
    /// <summary>
    /// Checks if <see cref="containedElements"/> subset is part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="containedElements">One or more elements to check for being part of the argument's values</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<ListContract<T>> Contain(IEnumerable<T> containedElements, string? message = null)
    {
        
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForContaining(containedElements, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if <see cref="notContainedElements"/> subset is not part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="notContainedElements">One or more elements to check for not being part of the argument's values</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<ListContract<T>> NotContain(params T[] notContainedElements)
    {
        return NotContain(notContainedElements, null);
    }

    /// <summary>
    /// Checks if <see cref="notContainedElements"/> subset is not part of the elements of the <see cref="IList{T}"/> argument.
    /// </summary>
    /// <param name="notContainedElements">One or more elements to check for not being part of the argument's values</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<ListContract<T>> NotContain(IEnumerable<T> notContainedElements, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotContaining(notContainedElements, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if all the elements of the <see cref="IList{T}"/> argument are of type <see cref="TElement"/>.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<ListContract<T>> HaveElementsOfType<TElement>(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForType<T, TElement>(ArgumentValue, ArgumentName, message);
        return _linker;
    }
}