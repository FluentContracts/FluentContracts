using System;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
using Xunit;
// ReSharper disable ExpressionIsAlwaysNull

namespace FluentContracts.Tests
{
    [ContractTest("String")]
    public class StringContractTests
    {
         [Fact]
        public void Test_Must_BeNull_Satisfied()
        {
            string nullString = null;

            var action = 
                () => nullString.Must().BeNull();

            action.Should().NotThrow();
        }
        
        [Fact]
        public void Test_Must_BeNull_Throwing()
        {
            var notNullString = DummyData.GetRandomString();

            var action = 
                () => notNullString.Must().BeNull();

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(notNullString));
        }
        
        [Fact]
        public void Test_Must_BeNull_Throwing_With_Message()
        {
            var notNullString = DummyData.GetRandomString();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(notNullString));

            var action = 
                () => notNullString.Must().BeNull(expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(notNullString))
                .WithMessage(expectedError.ExceptionMessage);
        }
        
        [Fact]
        public void Test_Must_NotBeNull_Satisfied()
        {
            var notNullString = DummyData.GetRandomString();

            var action = 
                () => notNullString.Must().NotBeNull();

            action.Should().NotThrow();
        }
        
        [Fact]
        public void Test_Must_NotBeNull_Throwing()
        {
            string nullString = null;

            var action = 
                () => nullString.Must().NotBeNull();

            action
                .Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(nullString));
        }
        
        [Fact]
        public void Test_Must_NotBeNull_Throwing_With_Message()
        {
            string nullString = null;
            var expectedError = DummyData.GetRandomErrorMessage(nameof(nullString));

            var action = 
                () => nullString.Must().NotBeNull(expectedError.Message);

            action
                .Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(nullString))
                .WithMessage(expectedError.ExceptionMessage);
        }
        
        [Fact]
        public void Test_Must_BeEmpty_Satisfied()
        {
            var emptyString = string.Empty;

            var action = 
                () => emptyString.Must().BeEmpty();

            action.Should().NotThrow();
        }
        
        [Fact]
        public void Test_Must_BeEmpty_Throwing()
        {
            var notEmptyString = DummyData.GetRandomString();

            var action = 
                () => notEmptyString.Must().BeEmpty();

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(notEmptyString));
        }
        
        [Fact]
        public void Test_Must_BeEmpty_Throwing_With_Message()
        {
            var notEmptyString = DummyData.GetRandomString();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(notEmptyString));

            var action = 
                () => notEmptyString.Must().BeEmpty(expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(notEmptyString))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_BeNotEmpty_Satisfied()
        {
            var notEmptyString = DummyData.GetRandomString();

            var action = 
                () => notEmptyString.Must().NotBeEmpty();

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_NotBeEmpty_Throwing()
        {
            var emptyString = string.Empty;

            var action = 
                () => emptyString.Must().NotBeEmpty();

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(emptyString));
        }
        
        [Fact]
        public void Test_Must_NotBeEmpty_Throwing_With_Message()
        {
            var emptyString = string.Empty;
            var expectedError = DummyData.GetRandomErrorMessage(nameof(emptyString));

            var action = 
                () => emptyString.Must().NotBeEmpty(expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(emptyString))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_Be_Satisfied()
        {
            var stringArgument = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().Be(stringArgument);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_Be_Throwing()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().Be(otherString);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument));
        }
        
        [Fact]
        public void Test_Must_Be_Throwing_With_Message()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(stringArgument));

            var action = 
                () => stringArgument.Must().Be(otherString, expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_NotBe_Satisfied()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().NotBe(otherString);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_NotBe_Throwing()
        {
            var stringArgument = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().NotBe(stringArgument);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument));
        }
        
        [Fact]
        public void Test_Must_NotBe_Throwing_With_Message()
        {
            var stringArgument = DummyData.GetRandomString();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(stringArgument));

            var action = 
                () => stringArgument.Must().NotBe(stringArgument, expectedError.Message);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_Must_BeAnyOf_Satisfied()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();
            var otherString2 = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().BeAnyOf(otherString, stringArgument, otherString2);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_BeAnyOf_Throwing()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();
            var otherString2 = DummyData.GetRandomString();
            var otherString3 = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().BeAnyOf(otherString, otherString2, otherString3);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument));
        }

        [Fact]
        public void Test_Must_BeAnyOf_Throwing_With_Message()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();
            var otherString2 = DummyData.GetRandomString();
            var otherString3 = DummyData.GetRandomString();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(stringArgument));

            var action = 
                () => stringArgument.Must().BeAnyOf(expectedError.Message, otherString, otherString2, otherString3);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument))
                .WithMessage(expectedError.ExceptionMessage);
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf_Satisfied()
        {
            var stringArgument = DummyData.GetRandomString();
            
            var otherString = DummyData.GetRandomString();
            var otherString2 = DummyData.GetRandomString();
            var otherString3 = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().NotBeAnyOf(otherString, otherString2, otherString3);

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Must_NotBeAnyOf_Throwing()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();
            var otherString2 = DummyData.GetRandomString();

            var action = 
                () => stringArgument.Must().NotBeAnyOf(otherString, stringArgument, otherString2);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument));
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf_Throwing_With_Message()
        {
            var stringArgument = DummyData.GetRandomString();
            var otherString = DummyData.GetRandomString();
            var otherString2 = DummyData.GetRandomString();
            var expectedError = DummyData.GetRandomErrorMessage(nameof(stringArgument));

            var action = 
                () => stringArgument.Must().NotBeAnyOf(expectedError.Message, otherString, stringArgument, otherString2);

            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(stringArgument))
                .WithMessage(expectedError.ExceptionMessage);
        }
    }
}