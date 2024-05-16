using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts.Struct;

public class NullableGuidContract(Guid? argumentValue, string argumentName)
    : ComparableContract<Guid?, NullableGuidContract>(argumentValue, argumentName)
{
}

public class GuidContract : ComparableContract<Guid, GuidContract>
{
    private readonly Linker<GuidContract> _linker;

    public GuidContract(Guid argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<GuidContract>(this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="Guid"/> argument is empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<GuidContract> BeEmpty(string? message = null)
    {
        Validator.CheckForSpecificValue(Guid.Empty, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Guid"/> argument is not empty.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    public Linker<GuidContract> NotBeEmpty(string? message = null)
    {
        Validator.CheckForNotSpecificValue(Guid.Empty, ArgumentValue, ArgumentName, message);
        return _linker;
    }
}