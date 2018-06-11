namespace FluentContracts
{
    public abstract class Contract<T>
    {
        protected Contract(T argument, string defaultFallbackName, int lineNumber = 0, string filePath = "")
        {
            Argument = argument;
            CallerName = CallerNameLocator.GetNameOrDefault(filePath, lineNumber, defaultFallbackName);
        }

        protected T Argument { get; }
        protected string CallerName { get; }
    }
}
