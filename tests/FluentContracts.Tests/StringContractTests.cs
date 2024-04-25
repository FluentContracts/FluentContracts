using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
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
            var pair = DummyData.GetRandomStringPair();
            
            TestContract<string, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var pair = DummyData.GetRandomStringPair();
            
            TestContract<string, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var pair = DummyData.GetRandomStringPair();
            var array = DummyData.GetArray(DummyData.GetRandomString, pair.TestArgument, pair.DifferentArgument);
            
            TestContract<string, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var pair = DummyData.GetRandomStringPair();
            var array = DummyData.GetArray(DummyData.GetRandomString, pair.TestArgument, pair.DifferentArgument);

            TestContract<string, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
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