using System.Text.Json.Serialization;

namespace ThemModdingHerds.Levels;
public class Background2D(string path)
{
    public const string DEFAULT_PATH = "temp/levels/textures/2D/2d_training.dds";
    [JsonPropertyName("path")]
    public string Path {get; set;} = path;
    public Background2D(): this(DEFAULT_PATH)
    {

    }
    public override string ToString()
    {
        return $"2D {Path}";
    }
}