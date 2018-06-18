using System;
using System.Linq;

namespace FluentContracts.Primitives
{
    public class GuidContract : Contract<Guid>
    {
        private const string DefaultFallbackName = "Guid argument";

        public GuidContract(Guid argument, int lineNumber = 0, string filePath = "")
            : base(argument, DefaultFallbackName, lineNumber, filePath)
        {
        }

        private Linker<GuidContract> _linker;
        protected Linker<GuidContract> Linker => _linker ?? (_linker = new Linker<GuidContract>(this));

        public Linker<GuidContract> NotBeEmpty()
        {
            Validator.CheckSpecificValue(Argument != Guid.Empty, CallerName);
            return Linker;
        }

        public Linker<GuidContract> BeEmpty()
        {
            Validator.CheckSpecificValue(Argument == Guid.Empty, CallerName);
            return Linker;
        }

        public Linker<GuidContract> Be(Guid expectedValue)
        {
            Validator.CheckSpecificValue(Argument == expectedValue, CallerName);
            return Linker;
        }

        public Linker<GuidContract> NotBe(Guid expectedValue)
        {
            Validator.CheckSpecificValue(Argument != expectedValue, CallerName);
            return Linker;
        }

        public Linker<GuidContract> BeAnyOf(params Guid[] expectedValues)
        {
            Validator.CheckOutOfRange(expectedValues.Any(g => g == Argument), CallerName);
            return Linker;
        }

        public Linker<GuidContract> NotBeAnyOf(params Guid[] expectedValues)
        {
            Validator.CheckOutOfRange(expectedValues.All(g => g != Argument), CallerName);
            return Linker;
        }
    }
}