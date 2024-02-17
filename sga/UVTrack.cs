using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class UVTrack
{
    public string Type {get; set;} = string.Empty;
    public List<UVPoint> AnimationFrames {get; set;} = [];
    // TODO: find out what this is
    public List<Unknown> Unknown {get; set;} = [];
    public List<UVPoint> Time {get; set;} = [];
}
public static class UVTrackExt
{
    public static UVTrack ReadSGAUVTrack(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new UVTrack()
        {
            Type = reader.ReadPascal64String(),
            AnimationFrames = reader.ReadSGAUVPoints(reader.ReadULong()),
            Unknown = reader.ReadSGAUnknowns(reader.ReadULong()),
            Time = reader.ReadSGAUVPoints(reader.ReadULong())
        };
    }
    public static void Write(this Writer writer,UVTrack track)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(track.Type);
        writer.Write(track.AnimationFrames);
        writer.Write(track.Unknown);
        writer.Write(track.Time);
    }
    public static List<UVTrack> ReadSGAUVTracks(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<UVTrack> tracks = [];
        for(ulong i = 0;i < count;i++)
            tracks.Add(ReadSGAUVTrack(reader));
        return tracks;
    }
    public static void Write(this Writer writer,IEnumerable<UVTrack> tracks)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(UVTrack track in tracks)
            Write(writer,track);
    }
}