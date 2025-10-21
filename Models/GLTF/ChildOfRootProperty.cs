using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class ChildOfRootProperty : Property
{
    [JsonPropertyName("name")]
    public string? Name {get;set;}
    public override string ToString()
    {
        return ToString(string.Empty);
    }
    protected string ToString(string other)
    {
        return Name ?? base.ToString() ?? other;
    }
}