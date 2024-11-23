using System.Numerics;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class Bone(string name,IEnumerable<Vector3> unknown1s,IEnumerable<Vector4> unknown2s,IEnumerable<Vector3> unknown3s)
{
    public string Name {get; set;} = name;
    public List<Vector3> Unknown2s {get; set;} = [..unknown1s];
    public List<Vector4> Unknown3s {get; set;} = [..unknown2s];
    public List<Vector3> Unknown4s {get; set;} = [..unknown3s];
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
    public static Bone ReadSGABone(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string name = reader.ReadPascal64String();
        List<Vector3> unknown1s = reader.ReadVectors3(reader.ReadULong());
        List<Vector4> unknown2s = reader.ReadList((r) => new Vector4(r.ReadFloat(),r.ReadFloat(),r.ReadFloat(),r.ReadFloat()),reader.ReadULong());
        List<Vector3> unknown3s = reader.ReadVectors3(reader.ReadULong());
        return new(name,unknown1s,unknown2s,unknown3s);
    }
    public static void Write(this Writer writer,Bone bone)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(bone.Name);
        writer.WriteVectors3(bone.Unknown2s);
        writer.Write(bone.Unknown3s,(w,v) => {w.Write(v.X);w.Write(v.Y);w.Write(v.Z);w.Write(v.W);});
        writer.WriteVectors3(bone.Unknown4s);
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