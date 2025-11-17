using System;
using System.Linq;

namespace AdoRadiator.Core.Helpers;

public static class PathHelper
{
    private const char PathSeparator = '\\';

    public static string[] Split(string path)
    {
        return path.Split(PathSeparator);
    }

    public static string[] GetParentPathWithoutRoot(string path)
    {
        var split = Split(path);

        if (split.Length < 3)
            throw new InvalidOperationException($"The path '{path}' doesn't have enough parts to get a parent.");

        return split.Skip(1).SkipLast(1).ToArray();
    }
}
