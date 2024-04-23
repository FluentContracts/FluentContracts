using System;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.TestAttributes;
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
            var included = DummyData.GetRandomGuid();
            var excluded = DummyData.GetRandomGuid();
            var array = DummyData.GetArray(DummyData.GetRandomGuid, included, excluded);
            
            TestContract<Guid, ArgumentOutOfRangeException>(
                included,
                excluded,
                (testArgument, message) => 
                    message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
                "testArgument");
        }
        
        [Fact]
        public void Test_Must_NotBeAnyOf()
        {
            var included = DummyData.GetRandomGuid();
            var excluded = DummyData.GetRandomGuid();
            var array = DummyData.GetArray(DummyData.GetRandomGuid, included, excluded);

            TestContract<Guid, ArgumentOutOfRangeException>(
                excluded,
                included,
                (testArgument, message) => 
                    message == null ? testArgument.Must().NotBeAnyOf(array) : testArgument.Must().NotBeAnyOf(message, array),
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