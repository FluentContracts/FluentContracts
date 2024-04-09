using System;
using FluentAssertions;
using FluentContracts.Exceptions;
using FluentContracts.Primitives;
using Xunit;

namespace FluentContracts.Tests
{
    public class InfrastructureTests
    {
        [Fact]
        [Trait("Infrastructure", null)]
        public void Test_Caller_Name_Discovery()
        {
            var someFancyGuid = Guid.Empty;

            Action failed = () => someFancyGuid.Must().NotBeEmpty();
            failed.Should().Throw<InvalidArgumentValueException>().WithMessage(
                "Exception of type 'FluentContracts.Exceptions.InvalidArgumentValueException' was thrown.\r\nParameter name:  someFancyGuid");
        }
    }
}