using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.Common;

namespace ThemModdingHerds.Levels.SGS;
public class Bone
{
    public string Name {get; set;} = string.Empty;
    public uint ParentBone {get; set;}
    public bool IsRootBone {get => ParentBone == 0xFFFFFFFF;}
    public float[] Matrix {get; set;} = new float[16];
}
public static class BoneExt
{
    public static Bone ReadSGSBone(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            Name = reader.ReadPascal64String(),
            ParentBone = reader.ReadUInt(),
            Matrix = reader.ReadMatrix()
        };
    }
    public static void Write(this Writer writer,Bone bone)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(bone.Name);
        writer.Write(bone.ParentBone);
        writer.WriteMatrix(bone.Matrix);
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