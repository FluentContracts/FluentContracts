using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using FluentContracts.Contracts.Collections;

namespace FluentContracts;

public static partial class ContractExtensions
{
    #region Array
    
    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Array"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ArrayContract class.</returns>
    [Pure]
    public static ArrayContract Must(
        this Array argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new ArrayContract(argument, argumentName);
    }

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="Array"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ArrayContract class.</returns>
    [Pure]
    public static ArrayContract Must<TArgument>(
        this TArgument[] argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new ArrayContract(argument, argumentName);
    }

    #endregion
    
    #region List

    /// <summary>
    /// Indicates a start in the fluent chain of validations for an argument of type <see cref="List{TArgument}"/>
    /// </summary>
    /// <param name="argument">Argument to be validated</param>
    /// <param name="argumentName">Optional parameter to overwrite the argument name</param>
    /// <returns>A new instance of the ListContract class.</returns>
    [Pure]
    public static ListContract Must<TArgument>(
        this List<TArgument> argument,
        [CallerArgumentExpression("argument")] string argumentName = DefaultArgumentName)
    {
        return new ListContract(argument, argumentName);
    }

    #endregion
}