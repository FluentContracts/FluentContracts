namespace FluentContracts.Infrastructure
{
    internal static class Validator
    {
        public static void CheckGenericCondition<T>(Func<T, bool> genericCondition, T argumentValue, string argumentName, string? message = null)
        {
            if (genericCondition(argumentValue)) return;
            
            ThrowHelper.ThrowInvalidArgumentConditionNotSatisfiedException(
                argumentName, 
                message ?? $"Condition for argument {message} was not satisfied");
        }
        
        public static void CheckForAnyOf<T>(IEnumerable<T> values, T argumentValue, string argumentName, string? message = null)
        {
            if (values.Any(v => EqualityComparer<T>.Default.Equals(v, argumentValue))) return;

            ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
        }
        
        public static void CheckForNotAnyOf<T>(IEnumerable<T> values, T argumentValue, string argumentName, string? message = null)
        {
            if (values.All(v => !EqualityComparer<T>.Default.Equals(v, argumentValue))) return;

            ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
        }

        public static void CheckForSpecificValue<T>(T value, T argumentValue, string argumentName, string? message = null)
        {
            if (EqualityComparer<T>.Default.Equals(value, argumentValue)) return;

            ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
        }
        
        public static void CheckForNotSpecificValue<T>(T value, T argumentValue, string argumentName, string? message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(value, argumentValue)) return;

            ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
        }

        public static void CheckForNotNull<T>(T value, string argumentName, string? message = null)
        {
            if (value != null) return;

            ThrowHelper.ThrowArgumentNullException(argumentName, message);
        }
        
        public static void CheckForNull<T>(T value, string argumentName, string? message = null)
        {
            if (value == null) return;

            ThrowHelper.ThrowArgumentOutOfRangeException(argumentName, message);
        }
    }
}