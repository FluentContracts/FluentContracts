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

        var randomChar = Faker.Value.Random.Char('A', 'z');
        
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return option switch
        {
            StringOption.Normal => randomChar,
            StringOption.Uppercase => char.ToUpperInvariant(randomChar),
            StringOption.Lowercase => char.ToLowerInvariant(randomChar),
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

    public static Person GetPerson()
    {
        return new Person();
    }
}