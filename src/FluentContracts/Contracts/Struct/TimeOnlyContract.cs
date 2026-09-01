#if NET8_0_OR_GREATER
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

// The whole file is net8-only: TimeOnly does not exist on netstandard2.0 and there is no
// sensible polyfill without taking a dependency, so consumers there simply do not see it.

/// <summary>
/// The entry point for checks on a <see cref="System.TimeOnly"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class TimeOnlyContract(TimeOnly? argumentValue, string argumentName)
    : TimeOnlyContract<TimeOnlyContract>(argumentValue, argumentName);

/// <summary>
/// The inheritable contract for a <see cref="System.TimeOnly"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class TimeOnlyContract<TContract> : EqualityContract<TimeOnly?, TContract>
    where TContract : TimeOnlyContract<TContract>
{
    private readonly Linker<TContract> _linker;

    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected TimeOnlyContract(TimeOnly? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be greater than</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterThan(TimeOnly value, string? message = null)
    {
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than or equal to <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be greater than or equal to</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterOrEqualTo(TimeOnly value, string? message = null)
    {
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is less than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be less than</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessThan(TimeOnly value, string? message = null)
    {
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is less than or equal to <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be less than or equal to</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessOrEqualTo(TimeOnly value, string? message = null)
    {
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is between <paramref name="start"/> and <paramref name="end"/>,
    /// with <see cref="TimeOnly.IsBetween"/>'s semantics: <paramref name="start"/> inclusive,
    /// <paramref name="end"/> exclusive, and the window wraps midnight — between 22:00 and 02:00
    /// contains 23:30.
    /// </summary>
    /// <param name="start">Start of the window, inclusive</param>
    /// <param name="end">End of the window, exclusive</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeBetween(TimeOnly start, TimeOnly end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value.IsBetween(start, end), ArgumentValue, ArgumentName, message,
            expectation: $"be between {Validator.Describe(start)} and {Validator.Describe(end)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is not between <paramref name="start"/> and <paramref name="end"/>,
    /// with <see cref="TimeOnly.IsBetween"/>'s semantics: <paramref name="start"/> inclusive,
    /// <paramref name="end"/> exclusive, and the window wraps midnight.
    /// </summary>
    /// <param name="start">Start of the window, inclusive</param>
    /// <param name="end">End of the window, exclusive</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeBetween(TimeOnly start, TimeOnly end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a!.Value.IsBetween(start, end), ArgumentValue, ArgumentName, message,
            expectation: $"not be between {Validator.Describe(start)} and {Validator.Describe(end)}");
        return _linker;
    }
}
#endif
