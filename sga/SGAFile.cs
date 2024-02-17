using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGA;

namespace ThemModdingHerds.Levels;
public class SGAFile
{
    public const string VERSION = "3.0";
    public string Version {get; set;} = VERSION;
    // TODO: dafuq is this?
    public uint Unknown {get; set;}
    public float AnimationLengthInSeconds {get; set;}
    public List<Bone> Bones {get; set;} = [];
    public List<UVTrack> UVTracks {get; set;} = [];
}
public static class SGAFileExt
{
    public static SGAFile ReadSGAFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        SGAFile file = new()
        {
            Version = reader.ReadPascal64String(),
            Unknown = reader.ReadUInt()
        };
        if(file.Version != SGAFile.VERSION)
            throw new Exception($"SGI version mismatch: got '{file.Version}', expected '{SGAFile.VERSION}'");
        ulong bones = reader.ReadULong();
        ulong tracks = reader.ReadULong();
        file.AnimationLengthInSeconds = reader.ReadFloat();
        file.Bones.AddRange(reader.ReadSGABones(bones));
        file.UVTracks.AddRange(reader.ReadSGAUVTracks(tracks));
        return file;
    }
    public static void Write(this Writer writer,SGAFile file)
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