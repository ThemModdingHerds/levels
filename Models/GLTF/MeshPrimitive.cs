using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class MeshPrimitive : Property
{
    [JsonPropertyName("attributes")]
    public Dictionary<string,int> Attributes {get;set;} = [];
    [JsonPropertyName("indices")]
    public int? Indices {get;set;}
    [JsonPropertyName("material")]
    public uint? Material {get;set;}
    [JsonPropertyName("mode")]
    public MeshPrimitiveMode? Mode {get;set;}
    [JsonPropertyName("targets")]
    public List<Dictionary<string,int>>? Targets {get;set;} = null;
}