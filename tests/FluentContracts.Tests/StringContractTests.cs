using System;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("String")]
    public class StringContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                null,
                DummyData.GetRandomString(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<string, ArgumentNullException>(
                DummyData.GetRandomString(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var sameArgument = DummyData.GetRandomString();
            var otherArgument = DummyData.GetRandomString();
            
            TestContract<string, ArgumentOutOfRangeException>(
                sameArgument,
                otherArgument,
                (testArgument, message) => testArgument.Must().Be(sameArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var sameArgument = DummyData.GetRandomString();
            var otherArgument = DummyData.GetRandomString();
            
            TestContract<string, ArgumentOutOfRangeException>(
                otherArgument,
                sameArgument,
                (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var argument1 = DummyData.GetRandomString();
            var argument2 = DummyData.GetRandomString();
            var argument3 = DummyData.GetRandomString();
            var argument4 = DummyData.GetRandomString();
            
            TestContract<string, ArgumentOutOfRangeException>(
                argument3,
                argument1,
                (testArgument, message) => 
                    testArgument.Must().BeAnyOf(message, argument2, argument3, argument4),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var argument1 = DummyData.GetRandomString();
            var argument2 = DummyData.GetRandomString();
            var argument3 = DummyData.GetRandomString();
            var argument4 = DummyData.GetRandomString();

            TestContract<string, ArgumentOutOfRangeException>(
                argument1,
                argument3,
                (testArgument, message) => 
                    testArgument.Must().NotBeAnyOf(message, argument2, argument3, argument4),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeEmpty()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                string.Empty,
                DummyData.GetRandomString(),
                (testArgument, message) => testArgument.Must().BeEmpty(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeEmpty()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetRandomString(),
                string.Empty,
                (testArgument, message) => testArgument.Must().NotBeEmpty(message),
                "testArgument");
        }
    }
}