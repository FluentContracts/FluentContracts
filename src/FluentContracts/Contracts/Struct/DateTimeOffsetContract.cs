using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

/// <summary>
/// The entry point for checks on a <see cref="System.DateTimeOffset"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
/// <param name="dateTimeProvider">Supplies the current moment to the checks that need one. Defaults to the system clock.</param>
public class DateTimeOffsetContract(
    DateTimeOffset? argumentValue,
    string argumentName,
    IDateTimeProvider? dateTimeProvider = null)
    : DateTimeOffsetContract<DateTimeOffsetContract>(argumentValue, argumentName, dateTimeProvider);

/// <summary>
/// The inheritable contract for a <see cref="System.DateTimeOffset"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class DateTimeOffsetContract<TContract> : EqualityContract<DateTimeOffset?, TContract>
    where TContract : DateTimeOffsetContract<TContract>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    /// <param name="dateTimeProvider">Supplies the current moment to the checks that need one. Defaults to the system clock.</param>
    protected DateTimeOffsetContract(
        DateTimeOffset? argumentValue,
        string argumentName,
        IDateTimeProvider? dateTimeProvider = null)
        : base(argumentValue, argumentName)
    {
        _dateTimeProvider = dateTimeProvider ?? new DotNetDateTimeProvider();
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be greater than</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterThan(DateTimeOffset value, string? message = null)
    {
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than or equal to <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be greater than or equal to</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterOrEqualTo(DateTimeOffset value, string? message = null)
    {
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is less than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be less than</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessThan(DateTimeOffset value, string? message = null)
    {
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is less than or equal to <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be less than or equal to</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessOrEqualTo(DateTimeOffset value, string? message = null)
    {
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is inclusively between <paramref name="start"/> and <paramref name="end"/>
    /// </summary>
    /// <param name="start">Value that must be less or equal to the argument</param>
    /// <param name="end">Value that must be greater or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeBetween(DateTimeOffset start, DateTimeOffset end, string? message = null)
    {
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument has a zero offset from UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeUtc(string? message = null) => HaveOffset(TimeSpan.Zero, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument does not have a zero offset from UTC.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeUtc(string? message = null) => NotHaveOffset(TimeSpan.Zero, message);

    /// <summary>
    /// Checks if the offset of the <see cref="DateTimeOffset"/> argument is <paramref name="offset"/>.
    /// </summary>
    /// <param name="offset">The expected offset from UTC</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract HaveOffset(TimeSpan offset, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForSpecificValue(offset, ArgumentValue.Value.Offset, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the offset of the <see cref="DateTimeOffset"/> argument is not <paramref name="offset"/>.
    /// </summary>
    /// <param name="offset">The offset from UTC the argument must not have</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotHaveOffset(TimeSpan offset, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckForNotSpecificValue(offset, ArgumentValue.Value.Offset, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is in the past.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInThePast(string? message = null) => BeInThePast(Now, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is in the past from
    /// <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInThePast(DateTimeOffset referenceDate, string? message = null) =>
        BeLessThan(referenceDate, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is not in the past.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInThePast(string? message = null) => NotBeInThePast(Now, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is not in the past from
    /// <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInThePast(DateTimeOffset referenceDate, string? message = null) =>
        BeGreaterOrEqualTo(referenceDate, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is in the future.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInTheFuture(string? message = null) => BeInTheFuture(Now, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is in the future from
    /// <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInTheFuture(DateTimeOffset referenceDate, string? message = null) =>
        BeGreaterThan(referenceDate, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is not in the future.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInTheFuture(string? message = null) => NotBeInTheFuture(Now, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateTimeOffset"/> argument is not in the future from
    /// <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInTheFuture(DateTimeOffset referenceDate, string? message = null) =>
        BeLessOrEqualTo(referenceDate, message);

    /// <summary>
    /// The current moment, taken from the injected <see cref="IDateTimeProvider"/> so tests can control it.
    /// </summary>
    /// <remarks>
    /// <see cref="IDateTimeProvider.Now"/> returns a local <see cref="DateTime"/>, and converting it keeps
    /// that local offset, so comparisons stay correct regardless of the argument's own offset.
    /// </remarks>
    private DateTimeOffset Now => new(_dateTimeProvider.Now);
}
