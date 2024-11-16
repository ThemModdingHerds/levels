using System.Text.Json.Serialization;

namespace ThemModdingHerds.Levels;
public class Camera(long fov,long near,long far)
{
    [JsonPropertyName("fov")]
    public long FieldOfView {get; set;} = fov;
    [JsonPropertyName("near")]
    public long ZNear {get; set;} = near;
    [JsonPropertyName("far")]
    public long ZFar {get; set;} = far;
    public Camera(): this(90,0,0)
    {

    }
    public override string ToString()
    {
        return $"CAMERA {FieldOfView} {ZNear} {ZFar}";
    }
    public static Camera Parse(string s)
    {
        long[] values = LevelParsers.ParseSameMultiple<long>(s,"CAMERA",3);
        return new(values[0],values[1],values[2]);
    }
}