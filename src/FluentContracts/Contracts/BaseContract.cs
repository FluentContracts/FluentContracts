using System.Diagnostics.CodeAnalysis;
using FluentContracts.Infrastructure;
using FluentContracts.Specifications;
using FluentContracts.Validators;

namespace FluentContracts.Contracts;

/// <summary>
/// The root of the contract hierarchy. It holds the argument and its name, which is all every
/// check needs; the checks themselves are added by the contracts deriving from it, in the order
/// <see cref="NullableContract{TArgument,TContract}"/>, <see cref="ObjectContract{TArgument,TContract}"/>,
/// <see cref="EqualityContract{TArgument,TContract}"/> and then one contract per type.
/// </summary>
/// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public abstract class BaseContract<TArgument, TContract>
    where TContract : BaseContract<TArgument, TContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected BaseContract(TArgument? argumentValue, string argumentName)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }

    // Both are additionally internal so ValueExtensions can end a chain with the validated value;
    // to deriving contracts outside the assembly nothing changes.
    /// <summary>The argument being checked, as it was handed to <c>Must()</c>.</summary>
    protected internal TArgument? ArgumentValue { get; }
    /// <summary>
    /// The argument's name, captured by <c>[CallerArgumentExpression]</c> at the call to <c>Must()</c>
    /// and reported as the parameter name when a check fails.
    /// </summary>
    protected internal string ArgumentName { get; }

    /// <summary>
    /// A message for every check in this chain, given to <c>Must()</c>:
    /// <c>environment.Must("This should be prod").NotBe("test").NotBeEmpty()</c>. A check's own
    /// <c>message</c> argument still wins for that check.
    /// </summary>
    protected internal string? ChainMessage { get; init; }

    /// <summary>
    /// The contract itself. Every check already returns the contract, so a chain reads
    /// <c>x.Must().NotBeNull().BeGreaterThan(5)</c>; <c>And</c> is kept so chains written as
    /// <c>x.Must().NotBeNull().And.BeGreaterThan(5)</c> keep compiling, and for anyone who finds
    /// it reads better.
    /// </summary>
    public TContract And => (TContract)this;

    /// <summary>
    /// Checks if the specified argument satisfies a custom condition.
    /// </summary>
    /// <param name="customCondition">The custom condition to check.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract Satisfy<T>(Func<T, bool> customCondition, string? message = null)
        where T : TArgument
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T>(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(customCondition, typedValue, ArgumentName, message ?? ChainMessage,
            expectation: "satisfy the given condition");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument satisfies a custom condition.
    /// </summary>
    /// <param name="customCondition">The custom condition to check.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract Satisfy<T,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TException>(
        Func<T, bool> customCondition)
        where TException : Exception, new()
        where T : TArgument
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T, TException>(ArgumentValue);
        Validator.CheckGenericCondition<T, TException>(customCondition, typedValue);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument satisfies a custom condition.
    /// </summary>
    /// <param name="customCondition">The custom condition to check.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract Satisfy<T,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TException>(
        Func<T, bool> customCondition, string message)
        where TException : Exception, new()
        where T : TArgument
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T, TException>(ArgumentValue, message);
        Validator.CheckGenericCondition<T, TException>(customCondition, typedValue, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument satisfies a specification — a named, reusable rule. The
    /// failure message uses the rule's <see cref="ISpecification{T}.Expectation"/>:
    /// <c>Expected iban to be a valid IBAN, but found "XX00".</c>
    /// </summary>
    /// <typeparam name="T">The type the specification applies to; the argument is converted to it first.</typeparam>
    /// <param name="specification">The rule to check.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null, exactly like <see cref="Satisfy{T}(Func{T,bool},string?)"/>.</remarks>
    public TContract Satisfy<T>(ISpecification<T> specification, string? message = null)
        where T : TArgument
    {
        Validator.CheckForNotNull(specification, nameof(specification));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T>(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecification(specification, typedValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument satisfies a specification, throwing
    /// <typeparamref name="TException"/> when it does not.
    /// </summary>
    /// <typeparam name="T">The type the specification applies to; the argument is converted to it first.</typeparam>
    /// <typeparam name="TException">The exception to throw.</typeparam>
    /// <param name="specification">The rule to check.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract Satisfy<T,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TException>(
        ISpecification<T> specification)
        where TException : Exception, new()
        where T : TArgument
    {
        Validator.CheckForNotNull(specification, nameof(specification));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T, TException>(ArgumentValue);
        Validator.CheckForSpecification<T, TException>(specification, typedValue);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the specified argument satisfies a specification, throwing
    /// <typeparamref name="TException"/> with <paramref name="message"/> when it does not.
    /// </summary>
    /// <typeparam name="T">The type the specification applies to; the argument is converted to it first.</typeparam>
    /// <typeparam name="TException">The exception to throw.</typeparam>
    /// <param name="specification">The rule to check.</param>
    /// <param name="message">The error message to include in the exception.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract Satisfy<T,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TException>(
        ISpecification<T> specification, string message)
        where TException : Exception, new()
        where T : TArgument
    {
        Validator.CheckForNotNull(specification, nameof(specification));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName);
        var typedValue = Validator.CheckForTypeAndConvert<TArgument, T, TException>(ArgumentValue, message);
        Validator.CheckForSpecification<T, TException>(specification, typedValue, message);
        return (TContract)this;
    }
}
