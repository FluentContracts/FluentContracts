using System;
using FluentContracts.Tests.TestAttributes;
using FluentContracts.Tests.Utils;
using Xunit;

namespace FluentContracts.Tests
{
    [ContractTest("Guid")]
    public class GuidContractTests : Tests
    {
        [Fact]
        public void Test_Must_BeNull()
        {
            TestContract<Guid?, ArgumentOutOfRangeException>(
                null,
                DummyData.GetRandomGuid(),
                (testArgument, message) => testArgument.Must().BeNull(message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeNull()
        {
            TestContract<Guid?, ArgumentNullException>(
                DummyData.GetRandomGuid(),
                null,
                (testArgument, message) => testArgument.Must().NotBeNull(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_Be()
        {
            var sameArgument = DummyData.GetRandomGuid();
            var otherArgument = DummyData.GetRandomGuid();
            
            TestContract<Guid, ArgumentOutOfRangeException>(
                sameArgument,
                otherArgument,
                (testArgument, message) => testArgument.Must().Be(sameArgument, message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBe()
        {
            var sameArgument = DummyData.GetRandomGuid();
            var otherArgument = DummyData.GetRandomGuid();
            
            TestContract<Guid, ArgumentOutOfRangeException>(
                otherArgument,
                sameArgument,
                (testArgument, message) => testArgument.Must().NotBe(sameArgument, message),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeAnyOf()
        {
            var argument1 = DummyData.GetRandomGuid();
            var argument2 = DummyData.GetRandomGuid();
            var argument3 = DummyData.GetRandomGuid();
            var argument4 = DummyData.GetRandomGuid();
            
            TestContract<Guid, ArgumentOutOfRangeException>(
                argument3,
                argument1,
                (testArgument, message) => 
                    testArgument.Must().BeAnyOf(message, argument2, argument3, argument4),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var argument1 = DummyData.GetRandomGuid();
            var argument2 = DummyData.GetRandomGuid();
            var argument3 = DummyData.GetRandomGuid();
            var argument4 = DummyData.GetRandomGuid();

            TestContract<Guid, ArgumentOutOfRangeException>(
                argument1,
                argument3,
                (testArgument, message) => 
                    testArgument.Must().NotBeAnyOf(message, argument2, argument3, argument4),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_BeEmpty()
        {
            TestContract<Guid, ArgumentOutOfRangeException>(
                Guid.Empty,
                DummyData.GetRandomGuid(),
                (testArgument, message) => testArgument.Must().BeEmpty(message),
                "testArgument");
        }

        [Fact]
        public void Test_Must_NotBeEmpty()
        {
            TestContract<Guid, ArgumentOutOfRangeException>(
                DummyData.GetRandomGuid(),
                Guid.Empty,
                (testArgument, message) => testArgument.Must().NotBeEmpty(message),
                "testArgument");
        }
    }
}