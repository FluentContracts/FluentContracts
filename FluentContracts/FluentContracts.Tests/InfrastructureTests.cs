using System;
using FluentAssertions;
using FluentContracts.Primitives;
using Xunit;

namespace FluentContracts.Tests
{
	public class InfrastructureTests
    {
	    [Fact]
	    public void Test_Caller_Name_Discovery()
	    {
		    var someFancyGuid = Guid.Empty;

		    Action failed = () => someFancyGuid.Must().NotBeEmpty();
		    failed.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Exception of type 'System.ArgumentOutOfRangeException' was thrown.\r\nParameter name:  someFancyGuid");
	    }
    }
}
