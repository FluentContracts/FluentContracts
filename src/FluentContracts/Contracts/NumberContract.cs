using System.Diagnostics.Contracts;
using FluentContracts.Infrastructure;

namespace FluentContracts.Contracts;

public class NumberContract<T>(T argumentValue, string argumentName) 
    : BaseContract<T>(argumentValue, argumentName)
{
    [Pure]
    public Linker<T> BeBetween(T start, T end, string? message = null)
    {
        Validator.CheckForBetween(start, end, ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    [Pure]
    public Linker<T> BeGreaterThan(T value, string? message = null)
    {
        Validator.CheckForGreaterThan(value, ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    [Pure]
    public Linker<T> BeGreaterOrEqualThan(T value, string? message = null)
    {
        Validator.CheckForGreaterOrEqualThan(value, ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    [Pure]
    public Linker<T> BeLessThan(T value, string? message = null)
    {
        Validator.CheckForLessThan(value, ArgumentValue, ArgumentName, message);
        return Linker;
    }
    
    [Pure]
    public Linker<T> BeLessOrEqualThan(T value, string? message = null)
    {
        Validator.CheckForLessOrEqualThan(value, ArgumentValue, ArgumentName, message);
        return Linker;
    }
}