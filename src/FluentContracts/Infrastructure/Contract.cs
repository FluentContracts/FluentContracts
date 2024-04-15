namespace FluentContracts.Infrastructure
{
    public abstract class Contract<T>(T argumentValue, string argumentName)
    {
        private Linker<T>? _linker;
        protected Linker<T> Linker => _linker ??= new Linker<T>(this);
        
        protected T? ArgumentValue { get; } = argumentValue;
        protected string ArgumentName { get; } = argumentName;
        
        public Linker<T> NotBeNull(string? message = null)
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
            return Linker;
        }

        public Linker<T> BeNull(string? message = null)
        {
            Validator.CheckForNull(ArgumentValue, ArgumentName, message);
            return Linker;
        }

        public Linker<T> Be(T expectedValue, string? message = null)
        {
            Validator.CheckForSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        public Linker<T> NotBe(T expectedValue, string? message = null)
        {
            Validator.CheckForNotSpecificValue(expectedValue, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        public Linker<T> BeAnyOf(params T[] expectedValues)
        {
            return BeAnyOf(null, expectedValues);
        }

        public Linker<T> BeAnyOf(string? message, params T[] expectedValues)
        {
            Validator.CheckForAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        public Linker<T> NotBeAnyOf(params T[] expectedValues)
        {
            return NotBeAnyOf(null, expectedValues);
        }
        
        public Linker<T> NotBeAnyOf(string? message, params T[] expectedValues)
        {
            Validator.CheckForNotAnyOf(expectedValues, ArgumentValue, ArgumentName, message);
            return Linker;
        }

        public void Satisfy(Func<T?, bool> customCondition, string? message = null)
        {
            Validator.CheckGenericCondition(customCondition, ArgumentValue, ArgumentName, message);
        }
    }
}
