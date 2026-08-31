using System;
using FluentAssertions;
using Xunit;

namespace FluentContracts.Tests.Mocks.Data;

/// <summary>
/// The generators feed every contract test, so a generator that can throw makes the whole suite
/// flaky. <see cref="DateTimeOption.SpecificDay"/> used to pick a month at random and then ask for
/// the requested day inside it, which throws for a 29th, 30th or 31st in a month that never reaches
/// one — a 31st failed roughly two times in five.
/// </summary>
public class DummyDataTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(28)]
    [InlineData(29)]
    [InlineData(30)]
    [InlineData(31)]
    public void GetDateTime_produces_a_valid_date_on_the_requested_day(int day)
    {
        for (var attempt = 0; attempt < 500; attempt++)
        {
            var date = DummyData.GetDateTime(DateTimeOption.SpecificDay, specificDay: day);

            date.Day.Should().Be(day, "the generator was asked for day {0}", day);
        }
    }
}
