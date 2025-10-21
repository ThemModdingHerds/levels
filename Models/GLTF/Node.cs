using System.Numerics;
using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Node : ChildOfRootProperty
{
    [JsonPropertyName("camera")]
    public uint? Camera {get;set;}
    [JsonPropertyName("children")]
    public List<uint>? Children {get;set;}
    [JsonPropertyName("skin")]
    public uint? Skin {get;set;}
    [JsonPropertyName("matrix")]
    public float[]? Matrix {get;set;}
    [JsonPropertyName("mesh")]
    public uint? Mesh {get;set;}
    [JsonPropertyName("rotation")]
    public float[]? Rotation {get;set;}
    [JsonPropertyName("scale")]
    public float[]? Scale {get;set;}
    [JsonPropertyName("translation")]
    public float[]? Translation {get;set;}
    [JsonPropertyName("weights")]
    public float[]? Weights {get;set;}
    public void SetMatrix(Matrix4x4 matrix)
    {
        Matrix = [
            matrix.M11,matrix.M12,matrix.M13,matrix.M14,
            matrix.M21,matrix.M22,matrix.M23,matrix.M24,
            matrix.M31,matrix.M32,matrix.M33,matrix.M34,
            matrix.M41,matrix.M42,matrix.M43,matrix.M44
        ];
    }
    public Matrix4x4 GetMatrix()
    {
        float[] matrix = Matrix ?? throw new Exception("no matrix found");
        return new Matrix4x4(
            matrix[0],matrix[1],matrix[2],matrix[3],
            matrix[4],matrix[5],matrix[6],matrix[7],
            matrix[8],matrix[9],matrix[10],matrix[11],
            matrix[12],matrix[13],matrix[14],matrix[15]
        );
    }
    public void SetTranslation(Vector3 position) => Translation = [position.X,position.Y,position.Z];
    public Vector3 GetTranslation() => new(Translation);
    public void SetScale(Vector3 scale) => Scale = [scale.X,scale.Y,scale.Z];
    public Vector3 GetScale() => new(Scale);
    public void SetRotation(Quaternion rotation) => Rotation = [rotation.X,rotation.Y,rotation.Z,rotation.W];
    public Quaternion GetRotation()
    {
        float[] rotation = Rotation ?? throw new Exception("no rotation found");
        return new(rotation[0],rotation[1],rotation[2],rotation[3]);
    }
}
public static class NodeExt
{
    public static List<Node> GetNodeChildren(this GLTFModel model,Node node)
    {
        if(model.Nodes == null || node.Children == null) return [];
        List<Node> nodes = [];
        for(uint i = 0;i < node.Children.Count;i++)
            nodes.Add(model.Nodes[(int)node.Children[(int)i]]);
        return nodes;
    }
    public static List<Node> GetNodeChildren(this GLTFModel model,uint node)
    {
        if(model.Nodes == null) return [];
        return GetNodeChildren(model,model.Nodes[(int)node]);
    }
    public static uint AddNode(this GLTFModel model,Node node)
    {
        model.Nodes ??= [];
        model.Nodes.Add(node);
        return (uint)model.Nodes.IndexOf(node);
    }
    public static uint AddNode(this GLTFModel model,string name)
    {
        return AddNode(model,new Node()
        {
            Name = name
        });
    }
    public static uint AddNode(this GLTFModel model,string name,IEnumerable<uint> children)
    {
        return AddNode(model,new Node()
        {
            Name = name,
            Children = [..children]
        });
    }
    public static uint AddNode(this GLTFModel model,IEnumerable<uint> children)
    {
        return AddNode(model,new Node()
        {
            Children = [..children]
        });
    }
    public static uint AddNode(this GLTFModel model,string name,IEnumerable<Node> children)
    {
        return AddNode(model,name,from child in children select model.AddNode(child));
    }
    public static uint AddNode(this GLTFModel model,IEnumerable<Node> children)
    {
        return AddNode(model,from child in children select model.AddNode(child));
    }
    public static uint AddCameraNode(this GLTFModel model,string name,Camera camera)
    {
        return AddNode(model,new Node()
        {
            Name = name,
            Camera = model.AddCamera(camera)
        });
    }
    public static uint AddCameraNode(this GLTFModel model,Camera camera)
    {
        return AddNode(model,new Node()
        {
            Camera = model.AddCamera(camera)
        });
    }
    public static uint AddMeshNode(this GLTFModel model,string name,Mesh mesh)
    {
        return AddNode(model,new Node()
        {
            Name = name,
            Mesh = model.AddMesh(mesh)
        });
    }
    public static uint AddMeshNode(this GLTFModel model,Mesh mesh)
    {
        return AddNode(model,new Node()
        {
            Mesh = model.AddMesh(mesh)
        });
    }
    public static uint AddMeshNode(this GLTFModel model,Mesh mesh,float[] weights)
    {
        return AddNode(model,new Node()
        {
            Mesh = model.AddMesh(mesh),
            Weights = weights
        });
    }
    public static uint AddSkinNode(this GLTFModel model,Skin skin)
    {
        return AddNode(model,new Node()
        {
            Skin = model.AddSkin(skin)
        });
    }
}