using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.Models;
using ThemModdingHerds.Levels.Models.Wavefront;

namespace ThemModdingHerds.Levels.SGM;
public class Triangle
{
    public short Z {get; set;}
    public short Y {get; set;}
    public short X {get; set;}
    public Face ToFace()
    {
        return new(X,Y,Z);
    }
    public static Triangle FromFace(IFace face)
    {
        return new()
        {
            X = (short)face.X,
            Y = (short)face.Y,
            Z = (short)face.Z
        };
    }
}
public static class TriangleExt
{
    public static Triangle ReadSGMTriangle(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            X = reader.ReadShort(),
            Y = reader.ReadShort(),
            Z = reader.ReadShort()
        };
    }
    public static void Write(this Writer writer,Triangle Triangle)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.Write(Triangle.X);
        writer.Write(Triangle.Y);
        writer.Write(Triangle.Z);
    }
    public static List<Triangle> ReadSGMTriangles(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Triangle> Triangles = [];
        for(ulong i = 0;i < count;i++)
            Triangles.Add(ReadSGMTriangle(reader));
        return Triangles;
    }
    public static void Write(this Writer writer,IEnumerable<Triangle> Triangles)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Triangle Triangle in Triangles)
            Write(writer,Triangle);
    }
}