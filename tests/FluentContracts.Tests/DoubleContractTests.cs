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
                DummyData.GetRandomDouble(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<double?, ArgumentNullException>(
                DummyData.GetRandomDouble(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var sameArgument = DummyData.GetRandomDouble();
            var otherArgument = DummyData.GetRandomDouble();
            
            TestContract<double, ArgumentOutOfRangeException>(
                sameArgument,
                otherArgument,
                (testArgument, message) => testArgument.Must().Be(sameArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var sameArgument = DummyData.GetRandomDouble();
            var otherArgument = DummyData.GetRandomDouble();
            
            TestContract<double, ArgumentOutOfRangeException>(
                otherArgument,
                sameArgument,
                (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var included = DummyData.GetRandomDouble();
            var excluded = DummyData.GetRandomDouble();
            var array = DummyData.GetArray(DummyData.GetRandomDouble, included, excluded);
            
            TestContract<double, ArgumentOutOfRangeException>(
                included,
                excluded,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var included = DummyData.GetRandomDouble();
            var excluded = DummyData.GetRandomDouble();
            var array = DummyData.GetArray(DummyData.GetRandomDouble, included, excluded);

            TestContract<double, ArgumentOutOfRangeException>(
                excluded,
                included,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetRandomDouble();
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
            var success = DummyData.GetRandomDouble();
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
            var success = DummyData.GetRandomDouble();
            var outOfRange = success - 10;

            TestContract<double, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualThan(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetRandomDouble();
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
            var success = DummyData.GetRandomDouble();
            var outOfRange = success + 10;

            TestContract<double, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualThan(success, message),
                "testArgument");
        }
    }
}