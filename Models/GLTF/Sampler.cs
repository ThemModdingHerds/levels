using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Sampler : ChildOfRootProperty
{
    [JsonPropertyName("magFilter")]
    public MagFilter? MagFilter {get;set;}
    [JsonPropertyName("minFilter")]
    public MinFilter? MinFilter {get;set;}
    [JsonPropertyName("wrapS")]
    public WrapMode? WrapS {get;set;}
    [JsonPropertyName("wrapT")]
    public WrapMode? WrapT {get;set;}
}
public static class SamplerExt
{
    public static uint AddSampler(this GLTFModel model,Sampler sampler)
    {
        model.Samplers ??= [];
        model.Samplers.Add(sampler);
        return (uint)model.Samplers.IndexOf(sampler);
    }
    public static uint AddSampler(this GLTFModel model,MagFilter? magFilter,MinFilter? minFilter,WrapMode? wrapS,WrapMode? wrapT)
    {
        return AddSampler(model,new Sampler()
        {
            MagFilter = magFilter,
            MinFilter = minFilter,
            WrapS = wrapS,
            WrapT = wrapT
        });
    }
}