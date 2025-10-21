using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Models.GLTF;
public partial class Accessor(AccessorComponentType componentType,uint count,AccessorElementType elementType) : ChildOfRootProperty
{
    [JsonPropertyName("bufferView")]
    public uint? BufferView {get;set;}
    [JsonPropertyName("byteOffset")]
    public uint? ByteOffset {get;set;}
    [JsonPropertyName("componentType")]
    public AccessorComponentType ComponentType {get;set;} = componentType;
    [JsonPropertyName("normalized")]
    public bool? Normalized {get;set;}
    [JsonPropertyName("count")]
    public uint Count {get;set;} = count;
    [JsonPropertyName("type")]
    public AccessorElementType Type {get;set;} = elementType;
    [JsonPropertyName("max")]
    public List<float>? Max {get;set;}
    [JsonPropertyName("min")]
    public List<float>? Min {get;set;}
    [JsonPropertyName("sparse")]
    public AccessorSparse? Sparse {get;set;}
    public Accessor(): this(AccessorComponentType.BYTE,0,AccessorElementType.SCALAR)
    {

    }
    public override string ToString()
    {
        return (Name != null ? $"{Name} = " : string.Empty) + $"{Type} {ComponentType}:{Count}";
    }
}
public class AccessorSparse : Property
{
    [JsonPropertyName("count")]
    public uint Count {get;set;}
    [JsonPropertyName("indices")]
    public AccessorSparseIndices Indices {get;set;} = new();
    [JsonPropertyName("values")]
    public AccessorSparseValues Values {get;set;} = new();
}
public class AccessorSparseIndices : Property
{
    [JsonPropertyName("bufferView")]
    public uint BufferView {get;set;}
    [JsonPropertyName("byteOffset")]
    public uint? ByteOffset {get;set;}
    [JsonPropertyName("componentType")]
    public AccessorSparseIndicesComponentType ComponentType {get;set;}
    public override string ToString()
    {
        return $"{ComponentType}";
    }
}
public class AccessorSparseValues : Property
{
    [JsonPropertyName("bufferView")]
    public uint BufferView {get;set;}
    [JsonPropertyName("byteOffset")]
    public uint? ByteOffset {get;set;}
}
public static class AccessorExt
{
    public static uint AddAccessor(this GLTFModel model,Accessor accessor)
    {
        model.Accessors ??= [];
        model.Accessors.Add(accessor);
        return (uint)model.Accessors.IndexOf(accessor);
    }
    public static uint AddAccessor(this GLTFModel model,AccessorComponentType componentType,uint count,AccessorElementType elementType)
    {
        return AddAccessor(model,new Accessor(componentType,count,elementType));
    }
    public static uint AddAccessor(this GLTFModel model,AccessorComponentType componentType,uint count,AccessorElementType elementType,uint bufferView)
    {
        return AddAccessor(model,new Accessor(componentType,count,elementType)
        {
            BufferView = bufferView
        });
    }
    public static BufferView? GetBufferView(this GLTFModel model,Accessor accessor)
    {
        uint? buffeView = accessor.BufferView;
        if(buffeView == null) return null;
        return model.BufferViews?[(int)buffeView];
    }
    public static Buffer? GetBuffer(this GLTFModel model,Accessor accessor)
    {
        BufferView? bufferView = GetBufferView(model,accessor);
        if(bufferView == null) return null;
        return model.GetBuffer(bufferView);
    }
    public static byte[] GetData(this GLTFModel model,Accessor accessor)
    {
        BufferView? bufferView = GetBufferView(model,accessor);
        if(bufferView == null) return [];
        uint offset = (accessor.ByteOffset ?? 0) + (bufferView.ByteOffset ?? 0);
        byte[] data = [];
        return data;
    }
    public static List<Vector2> GetVector2s(this GLTFModel model,Accessor accessor)
    {
        List<Vector2> vectors = [];
        if(accessor.Type != AccessorElementType.VEC2) throw new Exception("not a VEC2");
        byte[] data = GetData(model,accessor);
        using Reader reader = new(data);
        while(reader.Offset < reader.Length)
            vectors.Add(reader.ReadVector2());
        return vectors;
    }
    public static List<Vector3> GetVector3s(this GLTFModel model,Accessor accessor)
    {
        List<Vector3> vectors = [];
        if(accessor.Type != AccessorElementType.VEC3) throw new Exception("not a VEC2");
        byte[] data = GetData(model,accessor);
        using Reader reader = new(data);
        while(reader.Offset < reader.Length)
            vectors.Add(reader.ReadVector3());
        return vectors;
    }
    public static List<Vector4> GetVector4s(this GLTFModel model,Accessor accessor)
    {
        List<Vector4> vectors = [];
        if(accessor.Type != AccessorElementType.VEC4) throw new Exception("not a VEC2");
        byte[] data = GetData(model,accessor);
        using Reader reader = new(data);
        while(reader.Offset < reader.Length)
            vectors.Add(new(reader.ReadFloat(),reader.ReadFloat(),reader.ReadFloat(),reader.ReadFloat()));
        return vectors;
    }
}