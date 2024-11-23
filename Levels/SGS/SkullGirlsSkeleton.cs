using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGS;
public class SkullGirlsSkeleton(IEnumerable<Bone> bones)
{
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
    public List<Bone> Bones {get;set;} = [..bones];
    public SkullGirlsSkeleton(): this([])
    {

    }
}
public static class SkullGirlsSkeletonExt
{
    public static SkullGirlsSkeleton ReadSGSFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        List<Bone> bones = [];
        if(version != SkullGirlsSkeleton.VERSION)
            throw new Exception($"SGS version mismatch: got '{version}', expected '{SkullGirlsSkeleton.VERSION}'");
        bones.AddRange(reader.ReadSGSBones(reader.ReadULong()));
        return new(bones);
    }
    public static void Write(this Writer writer,SkullGirlsSkeleton file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write((ulong)file.Bones.Count);
        writer.Write(file.Bones);
    }
}