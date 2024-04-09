using System;

namespace FluentContracts
{
    public abstract class Contract<T>(T argument, string defaultFallbackName, int lineNumber = 0, string filePath = "")
    {
        protected T? Argument { get; } = argument;
        protected string CallerName { get; } = CallerNameLocator.GetNameOrDefault(filePath, lineNumber, defaultFallbackName);

        public void Satisfy(Func<T?, bool> customCondition)
        {
            if (customCondition(Argument)) return;

            ThrowHelper.ThrowInvalidArgumentConditionNotSatisfiedException(
                CallerName, 
                $"Condition for argument {CallerName} was not satisfied");
        }
    }
}
