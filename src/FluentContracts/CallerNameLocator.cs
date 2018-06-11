using System;
using System.IO;

namespace FluentContracts
{
    internal static class CallerNameLocator
    {
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