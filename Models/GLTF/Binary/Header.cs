using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Models.GLTF.Binary;
public class Header(uint length)
{
    public const string MAGIC = "glTF";
    public const uint VERSION = 2;
    public uint Length {get;set;} = length;
    public override string ToString()
    {
        return $"{MAGIC}:{VERSION}:{Length}";
    }
}
public static class HeaderExt
{
    public static Header ReadGLTFHeader(this Reader reader)
    {
        string magic = reader.ReadASCII((uint)Header.MAGIC.Length);
        if(magic != Header.MAGIC)
            throw new Exception("no a binary glTF file");
        uint version = reader.ReadUInt();
        if(version != Header.VERSION)
            throw new Exception($"unsupported version of glTF requested: {version}, expected {Header.VERSION}");
        uint length = reader.ReadUInt();
        return new(length);
    }
    public static void Write(this Writer writer,Header header)
    {
        writer.WriteASCII(Header.MAGIC);
        writer.Write(Header.VERSION);
        writer.Write(header.Length);
    }
}