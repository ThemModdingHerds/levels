using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Models.GLTF.Binary;
public class Chunk(string type,byte[] data)
{
    public const int TYPE_LENGTH = 4;
    public const string JSON = "JSON";
    public const string BIN = "BIN\0";
    public string Type {get;set;} = type;
    public byte[] Data {get;set;} = data;
    public override string ToString()
    {
        return Type;
    }
}
public static class ChunkExt
{
    public static Chunk ReadGLTFChunk(this Reader reader)
    {
        uint length = reader.ReadUInt();
        string type = reader.ReadASCII(Chunk.TYPE_LENGTH);
        byte[] data = new byte[length];
        reader.ReadBytes(data);
        return new(type,data);
    }
    public static void Write(this Writer writer,Chunk chunk)
    {
        writer.Write((uint)chunk.Data.Length);
        writer.WriteASCII(chunk.Type);
        writer.Write(chunk.Data);
    }
}