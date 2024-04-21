using System;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
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
                DummyData.GetRandomShort(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<short?, ArgumentNullException>(
                DummyData.GetRandomShort(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var sameArgument = DummyData.GetRandomShort();
            var otherArgument = DummyData.GetRandomShort();
            
            TestContract<short, ArgumentOutOfRangeException>(
                sameArgument,
                otherArgument,
                (testArgument, message) => testArgument.Must().Be(sameArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var sameArgument = DummyData.GetRandomShort();
            var otherArgument = DummyData.GetRandomShort();
            
            TestContract<short, ArgumentOutOfRangeException>(
                otherArgument,
                sameArgument,
                (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var included = DummyData.GetRandomShort();
            var excluded = DummyData.GetRandomShort();
            var array = DummyData.GetArray(DummyData.GetRandomShort, included, excluded);
            
            TestContract<short, ArgumentOutOfRangeException>(
                included,
                excluded,
                (testArgument, message) => 
                    testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var included = DummyData.GetRandomShort();
            var excluded = DummyData.GetRandomShort();
            var array = DummyData.GetArray(DummyData.GetRandomShort, included, excluded);

            TestContract<short, ArgumentOutOfRangeException>(
                excluded,
                included,
                (testArgument, message) => 
                    testArgument.Must().NotBeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeBetween()
        {
            var success = DummyData.GetRandomShort();
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
            var success = DummyData.GetRandomShort();
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
            var success = DummyData.GetRandomShort();
            var outOfRange = (short)(success - 10);

            TestContract<short, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeGreaterOrEqualThan(success, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeLessThan()
        {
            var success = DummyData.GetRandomShort();
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
            var success = DummyData.GetRandomShort();
            var outOfRange = (short)(success + 10);

            TestContract<short, ArgumentOutOfRangeException>(
                success,
                outOfRange,
                (testArgument, message) => 
                    testArgument.Must().BeLessOrEqualThan(success, message),
                "testArgument");
        }
    }
}