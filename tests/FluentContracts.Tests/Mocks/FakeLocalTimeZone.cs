using System;
using System.Reflection;

namespace FluentContracts.Tests.Mocks.Data;

// Code found here: https://stackoverflow.com/questions/44413407/mock-the-country-timezone-you-are-running-unit-test-from
public class FakeLocalTimeZone : IDisposable
{
    public FakeLocalTimeZone(TimeZoneInfo mockTimeZoneInfo)
    {
        var info = typeof(TimeZoneInfo).GetField("s_cachedData", BindingFlags.NonPublic | BindingFlags.Static);
        var cachedData = info?.GetValue(null);
        var field = cachedData?.GetType().GetField("_localTimeZone",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Instance);
        field?.SetValue(cachedData, mockTimeZoneInfo);
    }

    public void Dispose()
    {
        TimeZoneInfo.ClearCachedData();
    }
}