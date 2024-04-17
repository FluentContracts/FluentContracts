using System.Runtime.CompilerServices;
using FluentContracts.Contracts;

namespace FluentContracts

{
    public static class ContractExtensions
    {
        private const string DefaultArgumentName = "argument";
        
        public static GuidContract Must(
            this Guid? argumentValue,
            [CallerArgumentExpression("argumentValue")] string argumentName = DefaultArgumentName)
        {
            return new GuidContract(argumentValue, argumentName);
        }
        
        public static GuidContract Must(
            this Guid argumentValue,
            [CallerArgumentExpression("argumentValue")] string argumentName = DefaultArgumentName)
        {
            return new GuidContract(argumentValue, argumentName);
        }
        
        public static StringContract Must(
            this string? argumentValue,
            [CallerArgumentExpression("argumentValue")] string argumentName = DefaultArgumentName)
        {
            return new StringContract(argumentValue, argumentName);
        }
    }
}