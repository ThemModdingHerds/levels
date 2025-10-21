using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Property
{
    [JsonPropertyName("extensions")]
    public Dictionary<string,object>? Extensions {get;set;}
    [JsonPropertyName("extras")]
    public Dictionary<string,object>? Extras {get;set;}
    public T? GetExtension<T>(string name)
    {
        return (T?)Extensions?[name];
    }
    public T? GetExtra<T>(string name)
    {
        return (T?)Extras?[name];
    }
}