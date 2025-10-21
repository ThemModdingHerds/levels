using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Scene : ChildOfRootProperty
{
    [JsonPropertyName("nodes")]
    public HashSet<uint>? Nodes {get;set;}
}
public static class SceneExt
{
    public static uint AddScene(this GLTFModel model,Scene scene)
    {
        model.Scenes ??= [];
        model.Scenes.Add(scene);
        uint index = (uint)model.Scenes.IndexOf(scene);
        model.Scene ??= index;
        return index;
    }
    public static uint AddScene(this GLTFModel model,string name,IEnumerable<uint> nodes)
    {
        return AddScene(model,new Scene()
        {
            Name = name,
            Nodes = [..nodes]
        });
    }
    public static uint AddScene(this GLTFModel model,string name) => AddScene(model,name,Array.Empty<uint>());
    public static uint AddScene(this GLTFModel model,IEnumerable<uint> nodes)
    {
        return AddScene(model,new Scene()
        {
            Nodes = [..nodes]
        });
    }
    public static uint AddScene(this GLTFModel model) => AddScene(model,Array.Empty<uint>());
    public static uint AddScene(this GLTFModel model,string name,IEnumerable<Node> nodes)
    {
        return AddScene(model,name,from node in nodes select model.AddNode(node));
    }
    public static uint AddScene(this GLTFModel model,IEnumerable<Node> nodes)
    {
        return AddScene(model,from node in nodes select model.AddNode(node));
    }
    public static List<Node> GetSceneNodes(this GLTFModel model,Scene scene)
    {
        if(model.Nodes == null || scene.Nodes == null) return [];
        List<Node> nodes = [];
        for(uint i = 0;i < scene.Nodes.Count;i++)
            nodes.Add(model.Nodes[(int)scene.Nodes.ElementAt((int)i)]);
        return nodes;
    }
    public static List<Node> GetSceneNodes(this GLTFModel model,uint scene)
    {
        if(model.Scenes == null) return [];
        return GetSceneNodes(model,model.Scenes[(int)scene]);
    }
}