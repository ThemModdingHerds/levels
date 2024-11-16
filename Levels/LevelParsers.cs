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
    public static T[] ParseSameMultiple<T>(string line,string start,int count) where T : IParsable<T>
    {
        T[] values = new T[count];
        int index = 0;
        string rest = line[start.Length..];
        string buffer = string.Empty;
        for(int i = 0;i < rest.Length;i++)
        {
            char letter = rest[i];
            if(letter == ' ')
            {
                if(buffer.Length > 0)
                {
                    values[index] = T.Parse(buffer.Trim(),null);
                    index++;
                    buffer = string.Empty;
                }
                continue;
            }
            buffer += letter;
        }
        if(buffer.Length > 0)
            values[index] = T.Parse(buffer.Trim(),null);
        return values;
    }
    public static float[] ParseMultipleFloats(string line,string start,int count)
    {
        float[] values = new float[count];
        int index = 0;
        string rest = line[start.Length..];
        string buffer = string.Empty;
        for(int i = 0;i < rest.Length;i++)
        {
            char letter = rest[i];
            if(letter == ' ')
            {
                if(buffer.Length > 0)
                {
                    values[index] = float.Parse(buffer.Trim(),NumberStyles.Any,CultureInfo.InvariantCulture);
                    index++;
                    buffer = string.Empty;
                }
                continue;
            }
            buffer += letter;
        }
        if(buffer.Length > 0)
            values[index] = float.Parse(buffer.Trim(),NumberStyles.Any,CultureInfo.InvariantCulture);
        return values;
    }
}