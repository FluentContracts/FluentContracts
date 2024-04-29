using System;
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
    
    public static string GetString(StringOption option = StringOption.Normal)
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
                var differentArgument = testArgument.Substring(2, 5);

                return new Pair<string>(testArgument, differentArgument);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
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

    public static T[] GetArray<T>(Func<T> valueFactory, T includedValue, T excludedValue, int size = ArraySize)
    {
        if (excludedValue.Equals(includedValue))
        {
            throw new InvalidOperationException("Exclusive and inclusive values are the same");
        }
        
        var result = new T[size];
        int mid = size / 2;

        for (var i = 0; i < size; i++)
        {
            var value = i == mid ? includedValue : valueFactory();

            if (value.Equals(excludedValue))
            {
                value = valueFactory();
            }

            result[i] = value;
        }

        return result;
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