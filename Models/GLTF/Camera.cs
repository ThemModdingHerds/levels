using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Camera : ChildOfRootProperty
{
    [JsonPropertyName("type")]
    public virtual string Type {get;set;} = string.Empty;
    [JsonPropertyName("perspective")]
    public Perspective? Perspective {get;set;}
    [JsonPropertyName("orthographic")]
    public Orthographic? Orthographic {get;set;}
}
public class Perspective : Property
{
    [JsonPropertyName("aspectRatio")]
    public float? AspectRatio {get;set;}
    [JsonPropertyName("yfov")]
    public float FieldOfView {get;set;}
    [JsonPropertyName("zfar")]
    public float ZFar {get;set;}
    [JsonPropertyName("znear")]
    public float? ZNear {get;set;}
}
public class Orthographic : Property
{
    [JsonPropertyName("xmag")]
    public float XMag {get;set;}
    [JsonPropertyName("ymag")]
    public float YMag {get;set;}
    [JsonPropertyName("zfar")]
    public float ZFar {get;set;}
    [JsonPropertyName("znear")]
    public float ZNear {get;set;}
}
public static class CameraExt
{
    public static uint AddCamera(this GLTFModel model,Camera camera)
    {
        model.Cameras ??= [];
        model.Cameras.Add(camera);
        return (uint)model.Cameras.IndexOf(camera);
    }
    public static Camera? GetNodeCamera(this GLTFModel model,Node node)
    {
        if(node.Camera == null) return null;
        return model.Cameras?[(int)node.Camera];
    }
    public static Camera? GetNodeCamera(this GLTFModel model,uint node)
    {
        if(model.Nodes == null) return null;
        return GetNodeCamera(model,model.Nodes[(int)node]);
    }
}