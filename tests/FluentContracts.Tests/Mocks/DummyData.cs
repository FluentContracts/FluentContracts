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
    
    public static int GetRandomInt()
    {
        return Faker.Value.Random.Int(-1_000_000, 1_000_000);
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
    
    public static double GetRandomDouble()
    {
        return Faker.Value.Random.Double(-1_000_000, 1_000_000);
    }
    
    public static long GetRandomLong()
    {
        return Faker.Value.Random.Long(-1_000_000, 1_000_000);
    }
    
    public static float GetRandomFloat()
    {
        return Faker.Value.Random.Float(-1_000_000, 1_000_000);
    }
    
    public static short GetRandomShort()
    {
        return Faker.Value.Random.Short(-10_000, 10_000);
    }
    
    public static byte GetRandomByte()
    {
        return Faker.Value.Random.Byte(50, 100);
    }

    public static Person GetRandomPerson()
    {
        return new Person();
    }
}