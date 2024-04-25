using System;
using Bogus;

namespace FluentContracts.Tests.Mocks;

public static class DummyData
{
    private const int ArraySize = 10;
    
    private static Lazy<Faker> Faker { get; } = new(() => new Faker() { Random = new Randomizer(42)});

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
    
    public static Guid GetRandomGuid()
    {
        return Faker.Value.Random.Guid();
    }

    public static char GetRandomChar()
    {
        return Faker.Value.Random.Char();
    }
    
    public static Pair<char> GetRandomCharPair()
    {
        const char middle = (char)(char.MaxValue / 2);
        const char nextToMiddle = (char)(middle + 1);
        
        var testArgument = Faker.Value.Random.Char(char.MinValue, middle);
        var differentArgument = Faker.Value.Random.Char(nextToMiddle, char.MaxValue);

        return new Pair<char>(testArgument, differentArgument);
    }
    
    public static char GetRandomDigit()
    {
        return Faker.Value.Random.Char('0', '9');
    }
    
    public static char GetRandomLetter()
    {
        return Faker.Value.Random.Char('a', 'z');
    }

    public static string GetRandomString()
    {
        return Faker.Value.Random.String(5, 10);
    }
    
    public static Pair<string> GetRandomStringPair()
    {   
        var testArgument = Faker.Value.Random.String(5, 10);
        var differentArgument = Faker.Value.Random.String(11, 21);

        return new Pair<string>(testArgument, differentArgument);
    }
    
    public static int GetRandomInt()
    {
        return Faker.Value.Random.Int(-1_000_000, 1_000_000);
    }
    
    public static Pair<int> GetRandomIntPair()
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
    
    public static decimal GetRandomDecimal()
    {
        return Faker.Value.Random.Decimal(-1_000_000, 1_000_000);
    } 
    
    public static Pair<decimal> GetRandomDecimalPair()
    {
        const decimal middle = 0m / 2;
        const decimal nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Decimal(0m, middle);
        var differentArgument = Faker.Value.Random.Decimal(nextToMiddle, 1m);

        return new Pair<decimal>(testArgument, differentArgument);
    }
    
    public static double GetRandomDouble()
    {
        return Faker.Value.Random.Double(-1_000_000, 1_000_000);
    }
    
    public static Pair<double> GetRandomDoublePair()
    {
        const double middle = double.MaxValue / 2;
        const double nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Double(double.MinValue, middle);
        var differentArgument = Faker.Value.Random.Double(nextToMiddle, double.MaxValue);

        return new Pair<double>(testArgument, differentArgument);
    }
    
    public static long GetRandomLong()
    {
        return Faker.Value.Random.Long(-1_000_000, 1_000_000);
    }
    
    public static Pair<long> GetRandomLongPair()
    {
        const long middle = long.MaxValue / 2;
        const long nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Long(long.MinValue, middle);
        var differentArgument = Faker.Value.Random.Long(nextToMiddle, long.MaxValue);

        return new Pair<long>(testArgument, differentArgument);
    }
    
    public static float GetRandomFloat()
    {
        return Faker.Value.Random.Float(-1_000_000, 1_000_000);
    }
    
    public static Pair<float> GetRandomFloatPair()
    {
        const float middle = float.MaxValue / 2;
        const float nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Float(float.MinValue, middle);
        var differentArgument = Faker.Value.Random.Float(nextToMiddle, float.MaxValue);

        return new Pair<float>(testArgument, differentArgument);
    }
    
    public static short GetRandomShort()
    {
        return Faker.Value.Random.Short(-10_000, 10_000);
    }
    
    public static Pair<short> GetRandomShortPair()
    {
        const short middle = short.MaxValue / 2;
        const short nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Short(short.MinValue, middle);
        var differentArgument = Faker.Value.Random.Short(nextToMiddle, short.MaxValue);

        return new Pair<short>(testArgument, differentArgument);
    }
    
    public static byte GetRandomByte()
    {
        return Faker.Value.Random.Byte(50, 100);
    }
    
    public static Pair<byte> GetRandomBytePair()
    {
        const byte middle = byte.MaxValue / 2;
        const byte nextToMiddle = middle + 1;
        
        var testArgument = Faker.Value.Random.Byte(byte.MinValue, middle);
        var differentArgument = Faker.Value.Random.Byte(nextToMiddle, byte.MaxValue);

        return new Pair<byte>(testArgument, differentArgument);
    }

    public static Person GetRandomPerson()
    {
        return new Person();
    }
}