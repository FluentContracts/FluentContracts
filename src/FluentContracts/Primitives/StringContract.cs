using System.Linq;

namespace FluentContracts.Primitives
{
    public class StringContract : Contract<string>
    {
        private const string DefaultFallbackName = "String argument";

        public StringContract(string argument, int lineNumber = 0, string filePath = "")
            : base(argument, DefaultFallbackName, lineNumber, filePath)
        {
        }

        private Linker<StringContract> _linker;
        protected Linker<StringContract> Linker => _linker ?? (_linker = new Linker<StringContract>(this));

        public Linker<StringContract> NotBeNull()
        {
            Validator.CheckSpecificValue(Argument != null, CallerName);
            return Linker;
        }

        public Linker<StringContract> BeNull()
        {
            Validator.CheckSpecificValue(Argument == null, CallerName);
            return Linker;
        }

        public Linker<StringContract> NotBeEmpty()
        {
            Validator.CheckSpecificValue(Argument != string.Empty, CallerName);
            return Linker;
        }

        public Linker<StringContract> BeEmpty()
        {
            Validator.CheckSpecificValue(Argument == string.Empty, CallerName);
            return Linker;
        }

        public Linker<StringContract> NotBeNullOrEmpty()
        {
            Validator.CheckSpecificValue(!string.IsNullOrEmpty(Argument), CallerName);
            return Linker;
        }

        public Linker<StringContract> BeNullOrEmpty()
        {
            Validator.CheckSpecificValue(string.IsNullOrEmpty(Argument), CallerName);
            return Linker;
        }

        public Linker<StringContract> Be(string expectedValue)
        {
            Validator.CheckSpecificValue(Argument == expectedValue, CallerName);
            return Linker;
        }

        public Linker<StringContract> NotBe(string expectedValue)
        {
            Validator.CheckSpecificValue(Argument != expectedValue, CallerName);
            return Linker;
        }

        public Linker<StringContract> BeAnyOf(params string[] expectedValues)
        {
            Validator.CheckOutOfRange(expectedValues.Any(g => g == Argument), CallerName);
            return Linker;
        }

        public Linker<StringContract> NotBeAnyOf(params string[] expectedValues)
        {
            Validator.CheckOutOfRange(expectedValues.All(g => g != Argument), CallerName);
            return Linker;
        }
    }
}