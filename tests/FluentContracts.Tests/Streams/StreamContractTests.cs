using System;
using System.IO;
using FluentContracts.Contracts.Streams;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Streams;

[ContractTest("Stream")]
public class StreamContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            null,
            new MockStream(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<Stream, StreamContract<Stream>, ArgumentNullException>(
            new MockStream(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeWriteable()
    {
        var success = new MockStream(canWrite: true);
        var fail = new MockStream(canWrite: false);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeWriteable(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWriteable()
    {
        var success = new MockStream(canWrite: false);
        var fail = new MockStream(canWrite: true);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeWriteable(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeReadable()
    {
        var success = new MockStream(canRead: true);
        var fail = new MockStream(canRead: false);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeReadable(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeReadable()
    {
        var success = new MockStream(canRead: false);
        var fail = new MockStream(canRead: true);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeReadable(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeSeekable()
    {
        var success = new MockStream(canSeek: true);
        var fail = new MockStream(canSeek: false);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeSeekable(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeSeekable()
    {
        var success = new MockStream(canSeek: false);
        var fail = new MockStream(canSeek: true);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeSeekable(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeAbleToTimeout()
    {
        var success = new MockStream(canTimeout: true);
        var fail = new MockStream(canTimeout: false);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeAbleToTimeout(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAbleToTimeout()
    {
        var success = new MockStream(canTimeout: false);
        var fail = new MockStream(canTimeout: true);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeAbleToTimeout(message),
            "testArgument");
    }
    
    [Fact]
    public void Test_Must_BeAtPosition()
    {
        var position = DummyData.GetLongPair();
        
        var success = new MockStream(position: position.TestArgument);
        var fail = new MockStream(position: position.DifferentArgument);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeAtPosition(position.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAtPosition()
    {
        var position = DummyData.GetLongPair();
        
        var success = new MockStream(position: position.DifferentArgument);
        var fail = new MockStream(position: position.TestArgument);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeAtPosition(position.TestArgument, message),
            "testArgument");
    }
    
    
    
    [Fact]
    public void Test_Must_BeWithLength()
    {
        var length = DummyData.GetLongPair();
        
        var success = new MockStream(length: length.TestArgument);
        var fail = new MockStream(length: length.DifferentArgument);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().BeWithLength(length.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeWithLenght()
    {
        var length = DummyData.GetLongPair();
        
        var success = new MockStream(length: length.DifferentArgument);
        var fail = new MockStream(length: length.TestArgument);
        
        TestContract<Stream, StreamContract<Stream>, ArgumentOutOfRangeException>(
            success,
            fail,
            (testArgument, message) => testArgument.Must().NotBeWithLength(length.TestArgument, message),
            "testArgument");
    }
}