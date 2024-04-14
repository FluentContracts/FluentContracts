using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Guid")]
    public class GuidContractTests
    {
        [Fact]
        public void Test_Must_BeEmpty_Satisfied()
        {
            var emptyGuid = Guid.Empty;

            var action = 
                () => emptyGuid.Must().BeEmpty();

            action.Should().NotThrow();
        }
        
        [Fact]
        public void Test_Must_BeEmpty_Throwing()
        {
            var notEmptyGuid = Guid.NewGuid();

            var action = 
                () => notEmptyGuid.Must().BeEmpty();

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(notEmptyGuid));
        }
        
        [Fact]
        public void Test_Must_BeEmpty_Throwing_With_Message()
        {
            var notEmptyGuid = Guid.NewGuid();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(notEmptyGuid));

            var action = 
                () => notEmptyGuid.Must().BeEmpty(expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(notEmptyGuid))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_NotBeEmpty_Satisfied()
        {
            var emptyGuid = Guid.NewGuid();

            var action = 
                () => emptyGuid.Must().NotBeEmpty();

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_NotBeEmpty_Throwing()
        {
            var emptyGuid = Guid.Empty;

            var action = 
                () => emptyGuid.Must().NotBeEmpty();

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(emptyGuid));
        }
        
        [Fact]
        public void Test_Must_NotBeEmpty_Throwing_With_Message()
        {
            var emptyGuid = Guid.Empty;
            var expectedError = DummyData.GetRandomErrorMessage(nameof(emptyGuid));

            var action = 
                () => emptyGuid.Must().NotBeEmpty(expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(emptyGuid))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_Be_Satisfied()
        {
            var guid = Guid.NewGuid();

            var action = 
                () => guid.Must().Be(guid);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_Be_Throwing()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();

            var action = 
                () => guid.Must().Be(otherGuid);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid));
        }
        
        [Fact]
        public void Test_Must_Be_Throwing_With_Message()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(guid));

            var action = 
                () => guid.Must().Be(otherGuid, expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_NotBe_Satisfied()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();

            var action = 
                () => guid.Must().NotBe(otherGuid);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_NotBe_Throwing()
        {
            var guid = Guid.NewGuid();

            var action = 
                () => guid.Must().NotBe(guid);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid));
        }
        
        [Fact]
        public void Test_Must_NotBe_Throwing_With_Message()
        {
            var guid = Guid.NewGuid();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(guid));

            var action = 
                () => guid.Must().NotBe(guid, expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_BeAnyOf_Satisfied()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();

            var action = 
                () => guid.Must().BeAnyOf(otherGuid, guid, otherGuid2);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_BeAnyOf_Throwing()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var otherGuid3 = Guid.NewGuid();

            var action = 
                () => guid.Must().BeAnyOf(otherGuid, otherGuid2, otherGuid3);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid));
        }

        [Fact]
        public void Test_Must_BeAnyOf_Throwing_With_Message()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var otherGuid3 = Guid.NewGuid();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(guid));

            var action = 
                () => guid.Must().BeAnyOf(expectedError.Message, otherGuid, otherGuid2, otherGuid3);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid))
                .WithMessage(expectedError.ExceptionMessage);
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf_Satisfied()
        {
            var guid = Guid.NewGuid();
            
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var otherGuid3 = Guid.NewGuid();

            var action = 
                () => guid.Must().NotBeAnyOf(otherGuid, otherGuid2, otherGuid3);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_NotBeAnyOf_Throwing()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();

            var action = 
                () => guid.Must().NotBeAnyOf(otherGuid, guid, otherGuid2);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid));
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf_Throwing_With_Message()
        {
            var guid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var otherGuid2 = Guid.NewGuid();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(guid));

            var action = 
                () => guid.Must().NotBeAnyOf(expectedError.Message, otherGuid, guid, otherGuid2);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(guid))
                .WithMessage(expectedError.ExceptionMessage);
        }
    }
}