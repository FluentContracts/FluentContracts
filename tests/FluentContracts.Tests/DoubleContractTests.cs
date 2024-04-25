using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Double")]
    public class DoubleContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<double?, ArgumentOutOfRangeException>(
                null,
                DummyData.GetDouble(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<double?, ArgumentNullException>(
                DummyData.GetDouble(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var pair = DummyData.GetDoublePair();
            
            TestContract<double, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var pair = DummyData.GetDoublePair();
            
            TestContract<double, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var pair = DummyData.GetDoublePair();
            var array = DummyData.GetArray(DummyData.GetDouble, pair.TestArgument, pair.DifferentArgument);
            
            TestContract<double, ArgumentOutOfRangeException>(
                pair.TestArgument,
                pair.DifferentArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var pair = DummyData.GetDoublePair();
            var array = DummyData.GetArray(DummyData.GetDouble, pair.TestArgument, pair.DifferentArgument);

            TestContract<double, ArgumentOutOfRangeException>(
                pair.DifferentArgument,
                pair.TestArgument,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetDouble();
            var lower = success - 10;
            var higher = success + 10;
            var outOfRange = higher + 10;

            TestContract<double, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeBetween(lower, higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterThan()
        {
            var success = DummyData.GetDouble();
            var lower = success - 10;
            var outOfRange = lower - 10;

            TestContract<double, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterThan(lower, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterOrEqualThan()
        {
            var success = DummyData.GetDouble();
            var outOfRange = success - 10;

            TestContract<double, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualTo(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetDouble();
            var higher = success + 10;
            var outOfRange = higher + 10;

            TestContract<double, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessThan(higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessOrEqualThan()
        {
            var success = DummyData.GetDouble();
            var outOfRange = success + 10;

            TestContract<double, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualTo(success, message),
                "testArgument");
        }
    }
}