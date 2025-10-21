using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AnimationPath
{
    [JsonStringEnumMemberName("translation")]
    Translation,
    [JsonStringEnumMemberName("rotation")]
    Rotation,
    [JsonStringEnumMemberName("scale")]
    Scale,
    [JsonStringEnumMemberName("weights")]
    Weights,
}