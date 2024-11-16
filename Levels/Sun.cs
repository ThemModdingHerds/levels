using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels;
public class Sun(float unknown,float unknown2,float unknown3,float unknown4,float unknown5,float unknown6,float unknown7,float unknown8,float unknown9)
{
    public float Unknown {get; set;} = unknown;
    public float Unknown2 {get; set;} = unknown2;
    public float Unknown3 {get; set;} = unknown3;
    public float Unknown4 {get; set;} = unknown4;
    public float Unknown5 {get; set;} = unknown5;
    public float Unknown6 {get; set;} = unknown6;
    public float Unknown7 {get; set;} = unknown7;
    public float Unknown8 {get; set;} = unknown8;
    public float Unknown9 {get; set;} = unknown9;
    public Sun(): this(0,0,0,0,0,0,0,0,0)
    {

    }
    public override string ToString()
    {
        return $"SUN {Strings.FloatStringWithZeros(Unknown)} {Strings.FloatStringWithZeros(Unknown2)} {Strings.FloatStringWithZeros(Unknown3)} {Strings.FloatStringWithZeros(Unknown4)} {Strings.FloatStringWithZeros(Unknown5)} {Strings.FloatStringWithZeros(Unknown6)} {Strings.FloatStringWithZeros(Unknown7)} {Strings.FloatStringWithZeros(Unknown8)} {Strings.FloatStringWithZeros(Unknown9)}";
    }
    public static Sun Parse(string s)
    {
        float[] values = LevelParsers.ParseMultipleFloats(s,"SUN",9);
        return new(values[0],values[1],values[2],values[3],values[4],values[5],values[6],values[7],values[8]);
    }
}