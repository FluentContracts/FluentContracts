using System;
using FluentAssertions;
using FluentContracts.Exceptions;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Common")]
    public class ContractTests
    {
        [Fact]
        public void Test_Must_Satisfy_Satisfied()
        {
            var testString = DummyData.GetRandomString();
            var length = testString.Length;

            var action =
                () => testString.Must().Satisfy((s) => s.Length == length);

            action.Should().NotThrow();
        }
        
        [Fact]
        public void Test_Must_Satisfy_Throwing()
        {
            var testString = DummyData.GetRandomString();

            var action =
                () => testString.Must().Satisfy((s) => s.Length == 0);
            
            action
                .Should()
                .Throw<ArgumentConditionNotSatisfiedException>()
                .WithParameterName(nameof(testString))
                .WithMessage($"Condition for argument {nameof(testString)} was not satisfied (Parameter '{nameof(testString)}')");
        }
        
        [Fact]
        public void Test_Must_Satisfy_Throwing_With_Message()
        {  
            var testString = DummyData.GetRandomString();

            var expectedError = DummyData.GetRandomErrorMessage(nameof(testString));

            var action = 
                () => testString.Must().Satisfy((s) => s.Length == 0, expectedError.Message);

            action
                .Should()
                .Throw<ArgumentConditionNotSatisfiedException>()
                .WithParameterName(nameof(testString))
                .WithMessage(expectedError.ExceptionMessage);
        }

        [Fact]
        public void Test_And_Linker_Throws_On_The_First_Only()
        {
            string nullString = null;

            var action =
                // ReSharper disable once ExpressionIsAlwaysNull
                () => nullString.Must().NotBeNull().And.Be("hello");
            
            action
                .Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(nullString));
        }
        
        [Fact]
        public void Test_And_Linker_Throws_On_The_Second_Only()
        {
            var testString = "specific_value";

            var action =
                () => testString.Must().NotBeNull().And.Be("another_value");
            
            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(nameof(testString));
        }
    }
}