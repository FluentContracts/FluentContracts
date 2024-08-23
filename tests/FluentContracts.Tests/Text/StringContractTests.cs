using System;
using System.Collections.Generic;
using System.IO;
using FluentContracts.Contracts.Text;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Text;

[ContractTest("String")]
public class StringContractTests : Tests, IDisposable
{   
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<string?, StringContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<string?, StringContract, ArgumentNullException>(
            DummyData.GetString(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetStringPair();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetStringPair();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAnyOf()
    {
        var pair = DummyData.GetStringPair();
        var array = DummyData.GetArray(DummyData.GetString, pair.TestArgument, pair.DifferentArgument);

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) =>
                message == null ? testArgument.Must().BeAnyOf(array) : testArgument.Must().BeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAnyOf()
    {
        var pair = DummyData.GetStringPair();
        var array = DummyData.GetArray(DummyData.GetString, pair.TestArgument, pair.DifferentArgument);

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) =>
                message == null
                    ? testArgument.Must().NotBeAnyOf(array)
                    : testArgument.Must().NotBeAnyOf(message, array),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            string.Empty,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            string.Empty,
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNullOrEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            string.Empty,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrEmpty(message),
            "testArgument");

        TestContract<string?, StringContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNullOrEmpty()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            string.Empty,
            (testArgument, message) => testArgument.Must().NotBeNullOrEmpty(message),
            "testArgument");

