using System.IO;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    public static string GetFilePath()
    {
        return Path.GetTempFileName();
    }
    public static string GetDirectoryPath()
    {
        return Directory.CreateTempSubdirectory().FullName;
    }
}