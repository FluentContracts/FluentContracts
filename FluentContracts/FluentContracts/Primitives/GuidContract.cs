using System;
using System.Linq;

namespace FluentContracts.Primitives
{
    public class GuidContract : Contract<Guid>
    {
        public GuidContract(Guid argument, int lineNumber = 0, string filePath = "")
            : base(argument, lineNumber, filePath)
        {
        }

        public void NotBeEmpty()
        {
            Validator.CheckSpecificValue(Argument != Guid.Empty, CallerName);
        }

        public void BeEmpty()
        {
            Validator.CheckSpecificValue(Argument == Guid.Empty, CallerName);
        }

        public void Be(Guid expectedValue)
        {
            Validator.CheckSpecificValue(Argument == expectedValue, CallerName);
        }

        public void NotBe(Guid expectedValue)
        {
            Validator.CheckSpecificValue(Argument != expectedValue, CallerName);
        }

        public void BeAnyOf(params Guid[] expectedValues)
        {
            Validator.CheckOutOfRange(expectedValues.Any(g => g == Argument), CallerName);
        }

        public void NotBeAnyOf(params Guid[] expectedValues)
        {
            Validator.CheckOutOfRange(expectedValues.All(g => g != Argument), CallerName);
        }
    }
}