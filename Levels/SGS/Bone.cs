using System.Numerics;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGS;
public class Bone(string name,uint parentBone,Matrix4x4 matrix)
{
    public string Name {get; set;} = name;
    public uint ParentBone {get; set;} = parentBone;
    public bool IsRootBone {get => ParentBone == 0xFFFFFFFF;}
    public Matrix4x4 Matrix {get; set;} = matrix;
    public Bone(): this(string.Empty,0,Matrix4x4.Identity)
    {

    }
}
public static class BoneExt
{
    public static Bone ReadSGSBone(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string name = reader.ReadPascal64String();
        uint parentBone = reader.ReadUInt();
        Matrix4x4 matrix = reader.ReadMatrix4x4();
        return new(name,parentBone,matrix);
    }
    public static void Write(this Writer writer,Bone bone)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(bone.Name);
        writer.Write(bone.ParentBone);
        writer.WriteMatrix4x4(bone.Matrix);
    }
    public static List<Bone> ReadSGSBones(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Bone> bones = [];
        for(ulong i = 0;i < count;i++)
            bones.Add(ReadSGSBone(reader));
        return bones;
    }
    public static void Write(this Writer writer,IEnumerable<Bone> bones)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Bone bone in bones)
            Write(writer,bone);
    }
}