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
                DummyData.GetChar(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<char?, ArgumentNullException>(
                DummyData.GetChar(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var pair = DummyData.GetCharPair();
            
            TestContract<char, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var pair = DummyData.GetCharPair();
            
            TestContract<char, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var pair = DummyData.GetCharPair();
            var array = DummyData.GetArray(DummyData.GetChar, pair.TestArgument, pair.DifferentArgument);
            
            TestContract<char, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var pair = DummyData.GetCharPair();
            var array = DummyData.GetArray(DummyData.GetChar, pair.TestArgument, pair.DifferentArgument);

            TestContract<char, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeDigit()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetDigit(),
                DummyData.GetLetter(),
                (testArgument, message) => testArgument.Must().BeDigit(message),
                "testArgument");
        }

        
        [Fact]
        public void Test_Must_NotBeDigit()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetLetter(),
                DummyData.GetDigit(),
                (testArgument, message) => testArgument.Must().NotBeDigit(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLetter()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetLetter(),
                DummyData.GetDigit(),
                (testArgument, message) => testArgument.Must().BeLetter(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeLetter()
        {
            TestContract<char, ArgumentOutOfRangeException>(
                DummyData.GetDigit(),
                DummyData.GetLetter(),
                (testArgument, message) => testArgument.Must().NotBeLetter(message),
                "testArgument");
        }
    }
}