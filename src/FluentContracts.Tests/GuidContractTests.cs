using System;
using FluentAssertions;
using FluentContracts.Exceptions;
using FluentContracts.Primitives;
using Xunit;

namespace FluentContracts.Tests
{
    public class GuidContractTests
    {
        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Must_BeEmpty()
        {
            var notEmptyGuid = Guid.NewGuid();

            Action action = () => notEmptyGuid.Must().BeEmpty();

            action.Should().Throw<InvalidArgumentValueException>();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Invalid_Must_BeEmpty()
        {
            var emptyGuid = Guid.Empty;

            Action action = () => emptyGuid.Must().BeEmpty();

            action.Should().NotThrow();
        }
    }
}