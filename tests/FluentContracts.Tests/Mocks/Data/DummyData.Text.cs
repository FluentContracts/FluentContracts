using System;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    #region String
    
    private const string WhiteSpaceString = "      ";
    private const char WhiteSpaceChar = ' ';

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

    #endregion

    #region Char

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

    #endregion
}