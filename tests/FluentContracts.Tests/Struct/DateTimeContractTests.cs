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
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableDateTimePair();

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableDateTimePair();

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_BeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableDateTimePair();
        var array = DummyData.GetArray(() => DummyData.GetNullableDateTime(), pair.TestArgument, pair.DifferentArgument);

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_NotBeAnyOf_Nullable()
    {
        var pair = DummyData.GetNullableDateTimePair();
        var array = DummyData.GetArray(() => DummyData.GetNullableDateTime(), pair.TestArgument, pair.DifferentArgument);

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableDateTime();

        if (!success.HasValue)
        {
            throw new InvalidOperationException("DateTime from dummy cannot be null");
        }
        
        DateTime? lower = success.Value.AddDays(-1);
        DateTime? higher = success.Value.AddDays(1);
        DateTime? outOfRange = higher.Value.AddDays(1);

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableDateTime();

        if (!success.HasValue)
        {
            throw new InvalidOperationException("DateTime from dummy cannot be null");
        }
        
        DateTime? lower = success.Value.AddDays(-1);
        DateTime? outOfRange = lower.Value.AddDays(-1);

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableDateTime();

        if (!success.HasValue)
        {
            throw new InvalidOperationException("DateTime from dummy cannot be null");
        }
        
        DateTime? outOfRange = success.Value.AddDays(-1);

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableDateTime();
        
        if (!success.HasValue)
        {
            throw new InvalidOperationException("DateTime from dummy cannot be null");
        }
        
        DateTime? higher = success.Value.AddDays(1);
        DateTime? outOfRange = higher.Value.AddDays(1);

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableDateTime();

        if (!success.HasValue)
        {
            throw new InvalidOperationException("DateTime from dummy cannot be null");
        }
        
        var outOfRange = success.Value.AddDays(1);

        TestContract<DateTime?, DateTimeContract, ArgumentOutOfRangeException>(
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
    public void Test_Must_BeInMonth_By_Name()
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
    public void Test_Must_NotBeInMonth_By_Name()
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
    public void Test_Must_BeWeekday_By_Name()
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
    public void Test_Must_NotBeWeekday_By_Name()
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
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeInThePast(date, message),
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
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeInThePast(message),
            "testArgument");

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeInThePast(date, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInTheFuture()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(7);
        var fail = date.AddDays(-7);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeInTheFuture(message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeInTheFuture(date, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInTheFuture()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(-7);
        var fail = date.AddDays(7);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeInTheFuture(message),
            "testArgument");
        
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeInTheFuture(date, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeToday()
    {
        var date = DummyData.GetDateTime();
            
        var success = new DateTime(date.Year, date.Month, date.Day);
        var fail = date.AddDays(7);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeToday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeToday()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(7);
        var fail = new DateTime(date.Year, date.Month, date.Day);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeToday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeTomorrow()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(1);
        var fail = date.AddDays(-1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeTomorrow(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeTomorrow()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(-1);
        var fail = date.AddDays(1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeTomorrow(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeYesterday()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(-1);
        var fail = date.AddDays(1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeYesterday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeYesterday()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(1);
        var fail = date.AddDays(-1);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeYesterday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInMonth()
    {
        var date = DummyData.GetDateTime();
            
        var success = DummyData.GetDateTime(DateTimeOption.SpecificMonth, specificMonth: date.Month);
        var fail = date.AddMonths(1);
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeInMonth(date.Month, message),
            "testArgument");
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeInCurrentMonth(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInMonth()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddMonths(1);
        var fail = DummyData.GetDateTime(DateTimeOption.SpecificMonth, specificMonth: date.Month);
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeInMonth(date.Month, message),
            "testArgument");
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeInCurrentMonth(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeInYear()
    {
        var date = DummyData.GetDateTime();
            
        var success = DummyData.GetDateTime(DateTimeOption.SpecificYear, specificYear: date.Year);
        var fail = date.AddYears(1);
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeInYear(date.Year, message),
            "testArgument");
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeInCurrentYear(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeInYear()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddYears(1);
        var fail = DummyData.GetDateTime(DateTimeOption.SpecificYear, specificYear: date.Year);
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeInYear(date.Year, message),
            "testArgument");
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeInCurrentYear(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeOnDay()
    {
        var date = DummyData.GetDateTime();
            
        var success = DummyData.GetDateTime(DateTimeOption.SpecificDay, specificDay: date.Day);
        var fail = date.AddDays(1);
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeOnDay(date.Day, message),
            "testArgument");
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).BeOnCurrentDay(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeOnDay()
    {
        var date = DummyData.GetDateTime();
            
        var success = date.AddDays(1);
        var fail = DummyData.GetDateTime(DateTimeOption.SpecificDay, specificDay: date.Day);
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDay(date.Day, message),
            "testArgument");
    
        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must(dateTimeProvider: new MockDateTimeProvider(date)).NotBeOnCurrentDay(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWeekend()
    {
        var success = DummyData.GetDateTime(DateTimeOption.Weekend);
        var fail = DummyData.GetDateTime(DateTimeOption.Weekday);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeWeekend(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWeekend()
    {
        var success = DummyData.GetDateTime(DateTimeOption.Weekday);
        var fail = DummyData.GetDateTime(DateTimeOption.Weekend);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeWeekend(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWeekday()
    {
        var success = DummyData.GetDateTime(DateTimeOption.Weekday);
        var fail = DummyData.GetDateTime(DateTimeOption.Weekend);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeWeekday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWeekday()
    {
        var success = DummyData.GetDateTime(DateTimeOption.Weekend);
        var fail = DummyData.GetDateTime(DateTimeOption.Weekday);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeWeekday(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeOnDayOfYear()
    {
        var success = DummyData.GetDateTime();
        var fail = success.AddDays(5);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().BeOnDayOfYear(success.DayOfYear, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeOnDayOfYear()
    {
        var success = DummyData.GetDateTime();
        var fail = success.AddDays(5);

        TestContract<DateTime, DateTimeContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) =>
                testArgument.Must().NotBeOnDayOfYear(fail.DayOfYear, message),
            "testArgument");
    }
}