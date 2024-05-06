#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace FluentContracts.Tests.Mocks;

public static class DummyData
{
    private const int ArraySize = 10;
    private const string WhiteSpaceString = "      ";
    private const char WhiteSpaceChar = ' ';
    
    private static Lazy<Faker> Faker { get; } = new(() => new Faker { Random = new Randomizer(42)});

    public static string GetRandomMessage()
    {
        return Faker.Value.Lorem.Sentence(10);
    }
    
    public static (string Message, string ExceptionMessage) GetRandomErrorMessage(string parameterName)
    {
        
        var message = GetRandomMessage();
        var exceptionMessage = $"{message} (Parameter '{parameterName}')";
        return (message, exceptionMessage);
    }

    public static string GetEmailAddress()
    {
        return Faker.Value.Internet.Email();
    }
    
    public static Guid GetGuid()
    {
        return Faker.Value.Random.Guid();
    }

    public static char GetChar(StringOption option = StringOption.Normal)
    {
        if (option == StringOption.WhiteSpace) return WhiteSpaceChar;
        
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return option switch
        {
            StringOption.Normal => Faker.Value.Random.Char('A', 'z'),
            StringOption.Uppercase => Faker.Value.Random.Char('A', 'Z'),
            StringOption.Lowercase => Faker.Value.Random.Char('a', 'z'),
            StringOption.Ascii => Faker.Value.Random.Char((char)0, (char)127),
            StringOption.NonAscii => Faker.Value.Random.Char((char)161, (char)661),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }
    
    public static Pair<char> GetCharPair()
    {
        const char middle = (char)(char.MaxValue / 2);
        const char nextToMiddle = (char)(middle + 1);
        
        var testArgument = Faker.Value.Random.Char(char.MinValue, middle);
        var differentArgument = Faker.Value.Random.Char(nextToMiddle, char.MaxValue);

        return new Pair<char>(testArgument, differentArgument);
    }
    
    public static char GetDigit()
    {
        return Faker.Value.Random.Char('0', '9');
    }
    
    public static char GetLetter()
    {
        return Faker.Value.Random.Char('a', 'z');
    }

    public static string GetString() => GetString(StringOption.Normal);
    
    public static string GetString(StringOption option)
    {
        if (option == StringOption.WhiteSpace) return WhiteSpaceString;

        var randomString = Faker.Value.Random.String(5, 10, 'A', 'z');
        
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return option switch
        {
            StringOption.Normal => randomString,
            StringOption.Uppercase => randomString.ToUpperInvariant(),
            StringOption.Lowercase => randomString.ToLowerInvariant(),
            StringOption.Ascii => Faker.Value.Random.String(5, 10, (char)0, (char)127),
            StringOption.NonAscii => Faker.Value.Random.String(5, 10, (char)161, (char)661),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }
    
    public static Pair<string> GetStringPair(PairOption option = PairOption.Different)
    {
        switch (option)
        {
            case PairOption.Different:
            {
                var testArgument = Faker.Value.Random.String(5, 10, '0', 'z');
                var differentArgument = Faker.Value.Random.String(11, 21, '0', 'z');

                return new Pair<string>(testArgument, differentArgument);
            }
            case PairOption.Containing:
            {
                var testArgument = Faker.Value.Random.String(10, 30, '0', 'z');
                var differentArgument = testArgument[2..5];

                return new Pair<string>(testArgument, differentArgument);
            }
            case PairOption.StartWith:
            {
                var testArgument = Faker.Value.Random.String(10, 30, '0', 'z');
                var differentArgument = testArgument[..5];

                return new Pair<string>(testArgument, differentArgument);
            }
            case PairOption.EndWith:
            {
                var testArgument = Faker.Value.Random.String(10, 30, '0', 'z');
                var differentArgument = testArgument[5..];

                return new Pair<string>(testArgument, differentArgument);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }

    public static T GetEnumValue<T>(T? exclude = null)
        where T : struct, Enum
    {
        return exclude != null ? Faker.Value.Random.Enum<T>(exclude.Value) : Faker.Value.Random.Enum<T>();
    }
    
    public static int GetInt()
    {
        return Faker.Value.Random.Int(-1_000_000, 1_000_000);
    }
    
    public static Pair<int> GetIntPair()
    {
        const int middle = int.MaxValue / 2;
        const int nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Int(int.MinValue, middle);
        var differentArgument = Faker.Value.Random.Int(nextToMiddle, int.MaxValue);

        return new Pair<int>(testArgument, differentArgument);
    }
    
    public static uint GetUint()
    {
        return Faker.Value.Random.UInt(500_000U, 1_000_000U);
    }
    
    public static Pair<uint> GetUintPair()
    {
        const uint middle = uint.MaxValue / 2;
        const uint nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.UInt(uint.MinValue, middle);
        var differentArgument = Faker.Value.Random.UInt(nextToMiddle, uint.MaxValue);

        return new Pair<uint>(testArgument, differentArgument);
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
    
    public static decimal GetDecimal()
    {
        return Faker.Value.Random.Decimal(-1_000_000, 1_000_000);
    } 
    
    public static Pair<decimal> GetDecimalPair()
    {
        const decimal middle = 0m / 2;
        const decimal nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Decimal(0m, middle);
        var differentArgument = Faker.Value.Random.Decimal(nextToMiddle, 1m);

        return new Pair<decimal>(testArgument, differentArgument);
    }
    
    public static double GetDouble()
    {
        return Faker.Value.Random.Double(-1_000_000, 1_000_000);
    }
    
    public static Pair<double> GetDoublePair()
    {
        const double middle = double.MaxValue / 2;
        const double nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Double(double.MinValue, middle);
        var differentArgument = Faker.Value.Random.Double(nextToMiddle, double.MaxValue);

        return new Pair<double>(testArgument, differentArgument);
    }
    
    public static long GetLong()
    {
        return Faker.Value.Random.Long(-1_000_000, 1_000_000);
    }
    
    public static Pair<long> GetLongPair()
    {
        const long middle = long.MaxValue / 2;
        const long nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Long(long.MinValue, middle);
        var differentArgument = Faker.Value.Random.Long(nextToMiddle, long.MaxValue);

        return new Pair<long>(testArgument, differentArgument);
    }
    
    public static ulong GetUlong()
    {
        return Faker.Value.Random.ULong(1_000_000UL, 10_000_000UL);
    }
    
    public static Pair<ulong> GetUlongPair()
    {
        const ulong middle = ulong.MaxValue / 2;
        const ulong nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.ULong(ulong.MinValue, middle);
        var differentArgument = Faker.Value.Random.ULong(nextToMiddle, ulong.MaxValue);

        return new Pair<ulong>(testArgument, differentArgument);
    }
    
    public static float GetFloat()
    {
        return Faker.Value.Random.Float(-1_000_000, 1_000_000);
    }
    
    public static Pair<float> GetFloatPair()
    {
        const float middle = float.MaxValue / 2;
        const float nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Float(float.MinValue, middle);
        var differentArgument = Faker.Value.Random.Float(nextToMiddle, float.MaxValue);

        return new Pair<float>(testArgument, differentArgument);
    }
    
    public static short GetShort()
    {
        return Faker.Value.Random.Short(-10_000, 10_000);
    }
    
    public static Pair<short> GetShortPair()
    {
        const short middle = short.MaxValue / 2;
        const short nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Short(short.MinValue, middle);
        var differentArgument = Faker.Value.Random.Short(nextToMiddle, short.MaxValue);

        return new Pair<short>(testArgument, differentArgument);
    }
    
    public static ushort GetUshort()
    {
        return Faker.Value.Random.UShort(1_000, 40_000);
    }
    
    public static Pair<ushort> GetUshortPair()
    {
        const ushort middle = ushort.MaxValue / 2;
        const ushort nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.UShort(ushort.MinValue, middle);
        var differentArgument = Faker.Value.Random.UShort(nextToMiddle, ushort.MaxValue);

        return new Pair<ushort>(testArgument, differentArgument);
    }
    
    public static byte GetByte()
    {
        return Faker.Value.Random.Byte(50, 100);
    }
    
    public static Pair<byte> GetBytePair()
    {
        const byte middle = byte.MaxValue / 2;
        const byte nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Byte(byte.MinValue, middle);
        var differentArgument = Faker.Value.Random.Byte(nextToMiddle, byte.MaxValue);

        return new Pair<byte>(testArgument, differentArgument);
    }
    
    public static sbyte GetSbyte()
    {
        return Faker.Value.Random.SByte(-80, 80);
    }
    
    public static Pair<sbyte> GetSbytePair()
    {
        const sbyte middle = sbyte.MaxValue / 2;
        const sbyte nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.SByte(sbyte.MinValue, middle);
        var differentArgument = Faker.Value.Random.SByte(nextToMiddle, sbyte.MaxValue);

        return new Pair<sbyte>(testArgument, differentArgument);
    }
    
    public static DateTime GetDateTime(DateTimeOption option = DateTimeOption.Utc, int specificMonth = 1, DayOfWeek specificWeekday = DayOfWeek.Wednesday)
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
}