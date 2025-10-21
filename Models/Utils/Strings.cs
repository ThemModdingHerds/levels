using System.Globalization;

namespace ThemModdingHerds.Models.Utils;
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
    
    public static string ParseRest(string line,string start)
    {
        return line[start.Length..].Trim();
    }
    public static string[] ParseParts(string line)
    {
        return line.Split(' ').Where((s) => s != string.Empty).ToArray();
    }
    public static string[] ParseParts(string line,string start)
    {
        return ParseParts(ParseRest(line,start));
    }
    public static string[] ParseParts(string line,string start,int count)
    {
        string[] parts = ParseParts(line,start);
        if(parts.Length != count)
            throw new Exception($"found {parts.Length} parts, expected {count}");
        return parts;
    }
}