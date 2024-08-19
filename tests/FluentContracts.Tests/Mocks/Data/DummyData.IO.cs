using System;
using System.IO;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    public static FileInfo GetFileInfo(
        Tests test,
        FileInfoOption options = FileInfoOption.Existing,
        string fileExtension = "txt",
        bool readOnly = false,
        bool hidden = false)
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
                var directory = GetDirectoryPath(test);
                var missingFilePath = Path.Combine(directory, Faker.Value.System.FileName(fileExtension));
                return new FileInfo(missingFilePath);
            }
            case FileInfoOption.NotEmpty:
            {
                var filePath = GetFilePath(test, fileExtension, false, hidden);
                var contents = Faker.Value.Lorem.Paragraphs();
                File.WriteAllText(filePath, contents);
                
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

    public static string GetFileExtension() => Faker.Value.System.FileExt();
    
    public static string GetFilePath(
        Tests test,
        string extension = "txt",
        bool readOnly = false,
        bool hidden = false)
    {
        var directory = GetDirectoryPath(test);
        var filePath = Path.Combine(directory, Faker.Value.System.FileName(extension));
        File.Create(filePath).Close();

        if (readOnly)
        {
            File.SetAttributes(filePath, FileAttributes.ReadOnly);
        }

        if (hidden)
        {
            File.SetAttributes(filePath, FileAttributes.Hidden);
        }
        
        return filePath;
    }
    
    public static string GetDirectoryPath(Tests test)
    {
        var directoryPath = Directory.CreateTempSubdirectory().FullName;
        test.RegisterDirectory(directoryPath);
        return directoryPath;
    }
}