using System;
using System.Linq;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    #region String
    
    private const string WhiteSpaceString = "      ";
    private const char WhiteSpaceChar = ' ';

    public static string GetString() => GetString(StringOption.Normal);
    public static string GetString(int length) => GetString(StringOption.Normal, length);

    public static string GetString(StringOption option, int length = 10)
    {
        return option switch
        {
            StringOption.Alphanumeric => Faker.Value.Random.AlphaNumeric(length),
            StringOption.Ascii => Faker.Value.Random.String(length, (char)0, (char)127),
            StringOption.Digits => Faker.Value.Random.Digits(length).ToString(),
            StringOption.Guid => Faker.Value.Random.Guid().ToString(),
            StringOption.IpAddressV4 => Faker.Value.Internet.IpAddress().ToString(),
            StringOption.IpAddressV6 => Faker.Value.Internet.Ipv6Address().ToString(),
            StringOption.Letters => Faker.Value.Random.String(length, 'a', 'z'),
            StringOption.Lowercase => Faker.Value.Random.String(length, 'A', 'z').ToLowerInvariant(),
            StringOption.NonAscii => Faker.Value.Random.String(length, (char)161, (char)661),
            StringOption.Normal => Faker.Value.Random.String(length, 'A', 'z'),
            StringOption.Palindrome => CreatePalindrome(),
            StringOption.SpecialCharacters => Faker.Value.Random.String2(length, "~!@#$%^&*()_-=+/?.>,<]}[{"),
            StringOption.Uppercase => Faker.Value.Random.String(length, 'A', 'z').ToUpperInvariant(),
            StringOption.Url => Faker.Value.Internet.Url(),
            StringOption.WhiteSpace => WhiteSpaceString,
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }
    
    private static string CreatePalindrome()
    {
        var text = Faker.Value.Random.AlphaNumeric(10);
        var reversed = new string(text.Reverse().ToArray());
        return text + reversed;
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

    public static char GetChar(CharOption option = CharOption.Normal)
    {
        if (option == CharOption.WhiteSpace) return WhiteSpaceChar;

        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return option switch
        {
            CharOption.Alphanumeric => Faker.Value.Random.AlphaNumeric(1)[0],
            CharOption.Ascii => Faker.Value.Random.Char((char)0, (char)127),
            CharOption.Digit => Faker.Value.Random.Char('0', '9'),
            CharOption.Letter => Faker.Value.Random.Char('a', 'z'),
            CharOption.Lowercase => Faker.Value.Random.Char('a', 'z'),
            CharOption.NonAscii => Faker.Value.Random.Char((char)161, (char)661),
            CharOption.Normal => Faker.Value.Random.Char('A', 'z'),
            CharOption.SpecialCharacter => Faker.Value.Random.Char((char)33, (char)47),
            CharOption.Uppercase => Faker.Value.Random.Char('A', 'Z'),
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

    #endregion
}