#if NET8_0_OR_GREATER
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

// The whole file is net8-only: DateOnly does not exist on netstandard2.0 and there is no
// sensible polyfill without taking a dependency, so consumers there simply do not see it.

/// <summary>
/// The entry point for checks on a <see cref="System.DateOnly"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
/// <param name="dateTimeProvider">Supplies the current date to the checks that need one. Defaults to the system clock.</param>
public class DateOnlyContract(
    DateOnly? argumentValue,
    string argumentName,
    IDateTimeProvider? dateTimeProvider = null)
    : DateOnlyContract<DateOnlyContract>(argumentValue, argumentName, dateTimeProvider);

/// <summary>
/// The inheritable contract for a <see cref="System.DateOnly"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class DateOnlyContract<TContract> : EqualityContract<DateOnly?, TContract>
    where TContract : DateOnlyContract<TContract>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    /// <param name="dateTimeProvider">Supplies the current date to the checks that need one. Defaults to the system clock.</param>
    protected DateOnlyContract(
        DateOnly? argumentValue,
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
    public TContract BeGreaterThan(DateOnly value, string? message = null)
    {
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than or equal to <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be greater than or equal to</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeGreaterOrEqualTo(DateOnly value, string? message = null)
    {
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is less than <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be less than</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessThan(DateOnly value, string? message = null)
    {
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the argument is less than or equal to <paramref name="value"/>
    /// </summary>
    /// <param name="value">Value that the argument must be less than or equal to</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeLessOrEqualTo(DateOnly value, string? message = null)
    {
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
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
    public TContract BeBetween(DateOnly start, DateOnly end, string? message = null)
    {
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is before today.
    /// Today itself is neither past nor future.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInThePast(string? message = null) => BeInThePast(Today, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is before <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInThePast(DateOnly referenceDate, string? message = null) =>
        BeLessThan(referenceDate, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is not before today.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInThePast(string? message = null) => NotBeInThePast(Today, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is not before <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInThePast(DateOnly referenceDate, string? message = null) =>
        BeGreaterOrEqualTo(referenceDate, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is after today.
    /// Today itself is neither past nor future.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInTheFuture(string? message = null) => BeInTheFuture(Today, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is after <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeInTheFuture(DateOnly referenceDate, string? message = null) =>
        BeGreaterThan(referenceDate, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is not after today.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInTheFuture(string? message = null) => NotBeInTheFuture(Today, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is not after <paramref name="referenceDate"/>
    /// </summary>
    /// <param name="referenceDate">Reference date to be used as a point for evaluation of the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeInTheFuture(DateOnly referenceDate, string? message = null) =>
        BeLessOrEqualTo(referenceDate, message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is today.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForSpecificValue(Today, ArgumentValue.Value, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument is not today.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeToday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotSpecificValue(Today, ArgumentValue.Value, ArgumentName, message);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument falls on a weekday (Monday through Friday).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWeekday(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !IsWeekend(a!.Value), ArgumentValue, ArgumentName, message,
            expectation: "fall on a weekday");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument does not fall on a weekday.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWeekday(string? message = null) => BeWeekend(message);

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument falls on a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeWeekend(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => IsWeekend(a!.Value), ArgumentValue, ArgumentName, message,
            expectation: "fall on a weekend");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="DateOnly"/> argument does not fall on a weekend.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeWeekend(string? message = null) => BeWeekday(message);

    private static bool IsWeekend(DateOnly date) =>
        date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    /// The current date, taken from the injected <see cref="IDateTimeProvider"/> so tests can control it.
    /// </summary>
    private DateOnly Today => DateOnly.FromDateTime(_dateTimeProvider.Now);
}
#endif
