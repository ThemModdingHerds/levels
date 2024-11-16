using System.Globalization;
using System.Text.Json.Serialization;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels;
public class Background3D(float tiltRate,float tiltHeight1,float tiltHeight2)
{
    [JsonPropertyName("tiltRate")]
    public float TiltRate {get; set;} = tiltRate;
    [JsonPropertyName("tiltHeight1")]
    public float TiltHeight1 {get; set;} = tiltHeight1;
    [JsonPropertyName("tiltHeight2")]
    public float TiltHeight2 {get; set;} = tiltHeight2;
    public Background3D(): this(0,0,0)
    {

    }
    public override string ToString()
    {
        return $"3D {Strings.FloatString(TiltRate)} {TiltHeight1} {TiltHeight2}";
    }
    public static Background3D Parse(string s)
    {
        float[] values = LevelParsers.ParseMultipleFloats(s,"3D",3);
        return new(values[0],values[1],values[2]);
    }
}