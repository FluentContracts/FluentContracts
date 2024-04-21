using System;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Char")]
    public class CharContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<char?, ArgumentOutOfRangeException>(
                null,
                DummyData.GetRandomChar(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<char?, ArgumentNullException>(
                DummyData.GetRandomChar(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var sameArgument = DummyData.GetRandomChar();
            var otherArgument = DummyData.GetRandomChar();
            
            TestContract<char, ArgumentOutOfRangeException>(
                sameArgument,
                otherArgument,
                (testArgument, message) => testArgument.Must().Be(sameArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var sameArgument = DummyData.GetRandomChar();
            var otherArgument = DummyData.GetRandomChar();
            
            TestContract<char, ArgumentOutOfRangeException>(
                otherArgument,
                sameArgument,
                (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var argument1 = DummyData.GetRandomChar();
            var argument2 = DummyData.GetRandomChar();
            var argument3 = DummyData.GetRandomChar();
            var argument4 = DummyData.GetRandomChar();
            
            TestContract<char, ArgumentOutOfRangeException>(
                argument3,
                argument1,
                (testArgument, message) => 
                    testArgument.Must().BeAnyOf(message, argument2, argument3, argument4),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var argument1 = DummyData.GetRandomChar();
            var argument2 = DummyData.GetRandomChar();
            var argument3 = DummyData.GetRandomChar();
            var argument4 = DummyData.GetRandomChar();

            TestContract<char, ArgumentOutOfRangeException>(
                argument1,
                argument3,
                (testArgument, message) => 
                    testArgument.Must().NotBeAnyOf(message, argument2, argument3, argument4),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeDigit()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetRandomDigit(),
                DummyData.GetRandomLetter(),
                (testArgument, message) => testArgument.Must().BeDigit(message),
                "testArgument");
        }

        
        [Fact]
        public void Test_Must_NotBeDigit()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetRandomLetter(),
                DummyData.GetRandomDigit(),
                (testArgument, message) => testArgument.Must().NotBeDigit(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLetter()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetRandomLetter(),
                DummyData.GetRandomDigit(),
                (testArgument, message) => testArgument.Must().BeLetter(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeLetter()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetRandomDigit(),
                DummyData.GetRandomLetter(),
                (testArgument, message) => testArgument.Must().NotBeLetter(message),
                "testArgument");
        }
    }
}