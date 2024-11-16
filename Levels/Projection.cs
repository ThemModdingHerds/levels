using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels;
public class Projection(float unknown,float unknown2,float unknown3,float unknown4,float unknown5)
{
    public float Unknown {get; set;} = unknown;
    public float Unknown2 {get; set;} = unknown2;
    public float Unknown3 {get; set;} = unknown3;
    public float Unknown4 {get; set;} = unknown4;
    public float Unknown5 {get; set;} = unknown5;
    public Projection(): this(0,0,0,0,0)
    {

    }
    public override string ToString()
    {
        return $"PROJ {Strings.FloatStringWithZeros(Unknown)} {Strings.FloatStringWithZeros(Unknown2)} {Strings.FloatStringWithZeros(Unknown3)} {Strings.FloatStringWithZeros(Unknown4)} {Strings.FloatStringWithZeros(Unknown5)}";
    }
    public static Projection Parse(string s)
    {
        float[] values = LevelParsers.ParseSameMultiple<float>(s,"PROJ",5);
        return new(values[0],values[1],values[2],values[3],values[4]);
    }
}