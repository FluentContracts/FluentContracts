using System;
using System.IO;
using FluentContracts.Contracts.IO;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.IO;

[ContractTest("FileInfo")]
public class FileInfoContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetFileInfo(this),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<FileInfo?, FileInfoContract, ArgumentNullException>(
            DummyData.GetFileInfo(this),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Exist()
    {
        var successful = DummyData.GetFileInfo(this);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.NonExisting);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().Exist(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotExist()
    {
        var successful = DummyData.GetFileInfo(this, FileInfoOption.NonExisting);
        var failing = DummyData.GetFileInfo(this);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotExist(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveExtension()
    {
        var extension = DummyData.GetFileExtension();
        var differentExtension = DummyData.GetFileExtension();
        
        var successful = DummyData.GetFileInfo(this, fileExtension: extension);
        var failing = DummyData.GetFileInfo(this, fileExtension: differentExtension);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().HaveExtension(extension, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHaveExtension()
    {
        var extension = DummyData.GetFileExtension();
        var differentExtension = DummyData.GetFileExtension();
        
        var successful = DummyData.GetFileInfo(this, fileExtension: extension);
        var failing = DummyData.GetFileInfo(this, fileExtension: differentExtension);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotHaveExtension(differentExtension, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeReadOnly()
    {
        var successful = DummyData.GetFileInfo(this, readOnly: true);
        var failing = DummyData.GetFileInfo(this);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeReadOnly(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeReadOnly()
    {
        var successful = DummyData.GetFileInfo(this);
        var failing = DummyData.GetFileInfo(this, readOnly: true);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeReadOnly(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeHidden()
    {
        var successful = DummyData.GetFileInfo(this, hidden: true);
        var failing = DummyData.GetFileInfo(this);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeHidden(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeHidden()
    {
        var successful = DummyData.GetFileInfo(this);
        var failing = DummyData.GetFileInfo(this, hidden: true);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeHidden(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeEmpty()
    {
        var successful = DummyData.GetFileInfo(this, FileInfoOption.Empty);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().BeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeEmpty()
    {
        var successful = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.Empty);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().NotBeEmpty(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveSizeEqualTo()
    {
        var fileSize = DummyData.GetLong(minValue: 1024L, maxValue: 10240L);
        var successful = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize * 2);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().HaveSizeEqualTo(fileSize, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveSizeLessThan()
    {
        var fileSize = DummyData.GetLong(minValue: 1024L, maxValue: 10240L);
        var successful = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize - 512L);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize * 2);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().HaveSizeLessThan(fileSize, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveSizeLessOrEqualTo()
    {
        var fileSize = DummyData.GetLong(minValue: 1024L, maxValue: 10240L);
        var successful = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize * 2);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().HaveSizeLessOrEqualTo(fileSize, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveSizeGreaterOrEqualTo()
    {
        var fileSize = DummyData.GetLong(minValue: 1024L, maxValue: 10240L);
        var successful = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize - 512L);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().HaveSizeGreaterOrEqualTo(fileSize, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveSizeGreaterThan()
    {
        var fileSize = DummyData.GetLong(minValue: 1024L, maxValue: 10240L);
        var successful = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize * 2);
        var failing = DummyData.GetFileInfo(this, FileInfoOption.NotEmpty, fileSize: fileSize - 512L);
        
        TestContract<FileInfo?, FileInfoContract, ArgumentOutOfRangeException>(
            successful,
            failing,
            (testArgument, message) => testArgument.Must().HaveSizeGreaterThan(fileSize, message),
            "testArgument");
    }
}
