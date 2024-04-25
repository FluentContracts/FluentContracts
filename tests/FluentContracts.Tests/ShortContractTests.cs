using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Short")]
    public class ShortContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<short?, ArgumentOutOfRangeException>(
                null,
                DummyData.GetShort(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<short?, ArgumentNullException>(
                DummyData.GetShort(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var pair = DummyData.GetShortPair();
            
            TestContract<short, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var pair = DummyData.GetShortPair();
            
            TestContract<short, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var pair = DummyData.GetShortPair();
            var array = DummyData.GetArray(DummyData.GetShort, pair.TestArgument, pair.DifferentArgument);
            
            TestContract<short, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var pair = DummyData.GetShortPair();
            var array = DummyData.GetArray(DummyData.GetShort, pair.TestArgument, pair.DifferentArgument);

            TestContract<short, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetShort();
            var lower = (short)(success - 10);
            var higher = (short)(success + 10);
            var outOfRange = (short)(higher + 10);

            TestContract<short, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeBetween(lower, higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterThan()
        {
            var success = DummyData.GetShort();
            var lower = (short)(success - 10);
            var outOfRange = (short)(lower - 10);

            TestContract<short, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterThan(lower, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterOrEqualThan()
        {
            var success = DummyData.GetShort();
            var outOfRange = (short)(success - 10);

            TestContract<short, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualTo(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetShort();
            var higher = (short)(success + 10);
            var outOfRange = (short)(higher + 10);

            TestContract<short, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessThan(higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessOrEqualThan()
        {
            var success = DummyData.GetShort();
            var outOfRange = (short)(success + 10);

            TestContract<short, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualTo(success, message),
                "testArgument");
        }
    }
}