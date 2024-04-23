using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
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
            var included = DummyData.GetRandomChar();
            var excluded = DummyData.GetRandomChar();
            var array = DummyData.GetArray(DummyData.GetRandomChar, included, excluded);
            
            TestContract<char, ArgumentOutOfRangeException>(
                included,
                excluded,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var included = DummyData.GetRandomChar();
            var excluded = DummyData.GetRandomChar();
            var array = DummyData.GetArray(DummyData.GetRandomChar, included, excluded);

            TestContract<char, ArgumentOutOfRangeException>(
                excluded,
                included,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
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