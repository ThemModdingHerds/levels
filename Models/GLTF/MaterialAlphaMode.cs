using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MaterialAlphaMode
{
    OPAQUE,
    MASK,
    BLEND
}