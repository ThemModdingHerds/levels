using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class SGAnimation(uint unknown,float animationLengthInSeconds,IEnumerable<Bone> bones,IEnumerable<UVTrack> tracks)
{
    public const string VERSION = "3.0";
    public string Version {get; set;} = VERSION;
    // TODO: dafuq is this?
    public uint Unknown {get; set;} = unknown;
    public float AnimationLengthInSeconds {get; set;} = animationLengthInSeconds;
    public List<Bone> Bones {get; set;} = [..bones];
    public List<UVTrack> UVTracks {get; set;} = [..tracks];
    public SGAnimation(uint unknown,float animationLengthInSeconds): this(unknown,animationLengthInSeconds,[],[])
    {

    }
    public SGAnimation(): this(0,0)
    {

    }
}
public static class SGAnimationExt
{
    public static SGAnimation ReadSGAFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        if(version != SGAnimation.VERSION)
            throw new Exception($"SGI version mismatch: got '{version}', expected '{SGAnimation.VERSION}'");
        uint unknown = reader.ReadUInt();
        ulong bonesC = reader.ReadULong();
        ulong tracksC = reader.ReadULong();
        float animationLengthInSeconds = reader.ReadFloat();
        List<Bone> bones = reader.ReadSGABones(bonesC);
        List<UVTrack> tracks = reader.ReadSGAUVTracks(tracksC);
        return new(unknown,animationLengthInSeconds,bones,tracks);
    }
    public static void Write(this Writer writer,SGAnimation file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write(file.Unknown);
        writer.Write((ulong)file.Bones.Count);
        writer.Write((ulong)file.UVTracks.Count);
        writer.Write(file.AnimationLengthInSeconds);
        writer.Write(file.Bones);
        writer.Write(file.UVTracks);
    }
}