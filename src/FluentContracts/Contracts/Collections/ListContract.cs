using System.Collections;
using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

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
    /// Checks if <see cref="containedString"/> is part of the value of the <see cref="string"/> argument.
    /// </summary>
    /// <param name="containedString">A string to check for being part of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    [Pure]
    public Linker<ListContract<T>> Contain(params T[] containedElements)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForContaining(containedElements, ArgumentValue, ArgumentName);
        return _linker;
    }

    /// <summary>
    /// Checks if <see cref="containedString"/> is not part of the value of the <see cref="string"/> argument.
    /// </summary>
    /// <param name="containedString">A string to check for being part of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    [Pure]
    public Linker<ListContract<T>> NotContain(params T[] containedElements)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        Validator.CheckForNotContaining(containedElements, ArgumentValue, ArgumentName);
        return _linker;
    }
}