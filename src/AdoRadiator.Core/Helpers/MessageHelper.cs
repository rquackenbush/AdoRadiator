using System;
using System.IO;

namespace AdoRadiator.Core.Helpers;

public static class MessageHelper
{
    public static string GetRandomMessage(string path)
    {
        if (!File.Exists(path))
            return $"Swell. I can't find the file at '{path}'.";

        var lines = File.ReadAllLines(path);

        if (lines.Length == 0)
            return $"Much like my soul, the file '{path}' is empty.";

        var random = new Random();

        var lineNumber = random.Next(0, lines.Length - 1);

        return lines[lineNumber];
    }
}
