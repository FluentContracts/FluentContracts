using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

/// <summary>
/// The entry point for checks on an enum argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <typeparam name="TEnum">The enum type being checked.</typeparam>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class EnumContract<TEnum>(TEnum? argumentValue, string argumentName)
    : EnumContract<TEnum, EnumContract<TEnum>>(argumentValue, argumentName)
    where TEnum : struct, Enum;

/// <summary>
/// The inheritable contract for an enum argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TEnum">The enum type being checked.</typeparam>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class EnumContract<TEnum, TContract> : ObjectContract<TEnum?, TContract>
    where TEnum : struct, Enum
    where TContract : EnumContract<TEnum, TContract>
{
    private readonly Linker<TContract> _linker;

    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected EnumContract(TEnum? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract) this);
    }

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Be(TEnum expectedValue, string? message = null)
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
    public Linker<TContract> Be(TEnum? expectedValue, string? message = null)
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
    public Linker<TContract> NotBe(TEnum expectedValue, string? message = null)
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
    public Linker<TContract> NotBe(TEnum? expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params TEnum[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params TEnum?[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(string? message, params TEnum[] expectedValues)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForAnyOf(expectedValues, ArgumentValue.Value, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(string? message, params TEnum?[] expectedValues)
    {
        Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params TEnum[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params TEnum?[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(string? message, params TEnum[] expectedValues)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForNotAnyOf(expectedValues, ArgumentValue.Value, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(string? message, params TEnum?[] expectedValues)
    {
        Validator.CheckForNotAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Enum"/> argument has a specific flag
    /// </summary>
    /// <param name="flag">The flag to check against the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> HaveFlag(TEnum flag, string? message = null)
    {
        Validator.CheckForNotNull(flag, nameof(flag));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value.HasFlag(flag), ArgumentValue, ArgumentName, message,
            expectation: $"have the flag {Validator.Describe(flag)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Enum"/> argument does not have a specific flag
    /// </summary>
    /// <param name="flag">The flag to check against the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotHaveFlag(TEnum flag, string? message = null)
    {
        Validator.CheckForNotNull(flag, nameof(flag));
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a!.Value.HasFlag(flag), ArgumentValue, ArgumentName, message,
            expectation: $"not have the flag {Validator.Describe(flag)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is a declared member of <typeparamref name="TEnum"/>.
    /// A value cast from a number the enum never declared — <c>(DayOfWeek)9</c> — fails.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>
    /// Also checks for the argument to NOT be null. On a <see cref="FlagsAttribute"/> enum, a
    /// combination of flags that is not itself a declared member is not defined — that is
    /// <see cref="Enum.IsDefined(Type, object)"/>'s behaviour, which this check follows.
    /// </remarks>
    public Linker<TContract> BeDefined(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => Enum.IsDefined(typeof(TEnum), a!.Value), ArgumentValue, ArgumentName, message,
            expectation: $"be a defined member of {typeof(TEnum)}");
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not a declared member of <typeparamref name="TEnum"/>.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>
    /// Also checks for the argument to NOT be null. On a <see cref="FlagsAttribute"/> enum, a
    /// combination of flags that is not itself a declared member is not defined — that is
    /// <see cref="Enum.IsDefined(Type, object)"/>'s behaviour, which this check follows.
    /// </remarks>
    public Linker<TContract> NotBeDefined(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !Enum.IsDefined(typeof(TEnum), a!.Value), ArgumentValue, ArgumentName, message,
            expectation: $"not be a defined member of {typeof(TEnum)}");
        return _linker;
    }
}
