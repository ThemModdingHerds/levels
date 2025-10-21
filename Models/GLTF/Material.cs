using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Material : ChildOfRootProperty
{
    [JsonPropertyName("pbrMetallicRoughness")]
    public MetallicRoughness? MetallicRoughness {get;set;}
    [JsonPropertyName("normalTexture")]
    public NormalTextureInfo? NormalTexture {get;set;}
    [JsonPropertyName("occlusionTexture")]
    public OcclusionTextureInfo? OcclusionTexture {get;set;}
    [JsonPropertyName("emissiveTexture")]
    public TextureInfo? EmissiveTexture {get;set;}
    [JsonPropertyName("emissiveFactor")]
    public float[]? EmissiveFactor {get;set;}
    [JsonPropertyName("alphaMode")]
    public MaterialAlphaMode? AlphaMode {get;set;}
    [JsonPropertyName("alphaCutoff")]
    public float? AlphaCutoff {get;set;}
    [JsonPropertyName("doubleSided")]
    public bool? DoubleSided {get;set;}
}
public class NormalTextureInfo : TextureInfo
{
    [JsonPropertyName("scale")]
    public float? Scale {get;set;}
}
public class OcclusionTextureInfo : TextureInfo
{
    [JsonPropertyName("strength")]
    public float? Strength {get;set;}
}
public class MetallicRoughness : Property
{
    [JsonPropertyName("baseColorFactor")]
    public float[]? BaseColorFactor {get;set;}
    [JsonPropertyName("baseColorTexture")]
    public TextureInfo? BaseColorTexture {get;set;}
    [JsonPropertyName("metallicFactor")]
    public float? MetallicFactor {get;set;}
    [JsonPropertyName("roughnessFactor")]
    public float? RoughnessFactor {get;set;}
    [JsonPropertyName("metallicRoughnessTexture")]
    public TextureInfo? MetallicRoughnessTexture {get;set;}
}
public static class MaterialExt
{
    public static uint AddMaterial(this GLTFModel model,Material material)
    {
        model.Materials ??= [];
        model.Materials.Add(material);
        return (uint)model.Materials.IndexOf(material);
    }
}