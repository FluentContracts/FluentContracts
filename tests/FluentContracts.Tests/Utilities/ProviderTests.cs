using System;
using FluentAssertions;
using FluentContracts.Infrastructure;
using Xunit;

namespace FluentContracts.Tests.Utilities;

public class ProviderTests
{
    [Fact]
    public void DotNetDateTimeProvider_Must_Return_Now()
    {
        var provider = new DotNetDateTimeProvider();

        var providerNow = provider.Now;

        providerNow.Should().BeWithin(TimeSpan.FromSeconds(10));
    }
    
    
    [Fact]
    public void DotNetDateTimeProvider_Must_Return_Today()
    {
        var provider = new DotNetDateTimeProvider();

        var providerToday = provider.Today;

        providerToday.Should().BeWithin(TimeSpan.FromSeconds(10));
    }
}