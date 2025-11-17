using System.IO;

namespace AdoRadiator.Core.Helpers;

public static class MessageFilePaths
{
    public static string FirstDay => Path.Combine("Messages", "first-day.txt");

    public static string LastDay => Path.Combine("Messages", "last-day.txt");

    public static string MidPoint => Path.Combine("Messages", "midpoint.txt");

    public static string Trivia => Path.Combine("Messages", "trivia.txt");

}
