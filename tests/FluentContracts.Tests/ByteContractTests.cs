using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Byte")]
    public class ByteContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<byte?, ArgumentOutOfRangeException>(
                null,
                DummyData.GetByte(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<byte?, ArgumentNullException>(
                DummyData.GetByte(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var pair = DummyData.GetBytePair();
            
            TestContract<byte, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var pair = DummyData.GetBytePair();
            
            TestContract<byte, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var pair = DummyData.GetBytePair();
            var array = DummyData.GetArray(DummyData.GetByte, pair.TestArgument, pair.DifferentArgument);
            
            TestContract<byte, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var pair = DummyData.GetBytePair();
            var array = DummyData.GetArray(DummyData.GetByte, pair.TestArgument, pair.DifferentArgument);

            TestContract<byte, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetByte();
            var lower = (byte)(success - 10);
            var higher = (byte)(success + 10);
            var outOfRange = (byte)(higher + 10);

            TestContract<byte, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeBetween(lower, higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterThan()
        {
            var success = DummyData.GetByte();
            var lower = (byte)(success - 10);
            var outOfRange = (byte)(lower - 10);

            TestContract<byte, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterThan(lower, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterOrEqualThan()
        {
            var success = DummyData.GetByte();
            var outOfRange = (byte)(success - 10);

            TestContract<byte, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualTo(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetByte();
            var higher = (byte)(success + 10);
            var outOfRange = (byte)(higher + 10);

            TestContract<byte, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessThan(higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessOrEqualThan()
        {
            var success = DummyData.GetByte();
            var outOfRange = (byte)(success + 10);

            TestContract<byte, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualTo(success, message),
                "testArgument");
        }
    }
}