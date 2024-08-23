using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using FluentContracts.Contracts.Collections;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Collections;

[ContractTest("Dictionary")]
public class DictionaryContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<Dictionary<string, string>?, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            null,
            DummyData.GetDictionary(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<Dictionary<string, string>?, DictionaryContract<string, string>, ArgumentNullException>(
            DummyData.GetDictionary(DummyData.GetString),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetDictionaryPair(DummyData.GetString);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetDictionaryPair(DummyData.GetString);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            pair.DifferentArgument,
            pair.TestArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.TestArgument, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeEmpty()
    {
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            [],
            DummyData.GetDictionary(DummyData.GetString),
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            DummyData.GetDictionary(DummyData.GetString),
            [],
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountEqualTo()
    {
        const int size = 10;
        var success = DummyData.GetDictionary(DummyData.GetString, size: size);
        var fail = DummyData.GetDictionary(DummyData.GetString, size: size + 10);
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountEqualTo(size, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotHaveCountEqualTo()
    {
        const int size = 10;
        var success = DummyData.GetDictionary(DummyData.GetString, size: size + 10);
        var fail = DummyData.GetDictionary(DummyData.GetString, size: size);
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotHaveCountEqualTo(size, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountGreaterThan()
    {
        const int targetLength = 42;
        var success = DummyData.GetDictionary(DummyData.GetString, size: targetLength + 10);
        var fail = DummyData.GetDictionary(DummyData.GetString, size: targetLength - 10);
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountGreaterThan(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountGreaterOrEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetDictionary(DummyData.GetString, size: targetLength);
        var success2 = DummyData.GetDictionary(DummyData.GetString, size: targetLength + 10);
        var fail = DummyData.GetDictionary(DummyData.GetString, size: targetLength - 10);
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountGreaterOrEqualTo(targetLength, message),
            "testArgument");
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success2,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountGreaterOrEqualTo(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountLessThan()
    {
        const int targetLength = 42;
        var success = DummyData.GetDictionary(DummyData.GetString, size: targetLength - 10);
        var fail = DummyData.GetDictionary(DummyData.GetString, size: targetLength + 10);
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountLessThan(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountLessOrEqualTo()
    {
        const int targetLength = 42;
        var success = DummyData.GetDictionary(DummyData.GetString, size: targetLength);
        var success2 = DummyData.GetDictionary(DummyData.GetString, size: targetLength - 10);
        var fail = DummyData.GetDictionary(DummyData.GetString, size: targetLength + 10);
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountLessOrEqualTo(targetLength, message),
            "testArgument");
        
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success2,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountLessOrEqualTo(targetLength, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_HaveCountBetween()
    {
        const int targetLengthLow = 42;
        const int targetLengthHigh = 69;
        
        var success = DummyData.GetDictionary(DummyData.GetString, size: targetLengthLow + 8);
        var fail = DummyData.GetDictionary(DummyData.GetString, size: targetLengthHigh + 1);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().HaveCountBetween(targetLengthLow, targetLengthHigh, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_ContainKey()
    {
        var pair = DummyData.GetKeyValuePair(DummyData.GetString);
        var success = DummyData.GetDictionary(DummyData.GetString, includedPair: pair);
        var fail = DummyData.GetDictionary(DummyData.GetString, excludedPair: pair);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().ContainKey(pair.Key, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotContainKey()
    {
        var pair = DummyData.GetKeyValuePair(DummyData.GetString);
        var success = DummyData.GetDictionary(DummyData.GetString, excludedPair: pair);
        var fail = DummyData.GetDictionary(DummyData.GetString, includedPair: pair);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotContainKey(pair.Key, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_ContainValue()
    {
        var pair = DummyData.GetKeyValuePair(DummyData.GetString);
        var success = DummyData.GetDictionary(DummyData.GetString, includedPair: pair);
        var fail = DummyData.GetDictionary(DummyData.GetString, excludedPair: pair);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().ContainValue(pair.Value, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotContainValue()
    {
        var pair = DummyData.GetKeyValuePair(DummyData.GetString);
        var success = DummyData.GetDictionary(DummyData.GetString, excludedPair: pair);
        var fail = DummyData.GetDictionary(DummyData.GetString, includedPair: pair);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotContainValue(pair.Value, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_ContainKeyValuePair()
    {
        var pair = DummyData.GetKeyValuePair(DummyData.GetString);
        var success = DummyData.GetDictionary(DummyData.GetString, includedPair: pair);
        var fail = DummyData.GetDictionary(DummyData.GetString, excludedPair: pair);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().ContainKeyValuePair(pair, message),
            "testArgument");
        
        
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().ContainKeyValuePair(pair.Key, pair.Value, message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_NotContainKeyValuePair()
    {
        var pair = DummyData.GetKeyValuePair(DummyData.GetString);
        var success = DummyData.GetDictionary(DummyData.GetString, excludedPair: pair);
        var fail = DummyData.GetDictionary(DummyData.GetString, includedPair: pair);
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotContainKeyValuePair(pair, message),
            "testArgument");
        
        
    
        TestContract<Dictionary<string, string>, DictionaryContract<string, string>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotContainKeyValuePair(pair.Key, pair.Value, message),
            "testArgument");
    }
}