        TestContract<string?, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNullOrEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeNullOrWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.WhiteSpace),
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrWhiteSpace(message),
            "testArgument");

        TestContract<string?, StringContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeNullOrWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNullOrWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            DummyData.GetString(StringOption.WhiteSpace),
            (testArgument, message) => testArgument.Must().NotBeNullOrWhiteSpace(message),
            "testArgument");

        TestContract<string?, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNullOrWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.WhiteSpace),
            DummyData.GetString(),
            (testArgument, message) => testArgument.Must().BeWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWhiteSpace()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(),
            DummyData.GetString(StringOption.WhiteSpace),
            (testArgument, message) => testArgument.Must().NotBeWhiteSpace(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeUppercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Uppercase),
            DummyData.GetString(StringOption.Lowercase),
            (testArgument, message) => testArgument.Must().BeUppercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeUppercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Lowercase),
            DummyData.GetString(StringOption.Uppercase),
            (testArgument, message) => testArgument.Must().NotBeUppercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeLowercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Lowercase),
            DummyData.GetString(StringOption.Uppercase),
            (testArgument, message) => testArgument.Must().BeLowercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLowercase()
    {
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            DummyData.GetString(StringOption.Uppercase),
            DummyData.GetString(StringOption.Lowercase),
            (testArgument, message) => testArgument.Must().NotBeLowercase(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Contain()
    {
        var pair = DummyData.GetStringPair(PairOption.Containing);
        var notContaining = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            notContaining,
            (testArgument, message) => testArgument.Must().Contain(pair.DifferentArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotContain()
    {
        var pair = DummyData.GetStringPair(PairOption.Containing);
        var notContaining = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            notContaining,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotContain(pair.DifferentArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEmailAddress()
    {
        var success = DummyData.GetEmailAddress();
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeEmailAddress(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeEmailAddress()
    {
        var success = DummyData.GetString();
        var fail = DummyData.GetEmailAddress();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeEmailAddress(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeMatching()
    {
        const string pattern = "^JediMaster\\d{2}$";
        const string success = "JediMaster42";
        const string fail = "SithLord007";
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeMatching(pattern, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeMatching()
    {
        const string pattern = "^JediMaster\\d{2}$";
        const string success = "SithLord007";
        const string fail = "JediMaster42";
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeMatching(pattern, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_StartWith()
    {
        var pair = DummyData.GetStringPair(PairOption.StartWith);
        var notStartingWith = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            notStartingWith,
            (testArgument, message) => testArgument.Must().StartWith(pair.DifferentArgument, message: message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotStartWith()
    {
        var pair = DummyData.GetStringPair(PairOption.StartWith);
        var notStartingWith = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            notStartingWith,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotStartWith(pair.DifferentArgument, message: message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_EndWith()
    {
        var pair = DummyData.GetStringPair(PairOption.EndWith);
        var notStartingWith = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            notStartingWith,
            (testArgument, message) => testArgument.Must().EndWith(pair.DifferentArgument, message: message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotEndWith()
    {
        var pair = DummyData.GetStringPair(PairOption.EndWith);
        var notStartingWith = DummyData.GetString();

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            notStartingWith,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotEndWith(pair.DifferentArgument, message: message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BePalindrome()
    {
        var success = DummyData.GetString(StringOption.Palindrome);
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BePalindrome(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBePalindrome()
    {
        var success = DummyData.GetString();
        var fail = DummyData.GetString(StringOption.Palindrome);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBePalindrome(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeUrl()
    {
        var success = DummyData.GetString(StringOption.Url);
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeUrl(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeUrl()
    {
        var success = DummyData.GetString();
        var fail = DummyData.GetString(StringOption.Url);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeUrl(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveLengthEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetString(targetLength);
        var fail = DummyData.GetString(targetLength + 10);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthEqualTo(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotHaveLengthEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetString(targetLength + 10);
        var fail = DummyData.GetString(targetLength);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotHaveLengthEqualTo(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveLengthGreaterThan()
    {
        const int targetLength = 42;
        var success = DummyData.GetString(targetLength + 10);
        var fail = DummyData.GetString(targetLength - 10);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthGreaterThan(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveLengthGreaterOrEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetString(targetLength);
        var success2 = DummyData.GetString(targetLength + 10);
        var fail = DummyData.GetString(targetLength - 10);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthGreaterOrEqualTo(targetLength, message),
            "testArgument");
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success2,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthGreaterOrEqualTo(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveLengthLessThan()
    {
        const int targetLength = 42;
        var success = DummyData.GetString(targetLength - 10);
        var fail = DummyData.GetString(targetLength + 10);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthLessThan(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveLengthLessOrEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetString(targetLength);
        var success2 = DummyData.GetString(targetLength - 10);
        var fail = DummyData.GetString(targetLength + 10);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthLessOrEqualTo(targetLength, message),
            "testArgument");
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success2,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthLessOrEqualTo(targetLength, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveLengthBetween()
    {
        const int targetLengthLow = 42;
        const int targetLengthHigh = 69;
        
        var success = DummyData.GetString(targetLengthLow + 8);
        var fail = DummyData.GetString(targetLengthHigh + 1);

        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveLengthBetween(targetLengthLow, targetLengthHigh, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeAlphanumeric()
    {
        var success = DummyData.GetString(StringOption.Alphanumeric);
        var fail = DummyData.GetString(StringOption.SpecialCharacters);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeAlphanumeric(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeAlphanumeric()
    {
        var success = DummyData.GetString(StringOption.SpecialCharacters);
        var fail = DummyData.GetString(StringOption.Alphanumeric);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeAlphanumeric(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeIpAddress()
    {
        var success4 = DummyData.GetString(StringOption.IpAddressV4);
        var success6 = DummyData.GetString(StringOption.IpAddressV6);
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success4,
            fail,
            (testArgument, message) => testArgument.Must().BeIpAddress(message),
            "testArgument");
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success6,
            fail,
            (testArgument, message) => testArgument.Must().BeIpAddress(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeIpAddress()
    {
        var success = DummyData.GetString();
        var fail4 = DummyData.GetString(StringOption.IpAddressV4);
        var fail6 = DummyData.GetString(StringOption.IpAddressV6);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail4,
            (testArgument, message) => testArgument.Must().NotBeIpAddress(message),
            "testArgument");
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail6,
            (testArgument, message) => testArgument.Must().NotBeIpAddress(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeGuid()
    {
        var success = DummyData.GetString(StringOption.Guid);
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeGuid(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeGuid()
    {
        var success = DummyData.GetString();
        var fail = DummyData.GetString(StringOption.Guid);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeGuid(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeBase64()
    {
        var success = DummyData.GetString(StringOption.Base64);
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeBase64(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeBase64()
    {
        var success = DummyData.GetString();
        var fail = DummyData.GetString(StringOption.Base64);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeBase64(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeHexadecimal()
    {
        var success = DummyData.GetString(StringOption.Hexadecimal);
        var fail = DummyData.GetString(StringOption.SpecialCharacters);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeHexadecimal(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeHexadecimal()
    {
        var success = DummyData.GetString(StringOption.SpecialCharacters);
        var fail = DummyData.GetString(StringOption.Hexadecimal);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeHexadecimal(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeCreditCardNumber()
    {
        var success = DummyData.GetString(StringOption.CreditCardNumber);
        var fail = DummyData.GetString(length: 16);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeCreditCardNumber(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeCreditCardNumber()
    {
        var success = DummyData.GetString(length: 16);
        var fail = DummyData.GetString(StringOption.CreditCardNumber);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeCreditCardNumber(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeExistingFile()
    {
        var success = DummyData.GetFilePath(this);
        
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeExistingFile(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeExistingFile()
    {
        var success = DummyData.GetString();
        
        var fail = DummyData.GetFilePath(this);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeExistingFile(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeExistingDirectory()
    {
        var success = DummyData.GetDirectoryPath(this);
        
        var fail = DummyData.GetString();
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeExistingDirectory(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeExistingDirectory()
    {
        var success = DummyData.GetString();
        
        var fail = DummyData.GetDirectoryPath(this);
        
        TestContract<string, StringContract, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeExistingDirectory(message),
            "testArgument");
    }
}