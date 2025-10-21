using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccessorElementType
{
    SCALAR,
    VEC2,
    VEC3,
    VEC4,
    MAT2,
    MAT3,
    MAT4,
}