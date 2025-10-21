using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Buffer : ChildOfRootProperty
{
    [JsonPropertyName("uri")]
    public string? URI {get;set;}
    [JsonPropertyName("byteLength")]
    public uint ByteLength {get;set;}
    public override string ToString()
    {
        return URI ?? base.ToString();
    }
}
public static class BufferExt
{
    public static uint AddBuffer(this GLTFModel model,Buffer buffer,byte[] data)
    {
        model.Buffers ??= [];
        model.Buffers.Add(buffer);
        model.Data.Add(data);
        return (uint)model.Buffers.IndexOf(buffer);
    }
    public static uint AddBuffer(this GLTFModel model,byte[] data)
    {
        return AddBuffer(model,new Buffer()
        {
            ByteLength = (uint)data.Length
        },data);
    }
    public static uint AddBuffer(this GLTFModel model,string filepath)
    {
        if(!File.Exists(filepath)) throw new FileNotFoundException($"couldn't add '{filepath}' as Buffer",filepath);
        FileInfo info = new(filepath);
        return AddBuffer(model,new Buffer()
        {
            URI = Path.GetFileName(filepath),
            ByteLength = (uint)info.Length
        },File.ReadAllBytes(filepath));
    }
    public static byte[] GetData(this GLTFModel model,Buffer buffer)
    {
        return model.Data[model.Buffers?.IndexOf(buffer) ?? 0];
    }
}