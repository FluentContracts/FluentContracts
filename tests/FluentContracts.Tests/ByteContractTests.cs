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
                DummyData.GetRandomByte(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<byte?, ArgumentNullException>(
                DummyData.GetRandomByte(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var sameArgument = DummyData.GetRandomByte();
            var otherArgument = DummyData.GetRandomByte();
            
            TestContract<byte, ArgumentOutOfRangeException>(
                sameArgument,
                otherArgument,
                (testArgument, message) => testArgument.Must().Be(sameArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var sameArgument = DummyData.GetRandomByte();
            var otherArgument = DummyData.GetRandomByte();
            
            TestContract<byte, ArgumentOutOfRangeException>(
                otherArgument,
                sameArgument,
                (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var included = DummyData.GetRandomByte();
            var excluded = DummyData.GetRandomByte();
            var array = DummyData.GetArray(DummyData.GetRandomByte, included, excluded);
            
            TestContract<byte, ArgumentOutOfRangeException>(
                included,
                excluded,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var included = DummyData.GetRandomByte();
            var excluded = DummyData.GetRandomByte();
            var array = DummyData.GetArray(DummyData.GetRandomByte, included, excluded);

            TestContract<byte, ArgumentOutOfRangeException>(
                excluded,
                included,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetRandomByte();
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
            var success = DummyData.GetRandomByte();
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
            var success = DummyData.GetRandomByte();
            var outOfRange = (byte)(success - 10);

            TestContract<byte, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualThan(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetRandomByte();
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
            var success = DummyData.GetRandomByte();
            var outOfRange = (byte)(success + 10);

            TestContract<byte, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualThan(success, message),
                "testArgument");
        }
    }
}