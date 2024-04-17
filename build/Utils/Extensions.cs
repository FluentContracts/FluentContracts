using System;
using System.IO;
using System.Linq;
using Nuke.Common.IO;
using Nuke.Common.Tooling;

namespace Utils;

public static class Extensions
{
    public static Tool CreateDownloadableTool(this AbsolutePath toolDirectory, string executableFileName, string downloadUrl)
    {
        var directory = CreateToolDirectory(toolDirectory, executableFileName);
        if (directory == null)
            return null;
			
        var toolPath = TryGetToolAbsolutePath(executableFileName, directory);
        if (toolPath != null)
            return ToolResolver.GetPathTool(toolPath);

        var downloadFileName = Path.GetFileName(downloadUrl);
        if (string.IsNullOrEmpty(downloadFileName))
            return null;

        var downloadAbsolutePath = directory / downloadFileName;
			
        HttpTasks.HttpDownloadFile(
            downloadUrl,
            downloadAbsolutePath);

        if (downloadFileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        {
            downloadAbsolutePath.UnZipTo(directory);
        }
			
        toolPath = TryGetToolAbsolutePath(executableFileName, directory);
        return toolPath != null ? ToolResolver.GetPathTool(toolPath) : null;
    }
    
    private static AbsolutePath CreateToolDirectory(AbsolutePath toolDirectory, string executableFileName)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(executableFileName);
        if (string.IsNullOrEmpty(fileNameWithoutExtension)) 
            return null;

        var directory = toolDirectory / fileNameWithoutExtension;

        toolDirectory.CreateDirectory();
        return toolDirectory;
    }
    
    private static AbsolutePath TryGetToolAbsolutePath(string executableFileName, AbsolutePath directory)
    {
        var foundFiles = 
            directory.GlobFiles($"**/{executableFileName}");

        return foundFiles.Count < 1 ? null : foundFiles.First();
    }
}