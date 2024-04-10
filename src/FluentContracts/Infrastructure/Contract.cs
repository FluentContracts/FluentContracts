namespace FluentContracts.Infrastructure
{
    public abstract class Contract<T>(T argumentValue, string argumentName)
    {
        private Linker<T>? _linker;
        protected Linker<T> Linker => _linker ??= new Linker<T>(this);
        
        protected T? ArgumentValue { get; } = argumentValue;
        protected string ArgumentName { get; } = argumentName;
        
        public Linker<T> NotBeNull()
        {
            Validator.CheckForNotNull(ArgumentValue, ArgumentName);
            return Linker;
        }

        public Linker<T> BeNull()
        {
            Validator.CheckForNull(ArgumentValue, ArgumentName);
            return Linker;
        }

        public void Satisfy(Func<T?, bool> customCondition, string? message = null)
        {
            Validator.CheckGenericCondition(customCondition, ArgumentValue, ArgumentName, message);
        }
    }
}
