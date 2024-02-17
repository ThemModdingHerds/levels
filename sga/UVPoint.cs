using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class UVPoint
{
    public float U {get; set;}
    public float V {get; set;}
}
public static class UVPointExt
{
    public static UVPoint ReadSGAUVPoint(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            U = reader.ReadFloat(),
            V = reader.ReadFloat()
        };
    }
    public static void Write(this Writer writer,UVPoint point)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.Write(point.U);
        writer.Write(point.V);
    }
    public static List<UVPoint> ReadSGAUVPoints(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<UVPoint> points = [];
        for(ulong i = 0;i < count;i++)
            points.Add(ReadSGAUVPoint(reader));
        return points;
    }
    public static void Write(this Writer writer,IEnumerable<UVPoint> points)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(UVPoint point in points)
            Write(writer,point);
    }
}