using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Float")]
    public class IntContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<int?, ArgumentOutOfRangeException>(
                null,
                DummyData.GetRandomInt(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<int?, ArgumentNullException>(
                DummyData.GetRandomInt(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var sameArgument = DummyData.GetRandomInt();
            var otherArgument = DummyData.GetRandomInt();
            
            TestContract<int, ArgumentOutOfRangeException>(
                sameArgument,
                otherArgument,
                (testArgument, message) => testArgument.Must().Be(sameArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var sameArgument = DummyData.GetRandomInt();
            var otherArgument = DummyData.GetRandomInt();
            
            TestContract<int, ArgumentOutOfRangeException>(
                otherArgument,
                sameArgument,
                (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var included = DummyData.GetRandomInt();
            var excluded = DummyData.GetRandomInt();
            var array = DummyData.GetArray(DummyData.GetRandomInt, included, excluded);
            
            TestContract<int, ArgumentOutOfRangeException>(
                included,
                excluded,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var included = DummyData.GetRandomInt();
            var excluded = DummyData.GetRandomInt();
            var array = DummyData.GetArray(DummyData.GetRandomInt, included, excluded);

            TestContract<int, ArgumentOutOfRangeException>(
                excluded,
                included,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetRandomInt();
            var lower = success - 10;
            var higher = success + 10;
            var outOfRange = higher + 10;

            TestContract<int, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeBetween(lower, higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterThan()
        {
            var success = DummyData.GetRandomInt();
            var lower = success - 10;
            var outOfRange = lower - 10;

            TestContract<int, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterThan(lower, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeGreaterOrEqualThan()
        {
            var success = DummyData.GetRandomInt();
            var outOfRange = success - 10;

            TestContract<int, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualThan(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetRandomInt();
            var higher = success + 10;
            var outOfRange = higher + 10;

            TestContract<int, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessThan(higher, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessOrEqualThan()
        {
            var success = DummyData.GetRandomInt();
            var outOfRange = success + 10;

            TestContract<int, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualThan(success, message),
                "testArgument");
        }
    }
}