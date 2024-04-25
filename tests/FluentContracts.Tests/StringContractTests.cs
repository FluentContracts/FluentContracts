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
                DummyData.GetString(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<string, ArgumentNullException>(
                DummyData.GetString(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var pair = DummyData.GetStringPair();
            
            TestContract<string, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var pair = DummyData.GetStringPair();
            
            TestContract<string, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var pair = DummyData.GetStringPair();
            var array = DummyData.GetArray(() => DummyData.GetString(), pair.TestArgument, pair.DifferentArgument);
            
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
            var pair = DummyData.GetStringPair();
            var array = DummyData.GetArray(() => DummyData.GetString(), pair.TestArgument, pair.DifferentArgument);

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
                DummyData.GetString(),
                (testArgument, message) => testArgument.Must().BeEmpty(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeEmpty()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(),
                string.Empty,
                (testArgument, message) => testArgument.Must().NotBeEmpty(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeNullOrEmpty()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                string.Empty,
                DummyData.GetString(),
                (testArgument, message) => testArgument.Must().BeNullOrEmpty(message),
                "testArgument");
            
            TestContract<string, ArgumentOutOfRangeException>(
                null,
                DummyData.GetString(),
                (testArgument, message) => testArgument.Must().BeNullOrEmpty(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeNullOrEmpty()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(),
                string.Empty,
                (testArgument, message) => testArgument.Must().NotBeNullOrEmpty(message),
                "testArgument");
            
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNullOrEmpty(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeNullOrWhitespace()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(StringOption.Whitespace),
                DummyData.GetString(),
                (testArgument, message) => testArgument.Must().BeNullOrWhitespace(message),
                "testArgument");
            
            TestContract<string, ArgumentOutOfRangeException>(
                null,
                DummyData.GetString(),
                (testArgument, message) => testArgument.Must().BeNullOrWhitespace(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeNullOrWhitespace()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(),
                DummyData.GetString(StringOption.Whitespace),
                (testArgument, message) => testArgument.Must().NotBeNullOrWhitespace(message),
                "testArgument");
            
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNullOrWhitespace(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeWhitespace()
        {   
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(StringOption.Whitespace),
                DummyData.GetString(),
                (testArgument, message) => testArgument.Must().BeWhitespace(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeWhitespace()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(),
                DummyData.GetString(StringOption.Whitespace),
                (testArgument, message) => testArgument.Must().NotBeWhitespace(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeUppercase()
        {   
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(StringOption.Uppercase),
                DummyData.GetString(StringOption.Lowercase),
                (testArgument, message) => testArgument.Must().BeUppercase(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeUppercase()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(StringOption.Lowercase),
                DummyData.GetString(StringOption.Uppercase),
                (testArgument, message) => testArgument.Must().NotBeUppercase(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLowercase()
        {   
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(StringOption.Lowercase),
                DummyData.GetString(StringOption.Uppercase),
                (testArgument, message) => testArgument.Must().BeLowercase(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeLowercase()
        {
            TestContract<string, ArgumentOutOfRangeException>(
                DummyData.GetString(StringOption.Uppercase),
                DummyData.GetString(StringOption.Lowercase),
                (testArgument, message) => testArgument.Must().NotBeLowercase(message),
                "testArgument");
        }
        
        
        [Fact]
        public void Test_Must_Contain()
        {
            var pair = DummyData.GetStringPair(PairOption.Containing);
            var notContaining = DummyData.GetString();
            
            TestContract<string, ArgumentOutOfRangeException>(
                pair.TestArgument,
                notContaining,
                (testArgument, message) => testArgument.Must().Contain(pair.DifferentArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotContain()
        {
            var pair = DummyData.GetStringPair(PairOption.Containing);
            var notContaining = DummyData.GetString();
            
            TestContract<string, ArgumentOutOfRangeException>(
                notContaining,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotContain(pair.DifferentArgument, message),
                "testArgument");
        }
    }
}