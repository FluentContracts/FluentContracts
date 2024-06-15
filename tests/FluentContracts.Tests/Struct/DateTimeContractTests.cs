using System;
using FluentContracts.Contracts.Struct;
using FluentContracts.Infrastructure;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Struct;

[ContractTest("DateTime")]
public class DateTimeContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetDateTime(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<DateTime?, DateTimeContract, ArgumentNullException>(
            DummyData.GetDateTime(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetDateTimePair();

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetDateTimePair();

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetDateTimePair();
        var array = DummyData.GetArray(() => DummyData.GetDateTime(), pair.TestArgument, pair.DifferentArgument);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetDateTimePair();
        var array = DummyData.GetArray(() => DummyData.GetDateTime(), pair.TestArgument, pair.DifferentArgument);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween()
    {
        var success = DummyData.GetDateTime();
        var lower = success.AddDays(-1);
        var higher = success.AddDays(1);
        var outOfRange = higher.AddDays(1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetDateTime();
        var lower = success.AddDays(-1);
        var outOfRange = lower.AddDays(-1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetDateTime();
        var outOfRange = success.AddDays(-1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetDateTime();
        var higher = success.AddDays(1);
        var outOfRange = higher.AddDays(1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetDateTime();
        var outOfRange = success.AddDays(1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInDaylightSaving()
    {
        using var fakeTimeZone = new FakeLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Europe/Sofia"));

        var success = DummyData.GetDateTime(DateTimeOption.InDaylightSaving);
        var failing = DummyData.GetDateTime(DateTimeOption.NotInDaylightSaving);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failing,
            (testArgument, message) =>
                testArgument.Must().BeInDaylightSaving(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInDaylightSaving()
    {
        using var fakeTimeZone = new FakeLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Europe/Sofia"));

        var success = DummyData.GetDateTime(DateTimeOption.NotInDaylightSaving);
        var failing = DummyData.GetDateTime(DateTimeOption.InDaylightSaving);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failing,
            (testArgument, message) =>
                testArgument.Must().NotBeInDaylightSaving(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLeapYear()
    {
        var success = DummyData.GetDateTime(DateTimeOption.LeapYear);
        var fail = DummyData.GetDateTime(DateTimeOption.NotLeapYear);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeLeapYear(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLeapYear()
    {
        var success = DummyData.GetDateTime(DateTimeOption.NotLeapYear);
        var fail = DummyData.GetDateTime(DateTimeOption.LeapYear);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeLeapYear(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInMonth()
    {
        var months = new Func<DateTime, string?, Linker<DateTimeContract>>[]
        {
            (testArgument, message) => testArgument.Must().BeInJanuary(message),
            (testArgument, message) => testArgument.Must().BeInFebruary(message),
            (testArgument, message) => testArgument.Must().BeInMarch(message),
            (testArgument, message) => testArgument.Must().BeInApril(message),
            (testArgument, message) => testArgument.Must().BeInMay(message),
            (testArgument, message) => testArgument.Must().BeInJune(message),
            (testArgument, message) => testArgument.Must().BeInJuly(message),
            (testArgument, message) => testArgument.Must().BeInAugust(message),
            (testArgument, message) => testArgument.Must().BeInSeptember(message),
            (testArgument, message) => testArgument.Must().BeInOctober(message),
            (testArgument, message) => testArgument.Must().BeInNovember(message),
            (testArgument, message) => testArgument.Must().BeInDecember(message),
        };

        for (var i = 0; i < months.Length; i++)
        {
            var month = i + 1;
            var differentMonth = month == 12 ? month - 1 : month + 1;

            var success = DummyData.GetDateTime(DateTimeOption.SpecificMonth, month);
            var fail = DummyData.GetDateTime(DateTimeOption.SpecificMonth, differentMonth);

            TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
                success,
                fail,
                months[i],
                "testArgument");
        }
    }

    [Fact]
    public void Test_Must_NotBeInMonth()
    {
        var months = new Func<DateTime, string?, Linker<DateTimeContract>>[]
        {
            (testArgument, message) => testArgument.Must().NotBeInJanuary(message),
            (testArgument, message) => testArgument.Must().NotBeInFebruary(message),
            (testArgument, message) => testArgument.Must().NotBeInMarch(message),
            (testArgument, message) => testArgument.Must().NotBeInApril(message),
            (testArgument, message) => testArgument.Must().NotBeInMay(message),
            (testArgument, message) => testArgument.Must().NotBeInJune(message),
            (testArgument, message) => testArgument.Must().NotBeInJuly(message),
            (testArgument, message) => testArgument.Must().NotBeInAugust(message),
            (testArgument, message) => testArgument.Must().NotBeInSeptember(message),
            (testArgument, message) => testArgument.Must().NotBeInOctober(message),
            (testArgument, message) => testArgument.Must().NotBeInNovember(message),
            (testArgument, message) => testArgument.Must().NotBeInDecember(message),
        };

        for (var i = 0; i < months.Length; i++)
        {
            var month = i + 1;
            var differentMonth = month == 12 ? month - 1 : month + 1;

            var success = DummyData.GetDateTime(DateTimeOption.SpecificMonth, differentMonth);
            var fail = DummyData.GetDateTime(DateTimeOption.SpecificMonth, month);

            TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
                success,
                fail,
                months[i],
                "testArgument");
        }
    }

    [Fact]
    public void Test_Must_BeWeekday()
    {
        var days = new (DayOfWeek Day, Func<DateTime, string?, Linker<DateTimeContract>> Contract)[]
        {
            (DayOfWeek.Monday, (testArgument, message) => testArgument.Must().BeMonday(message)),
            (DayOfWeek.Tuesday, (testArgument, message) => testArgument.Must().BeTuesday(message)),
            (DayOfWeek.Wednesday, (testArgument, message) => testArgument.Must().BeWednesday(message)),
            (DayOfWeek.Thursday, (testArgument, message) => testArgument.Must().BeThursday(message)),
            (DayOfWeek.Friday, (testArgument, message) => testArgument.Must().BeFriday(message)),
            (DayOfWeek.Saturday, (testArgument, message) => testArgument.Must().BeSaturday(message)),
            (DayOfWeek.Sunday, (testArgument, message) => testArgument.Must().BeSunday(message)),
        };

        for (var i = 0; i < days.Length; i++)
        {
            var day = days[i].Day;
            var differentDay = day == DayOfWeek.Monday ? day - 1 : day + 1;

            var success = DummyData.GetDateTime(DateTimeOption.SpecificWeekday, specificWeekday: day);
            var fail = DummyData.GetDateTime(DateTimeOption.SpecificWeekday, specificWeekday: differentDay);

            TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
                success,
                fail,
                days[i].Contract,
                "testArgument");
        }
    }

    [Fact]
    public void Test_Must_NotBeWeekday()
    {
        var days = new (DayOfWeek Day, Func<DateTime, string?, Linker<DateTimeContract>> Contract)[]
        {
            (DayOfWeek.Monday, (testArgument, message) => testArgument.Must().NotBeMonday(message)),
            (DayOfWeek.Tuesday, (testArgument, message) => testArgument.Must().NotBeTuesday(message)),
            (DayOfWeek.Wednesday, (testArgument, message) => testArgument.Must().NotBeWednesday(message)),
            (DayOfWeek.Thursday, (testArgument, message) => testArgument.Must().NotBeThursday(message)),
            (DayOfWeek.Friday, (testArgument, message) => testArgument.Must().NotBeFriday(message)),
            (DayOfWeek.Saturday, (testArgument, message) => testArgument.Must().NotBeSaturday(message)),
            (DayOfWeek.Sunday, (testArgument, message) => testArgument.Must().NotBeSunday(message)),
        };

        for (var i = 0; i < days.Length; i++)
        {
            var day = days[i].Day;
            var differentDay = day == DayOfWeek.Monday ? day - 1 : day + 1;

            var success = DummyData.GetDateTime(DateTimeOption.SpecificWeekday, specificWeekday: differentDay);
            var fail = DummyData.GetDateTime(DateTimeOption.SpecificWeekday, specificWeekday: day);

            TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
                success,
                fail,
                days[i].Contract,
                "testArgument");
        }
    }

    [Fact]
    public void Test_Must_BeUtc()
    {
        var success = DummyData.GetDateTime();
        var fail = DummyData.GetDateTime(DateTimeOption.Local);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeUtc(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeUtc()
    {
        var success = DummyData.GetDateTime(DateTimeOption.Local);
        var fail = DummyData.GetDateTime();

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeUtc(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLocal()
    {
        var success = DummyData.GetDateTime(DateTimeOption.Local);
        var fail = DummyData.GetDateTime();

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeLocal(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLocal()
    {
        var success = DummyData.GetDateTime();
        var fail = DummyData.GetDateTime(DateTimeOption.Local);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeLocal(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeOnDate()
    {
        var day = DummyData.GetIntPair(1, 28);
        var month = DummyData.GetIntPair(1, 12);
        var year = DummyData.GetIntPair(1900, 2024);

        var success = new DateTime(year.TestArgument, month.TestArgument, day.TestArgument);
        
        var failYear = new DateTime(year.DifferentArgument, month.TestArgument, day.TestArgument);
        var failMonth = new DateTime(year.TestArgument, month.DifferentArgument, day.TestArgument);
        var failDay = new DateTime(year.TestArgument, month.TestArgument, day.DifferentArgument);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failYear,
            (testArgument, message) =>
                testArgument.Must().BeOnDate(year.TestArgument, month.TestArgument, day.TestArgument, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failMonth,
            (testArgument, message) =>
                testArgument.Must().BeOnDate(year.TestArgument, month.TestArgument, day.TestArgument, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failDay,
            (testArgument, message) =>
                testArgument.Must().BeOnDate(year.TestArgument, month.TestArgument, day.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeOnDate()
    {
        var day = DummyData.GetIntPair(1, 28);
        var month = DummyData.GetIntPair(1, 12);
        var year = DummyData.GetIntPair(1900, 2024);

        var success = new DateTime(year.DifferentArgument, month.DifferentArgument, day.DifferentArgument);
        
        var failYear = new DateTime(year.TestArgument, month.DifferentArgument, day.DifferentArgument);
        var failMonth = new DateTime(year.DifferentArgument, month.TestArgument, day.DifferentArgument);
        var failDay = new DateTime(year.DifferentArgument, month.DifferentArgument, day.TestArgument);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failYear,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDate(year.TestArgument, month.TestArgument, day.TestArgument, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failMonth,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDate(year.TestArgument, month.TestArgument, day.TestArgument, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failDay,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDate(year.TestArgument, month.TestArgument, day.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeOnDate_DateTimeParameter()
    {
        var day = DummyData.GetIntPair(1, 28);
        var month = DummyData.GetIntPair(1, 12);
        var year = DummyData.GetIntPair(1900, 2024);

        var date = new DateTime(year.TestArgument, month.TestArgument, day.TestArgument);

        var success = new DateTime(year.TestArgument, month.TestArgument, day.TestArgument);
        
        var failYear = new DateTime(year.DifferentArgument, month.TestArgument, day.TestArgument);
        var failMonth = new DateTime(year.TestArgument, month.DifferentArgument, day.TestArgument);
        var failDay = new DateTime(year.TestArgument, month.TestArgument, day.DifferentArgument);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failYear,
            (testArgument, message) =>
                testArgument.Must().BeOnDate(date, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failMonth,
            (testArgument, message) =>
                testArgument.Must().BeOnDate(date, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failDay,
            (testArgument, message) =>
                testArgument.Must().BeOnDate(date, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeOnDate_DateTimeParameter()
    {
        var day = DummyData.GetIntPair(1, 28);
        var month = DummyData.GetIntPair(1, 12);
        var year = DummyData.GetIntPair(1900, 2024);

        var date = new DateTime(year.TestArgument, month.TestArgument, day.TestArgument);

        var success = new DateTime(year.DifferentArgument, month.DifferentArgument, day.DifferentArgument);
        
        var failYear = new DateTime(year.TestArgument, month.DifferentArgument, day.DifferentArgument);
        var failMonth = new DateTime(year.DifferentArgument, month.TestArgument, day.DifferentArgument);
        var failDay = new DateTime(year.DifferentArgument, month.DifferentArgument, day.TestArgument);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failYear,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDate(date, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failMonth,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDate(date, message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            failDay,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDate(date, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInThePast()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(-7);
        var fail = date.AddDays(7);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeInThePast(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInThePast()
    {
        var date = DummyData.GetDateTime();
        
        var success = date.AddDays(7);
        var fail = date.AddDays(-7);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeInThePast(message),
            "testArgument");
    }
}