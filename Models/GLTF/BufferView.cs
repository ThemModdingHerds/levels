using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class BufferView(uint buffer) : ChildOfRootProperty
{
    [JsonPropertyName("buffer")]
    public uint Buffer {get;set;} = buffer;
    [JsonPropertyName("byteOffset")]
    public uint? ByteOffset {get;set;}
    [JsonPropertyName("byteLength")]
    public uint ByteLength {get;set;}
    [JsonPropertyName("byteStride")]
    public uint? ByteStride {get;set;}
    [JsonPropertyName("target")]
    public BufferViewTarget? Target {get;set;}
    public BufferView(): this(0)
    {

    }
}
public static class BufferViewExt
{
    public static uint AddBufferView(this GLTFModel model,BufferView bufferView)
    {
        model.BufferViews ??= [];
        model.BufferViews.Add(bufferView);
        return (uint)model.BufferViews.IndexOf(bufferView);
    }
    public static uint AddBufferView(this GLTFModel model,uint buffer,uint byteLength,uint byteOffset,uint byteStride,BufferViewTarget target)
    {
        return AddBufferView(model,new(buffer)
        {
            ByteOffset = byteOffset,
            ByteLength = byteLength,
            ByteStride = byteStride,
            Target = target
        });
    }
    public static uint AddBufferView(this GLTFModel model,uint buffer,uint byteLength,uint byteOffset,BufferViewTarget target)
    {
        return AddBufferView(model,new(buffer)
        {
            ByteOffset = byteOffset,
            ByteLength = byteLength,
            Target = target
        });
    }
    public static uint AddBufferView(this GLTFModel model,uint buffer,uint byteLength,BufferViewTarget target)
    {
        return AddBufferView(model,new(buffer)
        {
            ByteLength = byteLength,
            Target = target
        });
    }
    public static uint AddBufferView(this GLTFModel model,uint buffer,uint byteLength,uint byteOffset,uint byteStride)
    {
        return AddBufferView(model,new(buffer)
        {
            ByteOffset = byteOffset,
            ByteLength = byteLength,
            ByteStride = byteStride
        });
    }
    public static uint AddBufferView(this GLTFModel model,uint buffer,uint byteLength,uint byteOffset)
    {
        return AddBufferView(model,new(buffer)
        {
            ByteOffset = byteOffset,
            ByteLength = byteLength
        });
    }
    public static uint AddBufferView(this GLTFModel model,uint buffer,uint byteLength)
    {
        return AddBufferView(model,new(buffer)
        {
            ByteLength = byteLength
        });
    }
    public static Buffer? GetBuffer(this GLTFModel model,BufferView bufferView)
    {
        uint? buffer = bufferView?.Buffer;
        if(buffer == null) return null;
        return model.Buffers?[(int)buffer];
    }
}