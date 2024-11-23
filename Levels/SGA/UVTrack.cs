using System.Numerics;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class UVTrack(string type,IEnumerable<Vector2> frames,IEnumerable<Unknown> unknowns,IEnumerable<Vector2> time)
{
    public string Type {get; set;} = type;
    public List<Vector2> AnimationFrames {get; set;} = [..frames];
    // TODO: find out what this is
    public List<Unknown> Unknown {get; set;} = [..unknowns];
    public List<Vector2> Time {get; set;} = [..time];
    public UVTrack(string type): this(type,[],[],[])
    {

    }
    public UVTrack(): this(string.Empty)
    {

    }
    public override string ToString()
    {
        return $"{Type}:{AnimationFrames.Count}:{Unknown.Count}:{Time.Count}";
    }
}
public static class UVTrackExt
{
    public static UVTrack ReadSGAUVTrack(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string type = reader.ReadPascal64String();
        List<Vector2> frames = reader.ReadVectors2(reader.ReadULong());
        List<Unknown> unknown = reader.ReadSGAUnknowns(reader.ReadULong());
        List<Vector2> time = reader.ReadVectors2(reader.ReadULong());
        return new(type,frames,unknown,time);
    }
    public static void Write(this Writer writer,UVTrack track)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(track.Type);
        writer.WriteVectors2(track.AnimationFrames);
        writer.Write(track.Unknown);
        writer.WriteVectors2(track.Time);
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