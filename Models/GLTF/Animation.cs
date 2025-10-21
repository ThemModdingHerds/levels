using System.Formats.Asn1;
using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Animation : ChildOfRootProperty
{
    [JsonPropertyName("channels")]
    public List<AnimationChannel> Channels {get;set;} = [];
    [JsonPropertyName("samplers")]
    public List<AnimationSampler> Samplers {get;set;} = [];
    public List<AnimationChannel> GetChannels(uint node)
    {
        List<AnimationChannel> channels = [];
        foreach(AnimationChannel channel in Channels)
            if(channel.Target.Node == node)
                channels.Add(channel);
        return channels;
    }
}
public class AnimationChannel : Property
{
    [JsonPropertyName("sampler")]
    public uint Sampler {get;set;}
    [JsonPropertyName("target")]
    public AnimationChannelTarget Target {get;set;} = new();
}
public class AnimationChannelTarget : Property
{
    [JsonPropertyName("node")]
    public uint? Node {get;set;}
    [JsonPropertyName("path")]
    public AnimationPath Path {get;set;}
}
public class AnimationSampler : Property
{
    [JsonPropertyName("input")]
    public uint Input {get;set;}
    [JsonPropertyName("interpolation")]
    public AnimationInterpolation? Interpolation {get;set;}
    [JsonPropertyName("output")]
    public uint Output {get;set;}
}
public static class AnimationExt
{
    public static uint AddAnimation(this GLTFModel model,Animation animation)
    {
        model.Animations ??= [];
        model.Animations.Add(animation);
        return (uint)model.Animations.IndexOf(animation);
    }
    public static List<AnimationChannel> GetAnimationChannels(this GLTFModel model,Node node)
    {
        if(model.Nodes == null) return [];
        uint id = (uint)model.Nodes.IndexOf(node);
        List<AnimationChannel> channels = [];

        foreach(Animation animation in model.Animations ?? [])
            channels.AddRange(animation.GetChannels(id));

        return channels;
    }
}