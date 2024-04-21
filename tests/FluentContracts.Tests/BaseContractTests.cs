using System;
using Bogus;
using FluentAssertions;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Common")]
    public class BaseContractTests : Tests
    {
        [Fact]
        public void Test_Must_Satisfy()
        {
            var success = DummyData.GetRandomPerson();
            var failing = DummyData.GetRandomPerson();
            Func<Person, bool> testCondition = p => p.Email == success.Email;
            
            TestContract<Person, ArgumentOutOfRangeException>(
                success,
                failing,
                (testArgument, message) => 
                    testArgument.Must().Satisfy(testCondition, message),
                "testArgument");
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