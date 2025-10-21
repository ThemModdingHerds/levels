using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class TextureInfo : Property
{
    [JsonPropertyName("index")]
    public uint Index {get;set;}
    [JsonPropertyName("texCoord")]
    public uint? TexCoord {get;set;}
}