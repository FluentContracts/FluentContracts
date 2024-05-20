using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Struct;

public class NullableEnumContract<TEnum>(TEnum? argumentValue, string argumentName)
    : EqualityContract<TEnum?, NullableEnumContract<TEnum>>(argumentValue, argumentName);

public class EnumContract<TEnum> : EqualityContract<TEnum, EnumContract<TEnum>>
    where TEnum : struct, Enum
{
    private readonly Linker<EnumContract<TEnum>> _linker;

    public EnumContract(TEnum argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<EnumContract<TEnum>>(this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="Enum"/> argument has a specific flag
    /// </summary>
    /// <param name="flag">The flag to check against the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<EnumContract<TEnum>> HaveFlag(TEnum flag, string? message = null)
    {
        Validator.CheckGenericCondition(a => a.HasFlag(flag), ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Enum"/> argument does not have a specific flag
    /// </summary>
    /// <param name="flag">The flag to check against the argument</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<EnumContract<TEnum>> NotHaveFlag(TEnum flag, string? message = null)
    {
        Validator.CheckGenericCondition(a => !a.HasFlag(flag), ArgumentValue, ArgumentName, message);
        return _linker;
    }
}