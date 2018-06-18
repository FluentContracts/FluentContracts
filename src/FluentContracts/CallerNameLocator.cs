using System;
using System.IO;

namespace FluentContracts
{
    internal static class CallerNameLocator
    {
        /// <summary>
        /// Getting the name of the caller using the source code. This won't work when code is released.
        /// Using that until MS implement CallerExpressionArgument attribute in C# 8. Then we replace it.
        /// </summary>
        /// <param name="filePath">Path to the *.cs file where the method is called</param>
        /// <param name="lineNumber">Line number in the source file</param>
        /// <param name="defaultFallbackName">Default fallback name of the argument, when not found</param>
        /// <returns>Name of the caller argument</returns>
        public static string GetNameOrDefault(string filePath, int lineNumber, string defaultFallbackName)
        {
            if (!File.Exists(filePath))
                return defaultFallbackName;

            var fileLines = File.ReadLines(filePath);
            var lineIndex = 1;
            var foundLine = string.Empty;

            foreach (var line in fileLines)
            {
                if (lineIndex == lineNumber)
                {
                    foundLine = line;
                    break;
                }

                lineIndex++;
            }

            if (string.IsNullOrWhiteSpace(foundLine))
                return defaultFallbackName;

            var mustLocation = foundLine.IndexOf("Must()", StringComparison.Ordinal);

            if (mustLocation == -1)
                return defaultFallbackName;

            var startPosition = 0;
            for (var i = mustLocation - 2; i >= 0; i--)
            {
                if (!char.IsLetter(foundLine[i]))
                {
                    startPosition = i;
                    break;
                }
            }

            var name = foundLine.Substring(startPosition,
                foundLine.Length - startPosition - (foundLine.Length - mustLocation + 1));

            return name;
        }
    }
}