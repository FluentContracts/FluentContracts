using System;
using FluentContracts.Infrastructure;

namespace FluentContracts.Tests.Mocks;

public class MockDateTimeProvider(DateTime now) : IDateTimeProvider
{
    public DateTime Now => now;
    public DateTime Today => now;
}