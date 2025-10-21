using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGS;
public class SGSkeleton(IEnumerable<Bone> bones)
{
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
    public List<Bone> Bones {get;set;} = [..bones];
    public Bone RootBone => Bones.Find((bone) => bone.IsRootBone) ?? throw new Exception("no root bone");
    public SGSkeleton(): this([])
    {

    }
}
public static class SGSkeletonExt
{
    public static SGSkeleton ReadSGSFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        List<Bone> bones = [];
        if(version != SGSkeleton.VERSION)
            throw new Exception($"SGS version mismatch: got '{version}', expected '{SGSkeleton.VERSION}'");
        bones.AddRange(reader.ReadSGSBones(reader.ReadULong()));
        return new(bones);
    }
    public static void Write(this Writer writer,SGSkeleton file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write((ulong)file.Bones.Count);
        writer.Write(file.Bones);
    }
}