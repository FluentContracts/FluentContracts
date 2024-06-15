using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Text;


public class CharContract(char? argumentValue, string argumentName) 
    : CharContract<CharContract>(argumentValue, argumentName);

public class CharContract<TContract> : NullableContract<char?, TContract>
    where TContract : CharContract<TContract>
{
    private readonly Linker<TContract> _linker;

    protected CharContract(char? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    } 

    /// <summary>
    /// Checks if the specified argument is equal to the expected value.
    /// </summary>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> Be(char expectedValue, string? message = null)
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
    public Linker<TContract> Be(char? expectedValue, string? message = null)
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
    public Linker<TContract> NotBe(char expectedValue, string? message = null)
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
    public Linker<TContract> NotBe(char? expectedValue, string? message = null)
    {
        Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
        return _linker;
    } 
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params char[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }
    
    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(params char?[] expectedValues)
    {
        return BeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">Expected values among which the argument can be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAnyOf(string? message, params char[] expectedValues)
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
    public Linker<TContract> BeAnyOf(string? message, params char?[] expectedValues)
    {
        Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params char[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(params char?[] expectedValues)
    {
        return NotBeAnyOf(null, expectedValues);
    }

    /// <summary>
    /// Checks if the specified argument is not any of the expected values.
    /// </summary>
    /// <param name="message">The optional error message to include in the exception.</param>
    /// <param name="expectedValues">The expected values that the argument must not be.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAnyOf(string? message, params char[] expectedValues)
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
    public Linker<TContract> NotBeAnyOf(string? message, params char?[] expectedValues)
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
    public Linker<TContract> BeBetween(char start, char end, string? message = null)
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
    public Linker<TContract> BeBetween(char? start, char? end, string? message = null)
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
    public Linker<TContract> BeGreaterThan(char value, string? message = null)
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
    public Linker<TContract> BeGreaterThan(char? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is greater or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be lower or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeGreaterOrEqualTo(char value, string? message = null)
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
    public Linker<TContract> BeGreaterOrEqualTo(char? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForGreaterOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessThan(char value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower than <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be greater than the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessThan(char? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be lower or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessOrEqualTo(char value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the argument is lower or equal to the <see cref="value"/>
    /// </summary>
    /// <param name="value">Value that must be lower or equal to the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeLessOrEqualTo(char? value, string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckForLessOrEqualTo(value, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a digit
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeDigit(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => char.IsDigit(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not a digit
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeDigit(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !char.IsDigit(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a letter
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeLetter(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => char.IsLetter(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not a letter
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeLetter(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !char.IsLetter(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is alphanumeric
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAlphanumeric(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => char.IsLetterOrDigit(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not alphanumeric
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAlphanumeric(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !char.IsLetterOrDigit(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is lowercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeLowercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => char.IsLower(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not lowercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeLowercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !char.IsLower(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is uppercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeUppercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => char.IsUpper(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not uppercase
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeUppercase(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !char.IsUpper(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is a whitespace
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeWhiteSpace(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => char.IsWhiteSpace(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not a whitespace
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeWhiteSpace(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !char.IsWhiteSpace(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is an ASCII character
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> BeAscii(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => char.IsAscii(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="char"/> argument is not an ASCII character
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<TContract> NotBeAscii(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !char.IsAscii(a!.Value), ArgumentValue, ArgumentName, message);
        return _linker;
    }
}