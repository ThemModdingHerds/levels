using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AnimationInterpolation
{
    LINEAR,
    STEP,
    CUBICSPLINE
}