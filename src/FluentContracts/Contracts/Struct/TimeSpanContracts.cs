using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;
/// <summary>
/// The entry point for checks on a <see cref="System.TimeSpan"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class TimeSpanContract(TimeSpan? argumentValue, string argumentName)
    : TimeSpanContract<TimeSpanContract>(argumentValue, argumentName);

/// <summary>
/// The inheritable contract for a <see cref="System.TimeSpan"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class TimeSpanContract<TContract> : BaseContract<TimeSpan?, TContract>
    where TContract : TimeSpanContract<TContract>
{
    private readonly Linker<TContract> _linker;

    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected TimeSpanContract(
        TimeSpan? argumentValue,
        string argumentName) : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }
    
     /// <summary>
    /// Checks if the specified argument is not null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeNull(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is null.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeNull(string? message = null)
    {
        Validator.CheckForNull(ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Be(TimeSpan expectedValue, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Be(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBe(TimeSpan expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 

    /// <summary>
    /// Checks if the specified argument is not equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBe(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 

    /// <summary>
    /// Checks if the specified argument's elapsed ticks is shorter than <paramref name="expectedValue"/>
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeShorterThan(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForNotNull(expectedValue, nameof(expectedValue));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value.Ticks < expectedValue.Value.Ticks, ArgumentValue, ArgumentName, message,
            expectation: $"be shorter than {Validator.Describe(expectedValue)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument's elapsed ticks is not shorter than <paramref name="expectedValue"/>
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeShorterThan(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForNotNull(expectedValue, nameof(expectedValue));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        // Not the mirror of BeShorterThan's strict <: a span exactly equal to the expected value is
        // not shorter, so the negation must admit equality.
        Validator.CheckGenericCondition(a => a!.Value.Ticks >= expectedValue.Value.Ticks, ArgumentValue, ArgumentName, message,
            expectation: $"not be shorter than {Validator.Describe(expectedValue)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument's elapsed ticks is longer than <paramref name="expectedValue"/>
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLongerThan(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForNotNull(expectedValue, nameof(expectedValue));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value.Ticks > expectedValue.Value.Ticks, ArgumentValue, ArgumentName, message,
            expectation: $"be longer than {Validator.Describe(expectedValue)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument's elapsed ticks is not longer than <paramref name="expectedValue"/>
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeLongerThan(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForNotNull(expectedValue, nameof(expectedValue));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        // Same shape as NotBeShorterThan: equal is not longer, so the negation admits equality.
        Validator.CheckGenericCondition(a => a!.Value.Ticks <= expectedValue.Value.Ticks, ArgumentValue, ArgumentName, message,
            expectation: $"not be longer than {Validator.Describe(expectedValue)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument's elapsed ticks is equal to the ticks of <paramref name="expectedValue"/>
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeEqualTo(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForNotNull(expectedValue, nameof(expectedValue));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value.Ticks == expectedValue.Value.Ticks, ArgumentValue, ArgumentName, message,
            expectation: $"be equal to {Validator.Describe(expectedValue)}");
        return _linker;
    } 

    /// <summary>
    /// Checks if the specified argument's elapsed ticks is not equal to the ticks of <paramref name="expectedValue"/>
    /// </summary>
    /// <param name="expectedValue">The value to compare the argument against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeEqualTo(TimeSpan? expectedValue, string? message = null)
    {
        Validator.CheckForNotNull(expectedValue, nameof(expectedValue));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value.Ticks != expectedValue.Value.Ticks, ArgumentValue, ArgumentName, message,
            expectation: $"not be equal to {Validator.Describe(expectedValue)}");
        return _linker;
    } 
}
