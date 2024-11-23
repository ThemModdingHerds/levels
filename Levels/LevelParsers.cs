using System.Globalization;
using System.Numerics;

namespace ThemModdingHerds.Levels;
public static class LevelParsers
{
    public static Vector2 ParseStageSize(string line)
    {
        int[] size = ParseSameMultiple<int>(line,"StageSize:",2);
        return new(size[0],size[1]);
    }
    public static string ParseRest(string line,string start)
    {
        return line[start.Length..].Trim();
    }
    public static string[] ParseParts(string line,string start,int count)
    {
        string[] parts = ParseRest(line,start).Split(' ');
        if(parts.Length != count)
            throw new Exception($"found {parts.Length} parts, expected {count}");
        return parts;
    }
    public static T[] ParseSameMultiple<T>(string line,string start,int count) where T : IParsable<T>
    {
        return (from x in ParseParts(line,start,count) select T.Parse(x,null)).ToArray();
    }
    public static float[] ParseMultipleFloats(string line,string start,int count)
    {
        return (from x in ParseParts(line,start,count) select float.Parse(x,NumberStyles.Any,CultureInfo.InvariantCulture)).ToArray();
    }
}