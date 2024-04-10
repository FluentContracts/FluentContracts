using System.Runtime.CompilerServices;
using FluentContracts.Contracts;

namespace FluentContracts

{
    public static class ContractExtensions
    {
        public static GuidContract Must(
            this Guid? argumentValue,
            [CallerArgumentExpression("argumentValue")] string? argumentName = default)
        {
            return new GuidContract(argumentValue, argumentName);
        }
        
        public static GuidContract Must(
            this Guid argumentValue,
            [CallerArgumentExpression("argumentValue")] string? argumentName = default)
        {
            return new GuidContract(argumentValue, argumentName);
        }
        
        public static StringContract Must(
            this string? argumentValue,
            [CallerArgumentExpression("argumentValue")] string? argumentName = default)
        {
            return new StringContract(argumentValue, argumentName);
        }
    }
}