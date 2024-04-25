using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Decimal")]
    public class DecimalContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<decimal?, ArgumentOutOfRangeException>(
                null,
                DummyData.GetDecimal(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<decimal?, ArgumentNullException>(
                DummyData.GetDecimal(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var pair = DummyData.GetDecimalPair();
            
            TestContract<decimal, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var pair = DummyData.GetDecimalPair();
            
            TestContract<decimal, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var pair = DummyData.GetDecimalPair();
            var array = DummyData.GetArray(DummyData.GetDecimal, pair.TestArgument, pair.DifferentArgument);
            
            TestContract<decimal, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var pair = DummyData.GetDecimalPair();
            var array = DummyData.GetArray(DummyData.GetDecimal, pair.TestArgument, pair.DifferentArgument);

            TestContract<decimal, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetDecimal();
            var lower = success - 10;
            var higher = success + 10;
            var outOfRange = higher + 10;

            TestContract<decimal, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeBetween(lower, higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterThan()
        {
            var success = DummyData.GetDecimal();
            var lower = success - 10;
            var outOfRange = lower - 10;

            TestContract<decimal, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterThan(lower, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterOrEqualThan()
        {
            var success = DummyData.GetDecimal();
            var outOfRange = success - 10;

            TestContract<decimal, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualThan(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetDecimal();
            var higher = success + 10;
            var outOfRange = higher + 10;

            TestContract<decimal, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessThan(higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessOrEqualThan()
        {
            var success = DummyData.GetDecimal();
            var outOfRange = success + 10;

            TestContract<decimal, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualThan(success, message),
                "testArgument");
        }
    }
}