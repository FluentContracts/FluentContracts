using System;
using FluentContracts.Contracts.Text;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Text;

[ContractTest("Char")]
public class CharContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<char?, CharContract, ArgumentException>(
            null,
            DummyData.GetChar(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<char?, CharContract, ArgumentNullException>(
            DummyData.GetChar(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetCharPair();

        TestContract<char, CharContract, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be_Nullable()
    {
        var pair = DummyData.GetNullableCharPair();

        TestContract<char?, CharContract, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetCharPair();

        TestContract<char, CharContract, ArgumentException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe_Nullable()
    {
        var pair = DummyData.GetNullableCharPair();

        TestContract<char?, CharContract, ArgumentException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetCharPair();
        var array = DummyData.GetArray(() => DummyData.GetChar(), pair.TestArgument, pair.DifferentArgument);

        TestContract<char, CharContract, ArgumentException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                testArgument.Must().BeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetCharPair();
        var array = DummyData.GetArray(() => DummyData.GetChar(), pair.TestArgument, pair.DifferentArgument);

        TestContract<char, CharContract, ArgumentException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) =>
                testArgument.Must().NotBeAnyOf(array, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeDigit()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Digit),
            DummyData.GetChar(CharOption.Letter),
            (testArgument, message) => testArgument.Must().BeDigit(message),
            "testArgument");
    }


    [Fact]
    public void Test_Must_NotBeDigit()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Letter),
            DummyData.GetChar(CharOption.Digit),
            (testArgument, message) => testArgument.Must().NotBeDigit(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLetter()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Letter),
            DummyData.GetChar(CharOption.Digit),
            (testArgument, message) => testArgument.Must().BeLetter(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLetter()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Digit),
            DummyData.GetChar(CharOption.Letter),
            (testArgument, message) => testArgument.Must().NotBeLetter(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAlphanumeric()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Alphanumeric),
            DummyData.GetChar(CharOption.SpecialCharacter),
            (testArgument, message) => testArgument.Must().BeAlphanumeric(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAlphanumeric()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.SpecialCharacter),
            DummyData.GetChar(CharOption.Alphanumeric),
            (testArgument, message) => testArgument.Must().NotBeAlphanumeric(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWhiteSpace()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.WhiteSpace),
            DummyData.GetChar(),
            (testArgument, message) => testArgument.Must().BeWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWhiteSpace()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(),
            DummyData.GetChar(CharOption.WhiteSpace),
            (testArgument, message) => testArgument.Must().NotBeWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeUppercase()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Uppercase),
            DummyData.GetChar(CharOption.Lowercase),
            (testArgument, message) => testArgument.Must().BeUppercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeUppercase()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Lowercase),
            DummyData.GetChar(CharOption.Uppercase),
            (testArgument, message) => testArgument.Must().NotBeUppercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLowercase()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Lowercase),
            DummyData.GetChar(CharOption.Uppercase),
            (testArgument, message) => testArgument.Must().BeLowercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLowercase()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Uppercase),
            DummyData.GetChar(CharOption.Lowercase),
            (testArgument, message) => testArgument.Must().NotBeLowercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAscii()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.Ascii),
            DummyData.GetChar(CharOption.NonAscii),
            (testArgument, message) => testArgument.Must().BeAscii(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAscii()
    {
        TestContract<char, CharContract, ArgumentException>(
            DummyData.GetChar(CharOption.NonAscii),
            DummyData.GetChar(CharOption.Ascii),
            (testArgument, message) => testArgument.Must().NotBeAscii(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween()
    {
        var success = DummyData.GetChar();
        var lower = (char)(success - 10);
        var higher = (char)(success + 10);
        var outOfRange = (char)(higher + 10);

        TestContract<char, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeBetween_Nullable()
    {
        var success = DummyData.GetNullableChar();
        var lower = (char?)(success - 10);
        var higher = (char?)(success + 10);
        var outOfRange = (char?)(higher + 10);

        TestContract<char?, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeBetween(lower, higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan()
    {
        var success = DummyData.GetChar();
        var lower = (char)(success - 10);
        var outOfRange = (char)(lower - 10);

        TestContract<char, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterThan_Nullable()
    {
        var success = DummyData.GetNullableChar();
        var lower = (char?)(success - 10);
        var outOfRange = (char?)(lower - 10);

        TestContract<char?, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterThan(lower, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan()
    {
        var success = DummyData.GetChar();
        var outOfRange = (char)(success - 10);

        TestContract<char, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeGreaterOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableChar();
        var outOfRange = (char?)(success - 10);

        TestContract<char?, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeGreaterOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan()
    {
        var success = DummyData.GetChar();
        var higher = (char)(success + 10);
        var outOfRange = (char)(higher + 10);

        TestContract<char, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessThan_Nullable()
    {
        var success = DummyData.GetNullableChar();
        var higher = (char?)(success + 10);
        var outOfRange = (char?)(higher + 10);

        TestContract<char?, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessThan(higher, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan()
    {
        var success = DummyData.GetChar();
        var outOfRange = (char)(success + 10);

        TestContract<char, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLessOrEqualThan_Nullable()
    {
        var success = DummyData.GetNullableChar();
        var outOfRange = (char?)(success + 10);

        TestContract<char?, CharContract, ArgumentOutOfRangeException>(
            success,
            outOfRange,
            (testArgument, message) =>
                testArgument.Must().BeLessOrEqualTo(success, message),
            "testArgument");
    }
}