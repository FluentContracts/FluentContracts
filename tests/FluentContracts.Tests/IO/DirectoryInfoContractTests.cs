using System;
using System.IO;
using FluentContracts.Contracts.IO;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.IO;

[ContractTest("DirectoryInfo")]
public class DirectoryInfoContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetDirectoryInfo(this),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentNullException>(
            DummyData.GetDirectoryInfo(this),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Exist()
    {
        var successful = DummyData.GetDirectoryInfo(this);
        var failing = DummyData.GetDirectoryInfo(this, FileInfoOption.NonExisting);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().Exist(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotExist()
    {
        var successful = DummyData.GetDirectoryInfo(this, FileInfoOption.NonExisting);
        var failing = DummyData.GetDirectoryInfo(this);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotExist(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeReadOnly()
    {
        var successful = DummyData.GetDirectoryInfo(this, readOnly: true);
        var failing = DummyData.GetDirectoryInfo(this);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeReadOnly(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeReadOnly()
    {
        var successful = DummyData.GetDirectoryInfo(this);
        var failing = DummyData.GetDirectoryInfo(this, readOnly: true);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeReadOnly(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeHidden()
    {
        var successful = DummyData.GetDirectoryInfo(this, hidden: true);
        var failing = DummyData.GetDirectoryInfo(this);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeHidden(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeHidden()
    {
        var successful = DummyData.GetDirectoryInfo(this);
        var failing = DummyData.GetDirectoryInfo(this, hidden: true);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeHidden(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeEmpty()
    {
        var successful = DummyData.GetDirectoryInfo(this, FileInfoOption.Empty);
        var failing = DummyData.GetDirectoryInfo(this, FileInfoOption.NotEmpty);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        var successful = DummyData.GetDirectoryInfo(this, FileInfoOption.NotEmpty);
        var failing = DummyData.GetDirectoryInfo(this, FileInfoOption.Empty);
        
        TestContract<DirectoryInfo?, DirectoryInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }
}
