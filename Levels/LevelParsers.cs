using System.Globalization;
using System.Numerics;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels;
public static class LevelParsers
{
    public static Vector2 ParseStageSize(string line)
    {
        int[] size = ParseSameMultiple<int>(line,"StageSize:",2);
        return new(size[0],size[1]);
    }
    public static T[] ParseSameMultiple<T>(string line,string start,int count) where T : IParsable<T>
    {
        return (from x in Strings.ParseParts(line,start,count) select T.Parse(x,null)).ToArray();
    }
    public static float[] ParseMultipleFloats(string line,string start,int count)
    {
        return (from x in Strings.ParseParts(line,start,count) select float.Parse(x,NumberStyles.Any,CultureInfo.InvariantCulture)).ToArray();
    }
}