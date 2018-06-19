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

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Must_BeNotEmpty()
        {
            var emptyGuid = Guid.NewGuid();

            Action action = () => emptyGuid.Must().NotBeEmpty();

            action.Should().NotThrow();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Invalid_Must_BeNotEmpty()
        {
            var emptyGuid = Guid.Empty;

            Action action = () => emptyGuid.Must().NotBeEmpty();

            action.Should().Throw<InvalidArgumentValueException>();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Must_Be()
        {
            var guid = Guid.NewGuid();
            var sameGuid = guid;

            Action action = () => guid.Must().Be(sameGuid);

            action.Should().NotThrow();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Invalid_Must_Be()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();

            Action action = () => guid.Must().Be(otherGuid);

            action.Should().Throw<InvalidArgumentValueException>();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Must_NotBe()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();

            Action action = () => guid.Must().NotBe(otherGuid);

            action.Should().NotThrow();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Invalid_Must_NotBe()
        {
            var guid = Guid.NewGuid();
            var sameGuid = guid;

            Action action = () => guid.Must().NotBe(sameGuid);

            action.Should().Throw<InvalidArgumentValueException>();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Must_BeAnyOf()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var sameGuid = guid;

            Action action = () => guid.Must().BeAnyOf(otherGuid, sameGuid, otherGuid2);

            action.Should().NotThrow();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Invalid_Must_BeAnyOf()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var otherGuid3 = Guid.NewGuid();

            Action action = () => guid.Must().BeAnyOf(otherGuid, otherGuid2, otherGuid3);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }


        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Must_NotBeAnyOf()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var otherGuid3 = Guid.NewGuid();

            Action action = () => guid.Must().NotBeAnyOf(otherGuid, otherGuid2, otherGuid3);

            action.Should().NotThrow();
        }

        [Fact]
        [Trait("Guid Contracts", null)]
        public void Test_Invalid_Must_NotBeAnyOf()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var sameGuid = guid;

            Action action = () => guid.Must().NotBeAnyOf(otherGuid, sameGuid, otherGuid2);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}