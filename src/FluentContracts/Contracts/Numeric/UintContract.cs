using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Numeric;

public class UintContract(uint? argumentValue, string argumentName)
    : UintContract<UintContract>(argumentValue, argumentName);

public class UintContract<TContract> : ObjectContract<uint?, TContract>
    where TContract : UintContract<TContract>
{
    private const uint Zero = 0;
    private readonly Linker<TContract> _linker;

    protected UintContract(uint? argumentValue, string argumentName)
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
    public Linker<TContract> Be(uint expectedValue, string? message = null)
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
    public Linker<TContract> Be(uint? expectedValue, string? message = null)
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
    public Linker<TContract> NotBe(uint expectedValue, string? message = null)
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
    public Linker<TContract> NotBe(uint? expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params uint[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params uint?[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(string? message, params uint[] expectedValues)
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
    public Linker<TContract> BeAnyOf(string? message, params uint?[] expectedValues)
    {
        Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params uint[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params uint?[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(string? message, params uint[] expectedValues)
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
    public Linker<TContract> NotBeAnyOf(string? message, params uint?[] expectedValues)
    {
        Validator.CheckForNotAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is inclusively between the values of <see cref="start"/> and <see cref="end"/>
    /// </summary>
    /// <param name="start">Value that must be less or equal to the argument</param>
    /// <param name="end">Value that must be greater or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeBetween(uint start, uint end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is inclusively between the values of <see cref="start"/> and <see cref="end"/>
    /// </summary>
    /// <param name="start">Value that must be less or equal to the argument</param>
    /// <param name="end">Value that must be greater or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeBetween(uint? start, uint? end, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterThan(uint value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterThan(uint? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterOrEqualTo(uint value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterOrEqualTo(uint? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is less than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessThan(uint value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is less than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessThan(uint? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessOrEqualTo(uint value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be less or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessOrEqualTo(uint? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument is equal to zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeZero(string? message = null)
    {
        Validator.CheckForSpecificValue(Zero, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument is not equal to zero
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeZero(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Zero, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument has an odd value
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeOdd(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value % 2 != 0, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument does not have an odd value
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeOdd(string? message = null)
    {
        return BeEven(message);
    }
    
    /// <summary>
    /// Checks if the value of the argument has an even value
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeEven(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a!.Value % 2 == 0, ArgumentValue, ArgumentName, message);
        return _linker;
    }
    
    /// <summary>
    /// Checks if the value of the argument does not have an even value
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeEven(string? message = null)
    {
        return BeOdd(message);
    }
}