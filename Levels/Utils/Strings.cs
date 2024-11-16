using System.Globalization;

namespace ThemModdingHerds.Levels.Utils;
public static class Strings
{
    public const char COMMENT = '#';
    public static string RemoveComments(string content)
    {
        IEnumerable<string> norlines = from line in content.Split('\n') select line.Replace("\r",string.Empty).Trim();
        IEnumerable<string> lines = norlines.Where((line) => line != "\n");
        IEnumerable<string> removed = from line in lines select line.Contains(COMMENT) ? line[..line.IndexOf(COMMENT)] : line;
        return string.Join('\n',removed.Where((line) => line.Length > 0));
    }
    public static string FloatStringWithZeros(float value)
    {
        return value.ToString("0.000000",CultureInfo.InvariantCulture);
    }
    public static string FloatString(float value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }
}