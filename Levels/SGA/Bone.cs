using System.Numerics;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class Bone(string name,IEnumerable<Vector3> unknown1s,IEnumerable<Vector4> unknown2s,IEnumerable<Vector3> unknown3s)
{
    public string Name {get; set;} = name;
    public List<Vector3> Unknown1s {get; set;} = [..unknown1s];
    public List<Vector4> Unknown2s {get; set;} = [..unknown2s];
    public List<Vector3> Unknown3s {get; set;} = [..unknown3s];
    public Bone(string name): this(name,[],[],[])
    {

    }
    public Bone(): this(string.Empty)
    {

    }
    public override string ToString()
    {
        return Name;
    }
}
public static class BoneExt
{
    private static Vector4 ReadVector4(this Reader reader)
    {
        return new Vector4(reader.ReadFloat(),reader.ReadFloat(),reader.ReadFloat(),reader.ReadFloat());
    }
    private static List<Vector4> ReadVectors4(this Reader reader,ulong count)
    {
        List<Vector4> vectors = [];
        for(ulong i = 0;i < count;i++)
            vectors.Add(reader.ReadVector4());
        return vectors;
    }
    private static void WriteVector4(this Writer writer,Vector4 vector)
    {
        writer.Write(vector.X);
        writer.Write(vector.Y);
        writer.Write(vector.Z);
        writer.Write(vector.W);
    }
    private static void WriteVectors4(this Writer writer,IEnumerable<Vector4> vectors)
    {
        foreach(Vector4 vector in vectors)
            writer.WriteVector4(vector);
    }
    public static Bone ReadSGABone(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string name = reader.ReadPascal64String();
        ulong unknown1count = reader.ReadULong();
        List<Vector3> unknown1s = reader.ReadVectors3(unknown1count);
        ulong unknown2count = reader.ReadULong();
        List<Vector4> unknown2s = reader.ReadVectors4(unknown2count);
        ulong unknown3count = reader.ReadULong();
        List<Vector3> unknown3s = reader.ReadVectors3(unknown3count);
        return new(name,unknown1s,unknown2s,unknown3s);
    }
    public static void Write(this Writer writer,Bone bone)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(bone.Name);
        writer.Write((ulong)bone.Unknown1s.Count);
        writer.WriteVectors3(bone.Unknown1s);
        writer.Write((ulong)bone.Unknown2s.Count);
        writer.WriteVectors4(bone.Unknown2s);
        writer.Write((ulong)bone.Unknown3s.Count);
        writer.WriteVectors3(bone.Unknown3s);
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