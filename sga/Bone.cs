using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class Bone
{
    public string Name {get; set;} = string.Empty;
    public List<Unknown2> Unknown2s {get; set;} = [];
    public List<Unknown3> Unknown3s {get; set;} = [];
    public List<Unknown4> Unknown4s {get; set;} = [];
}
public static class BoneExt
{
    public static Bone ReadSGABone(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            Name = reader.ReadPascal64String(),
            Unknown2s = reader.ReadSGAUnknown2s(reader.ReadULong()),
            Unknown3s = reader.ReadSGAUnknown3s(reader.ReadULong()),
            Unknown4s = reader.ReadSGAUnknown4s(reader.ReadULong())
        };
    }
    public static void Write(this Writer writer,Bone bone)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(bone.Name);
        writer.Write(bone.Unknown2s);
        writer.Write(bone.Unknown3s);
        writer.Write(bone.Unknown4s);
    }
    public static List<Bone> ReadSGABones(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Bone> bones = [];
        for(ulong i = 0;i < count;i++)
            bones.Add(ReadSGABone(reader));
        return bones;
    }
    public static void Write(this Writer writer,IEnumerable<Bone> bones)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Bone bone in bones)
            Write(writer,bone);
    }
}