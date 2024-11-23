using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class SkullGirlsAnimation(uint unknown,float animationLengthInSeconds,IEnumerable<Bone> bones,IEnumerable<UVTrack> tracks)
{
    public const string VERSION = "3.0";
    public string Version {get; set;} = VERSION;
    // TODO: dafuq is this?
    public uint Unknown {get; set;} = unknown;
    public float AnimationLengthInSeconds {get; set;} = animationLengthInSeconds;
    public List<Bone> Bones {get; set;} = [..bones];
    public List<UVTrack> UVTracks {get; set;} = [..tracks];
    public SkullGirlsAnimation(uint unknown,float animationLengthInSeconds): this(unknown,animationLengthInSeconds,[],[])
    {

    }
    public SkullGirlsAnimation(): this(0,0)
    {

    }
}
public static class SkullGirlsAnimationExt
{
    public static SkullGirlsAnimation ReadSGAFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        uint unknown = reader.ReadUInt();
        if(version != SkullGirlsAnimation.VERSION)
            throw new Exception($"SGI version mismatch: got '{version}', expected '{SkullGirlsAnimation.VERSION}'");
        ulong bonesC = reader.ReadULong();
        ulong tracksC = reader.ReadULong();
        float animationLengthInSeconds = reader.ReadFloat();
        List<Bone> bones = reader.ReadSGABones(bonesC);
        List<UVTrack> tracks = reader.ReadSGAUVTracks(tracksC);
        return new(unknown,animationLengthInSeconds,bones,tracks);
    }
    public static void Write(this Writer writer,SkullGirlsAnimation file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write(file.Unknown);
        writer.Write(file.AnimationLengthInSeconds);
        writer.Write((ulong)file.Bones.Count);
        writer.Write((ulong)file.UVTracks.Count);
        writer.Write(file.Bones);
        writer.Write(file.UVTracks);
    }
}