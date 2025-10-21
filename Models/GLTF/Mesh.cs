using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Mesh : ChildOfRootProperty
{
    [JsonPropertyName("primitives")]
    public List<MeshPrimitive> Primitives {get;set;} = [];
    [JsonPropertyName("weights")]
    public float[]? Weights {get;set;}
}
public static class MeshExt
{
    public static uint AddMesh(this GLTFModel model,Mesh mesh)
    {
        model.Meshes ??= [];
        model.Meshes.Add(mesh);
        return (uint)model.Meshes.IndexOf(mesh);
    }
    public static Mesh? GetNodeMesh(this GLTFModel model,Node node)
    {
        if(model.Meshes == null || node.Mesh == null) return null;
        return model.Meshes[(int)node.Mesh];
    }
    public static Mesh? GetNodeMesh(this GLTFModel model,uint node)
    {
        if(model.Nodes == null) return null;
        return GetNodeMesh(model,model.Nodes[(int)node]);
    }
}