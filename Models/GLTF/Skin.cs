using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Skin : ChildOfRootProperty
{
    [JsonPropertyName("inverseBindMatrices")]
    public uint? InverseBindMatrices {get;set;}
    [JsonPropertyName("skeleton")]
    public uint? Skeleton {get;set;}
    [JsonPropertyName("joints")]
    public HashSet<uint> Joints {get;set;} = [];
}
public static class SkinExt
{
    public static uint AddSkin(this GLTFModel model,Skin skin)
    {
        model.Skins ??= [];
        model.Skins.Add(skin);
        return (uint)model.Skins.IndexOf(skin);
    }
    public static Skin? GetNodeSkin(this GLTFModel model,Node node)
    {
        if(model.Skins == null || node.Skin == null) return null;
        return model.Skins[(int)node.Skin];
    }
    public static Skin? GetNodeSkin(this GLTFModel model,uint node)
    {
        if(model.Nodes == null) return null;
        return GetNodeSkin(model,model.Nodes[(int)node]);
    }
}