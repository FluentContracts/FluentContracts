using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    public static FileInfo GetFileInfo(
        Tests test,
        FileInfoOption options = FileInfoOption.Existing,
        string fileExtension = "txt",
        bool readOnly = false,
        bool hidden = false,
        long fileSize = 1024)
    {
        switch (options)
        {
            case FileInfoOption.Existing:
            case FileInfoOption.Empty:
            {
                return new FileInfo(GetFilePath(test, fileExtension, readOnly, hidden));
            }
            case FileInfoOption.NonExisting:
            {
                var directory = Faker.Value.System.DirectoryPath();
                var missingFilePath = Path.Combine(directory, Faker.Value.System.FileName(fileExtension));
                return new FileInfo(missingFilePath);
            }
            case FileInfoOption.NotEmpty:
            {
                var filePath = GetFilePath(test, fileExtension, false, hidden);
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.SetLength(fileSize);
                }
                
                if (readOnly)
                {
                    File.SetAttributes(filePath, FileAttributes.ReadOnly);
                }
                
                return new FileInfo(filePath);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(options), options, null);
        }
    }
    
    public static DirectoryInfo GetDirectoryInfo(
        Tests test,
        FileInfoOption options = FileInfoOption.Existing,
        bool readOnly = false,
        bool hidden = false)
    {
        switch (options)
        {
            case FileInfoOption.Existing:
            case FileInfoOption.Empty:
            {
                var directoryInfo = new DirectoryInfo(GetDirectoryPath(test, hidden));

                SetDirectoryAttributes(directoryInfo, readOnly, hidden);

                return directoryInfo;
            }
            case FileInfoOption.NonExisting:
            {
                var randomDirectory = Faker.Value.System.DirectoryPath();
                var nonExistingDirectory = Path.Combine(randomDirectory, Faker.Value.Random.Word());
                return new DirectoryInfo(nonExistingDirectory);
            }
            case FileInfoOption.NotEmpty:
            {
                var directoryPath = GetDirectoryPath(test, hidden);
                var filePath = Path.Combine(directoryPath, Faker.Value.System.FileName());
                var contents = Faker.Value.Lorem.Paragraphs();
                File.WriteAllText(filePath, contents);
                
                var directoryInfo = new DirectoryInfo(directoryPath);
                SetDirectoryAttributes(directoryInfo, readOnly, hidden);
                return directoryInfo;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(options), options, null);
        }
    }

    public static string GetFileExtension() => Faker.Value.System.FileExt();
    
    public static string GetFilePath(
        Tests test,
        string extension = "txt",
        bool readOnly = false,
        bool hidden = false)
    {
        var directory = GetDirectoryPath(test);
        var fileName = Faker.Value.System.FileName(extension);

        if (hidden && IsLinux())
        {
            fileName = $".{fileName}";
        }
        
        var filePath = Path.Combine(directory, fileName);
        
        File.Create(filePath).Close();

        if (readOnly)
        {
            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.ReadOnly);
        }

        if (hidden)
        {
            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
        }
        
        return filePath;
    }
    
    public static string GetDirectoryPath(Tests test, bool hidden = false)
    {
        var directoryPath = Directory.CreateTempSubdirectory(hidden && IsLinux() ? "." : "").FullName;
        test.RegisterDirectory(directoryPath);
        return directoryPath;
    }
    
    private static bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    private static void SetDirectoryAttributes(
        DirectoryInfo directoryInfo,
        bool isReadOnly = false,
        bool isHidden = false)
    {
        if (isReadOnly)
        {
            directoryInfo.Attributes |= FileAttributes.ReadOnly;
        }
                
        if (isHidden)
        {
            directoryInfo.Attributes |= FileAttributes.Hidden;
        }
    }
}