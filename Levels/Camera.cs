using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels;
public class Camera(long fov,long near,BigInteger far)
{
    [JsonPropertyName("fov")]
    public long FieldOfView {get; set;} = fov;
    [JsonPropertyName("near")]
    public long ZNear {get; set;} = near;
    [JsonPropertyName("far")]
    public BigInteger ZFar {get; set;} = far;
    public Camera(): this(90,0,0)
    {

    }
    public override string ToString()
    {
        return $"CAMERA {FieldOfView} {ZNear} {ZFar}";
    }
    public static Camera Parse(string s)
    {
        string[] values = Strings.ParseParts(s,"CAMERA",3);
        return new(long.Parse(values[0]),long.Parse(values[1]),BigInteger.Parse(values[2]));
    }
}