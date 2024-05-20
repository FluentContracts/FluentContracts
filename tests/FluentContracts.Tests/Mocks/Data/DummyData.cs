#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    private const int ArraySize = 10;
    
    private static Lazy<Faker> Faker { get; } = 
        new(() => new Faker { Random = new Randomizer(42)});

    public static Guid GetGuid()
    {
        return Faker.Value.Random.Guid();
    }

    public static T GetEnumValue<T>(T? exclude = null)
        where T : struct, Enum
    {
        return exclude != null ? Faker.Value.Random.Enum<T>(exclude.Value) : Faker.Value.Random.Enum<T>();
    }

    public static T[] GetArray<T>(
        Func<T> valueFactory, 
        T? includedValue = default, 
        T? excludedValue = default, 
        int size = ArraySize)
    {
        if (excludedValue != null 
            && includedValue != null
            && excludedValue.Equals(includedValue))
        {
            throw new InvalidOperationException("Exclusive and inclusive values are the same");
        }
        
        var result = new T[size];
        int mid = size / 2;

        includedValue ??= valueFactory();

        for (var i = 0; i < size; i++)
        {
            var value = 
                i == mid 
                    ? includedValue 
                    : GetNotMatchingValue(valueFactory, excludedValue);

            result[i] = value;
        }

        return result;
    }

    private const int MaxFallbackCounter = 100;
    private static T GetNotMatchingValue<T>(
        Func<T> valueFactory,
        T? excludedValue)
    {
        if (excludedValue == null)
            return valueFactory();
        
        var fallbackCounter = 0;
        var differentValue = valueFactory();
        
        while (differentValue == null 
               || differentValue.Equals(excludedValue))
        {
            fallbackCounter++;
            differentValue = valueFactory();
            
            if (fallbackCounter == MaxFallbackCounter)
                break;
        }

        return differentValue;
    }
    
    public static Pair<T[]> GetArrayPair<T>(Func<T> valueFactory)
    {
        var testArgument = GetArray(valueFactory);
        var differentArgument = GetArray(valueFactory); 
        return new Pair<T[]>(testArgument, differentArgument);
    }

    public static List<T> GetList<T>(
        Func<T> valueFactory,
        T? includedValue = default,
        T? excludedValue = default,
        int size = ArraySize) => 
        GetArray(valueFactory, includedValue, excludedValue, size).ToList();
    
    public static Pair<List<T>> GetListPair<T>(Func<T> valueFactory)
    {
        var testArgument = GetList(valueFactory);
        var differentArgument = GetList(valueFactory); 
        return new Pair<List<T>>(testArgument, differentArgument);
    }
    
    public static DateTime GetDateTime(
        DateTimeOption option = DateTimeOption.Utc, 
        int specificMonth = 1, 
        DayOfWeek specificWeekday = DayOfWeek.Wednesday)
    {
        switch (option)
        {
            case DateTimeOption.Utc:
            {
                var date = Faker.Value.Date.BetweenDateOnly(DateOnly.MinValue.AddYears(1), DateOnly.MaxValue.AddYears(-1));
                var time = Faker.Value.Date.BetweenTimeOnly(TimeOnly.MinValue, TimeOnly.MaxValue);

                return new DateTime(date, time, DateTimeKind.Utc);
            }
            case DateTimeOption.Local:
            {
                var date = Faker.Value.Date.BetweenDateOnly(DateOnly.MinValue.AddYears(1), DateOnly.MaxValue.AddYears(-1));
                var time = Faker.Value.Date.BetweenTimeOnly(TimeOnly.MinValue, TimeOnly.MaxValue);

                return new DateTime(date, time, DateTimeKind.Local);
            }
            case DateTimeOption.InDaylightSaving:
            {
                return new DateTime(2024, 5, 5);
            }
            case DateTimeOption.NotInDaylightSaving:
            {
                return new DateTime(2024, 2, 6);
            }
            case DateTimeOption.LeapYear:
            {
                var month = Faker.Value.Random.Int(1, 12);
                var day = Faker.Value.Random.Int(1, 29);
                
                return new DateTime(2024, month, day);
            }
            case DateTimeOption.NotLeapYear:
            {
                var month = Faker.Value.Random.Int(1, 12);
                var day = Faker.Value.Random.Int(1, 28);
                
                return new DateTime(2023, month, day);
            }
            case DateTimeOption.SpecificMonth:
            {
                var year = Faker.Value.Random.Int(1800, 2500);
                var day = Faker.Value.Random.Int(1, 28);
                
                return new DateTime(year, specificMonth, day);
            }
            case DateTimeOption.SpecificWeekday:
            {
                var year = Faker.Value.Random.Int(1800, 2500);
                var day = Faker.Value.Random.Int(1, 28);
                var date = new DateTime(year, specificMonth, day);

                if (date.DayOfWeek == specificWeekday) return date;
                
                int daysUntilDesiredDay = ((int)specificWeekday - (int)date.DayOfWeek + 7) % 7;

                return date.AddDays(daysUntilDesiredDay);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }
    
    public static Pair<DateTime> GetDateTimePair(DateTimeOption option = DateTimeOption.Utc)
    {
        var kind = option == DateTimeOption.Utc ? DateTimeKind.Utc : DateTimeKind.Local;
        var now = DateTime.SpecifyKind(DateTime.Now, kind);
        var nextToNow = now.AddDays(1);
        
        var testArgument = 
            DateTime.SpecifyKind(Faker.Value.Date.Between(DateTime.MinValue, now), kind);
        var differentArgument = 
            DateTime.SpecifyKind(Faker.Value.Date.Between(nextToNow, DateTime.MaxValue), kind);

        return new Pair<DateTime>(testArgument, differentArgument);
    }

    public static Person GetPerson()
    {
        return new Person();
    }

    public static Employee GetEmployee()
    {
        var role = Faker.Value.Random.Enum<Role>();
        return new Employee(role);
    }
}