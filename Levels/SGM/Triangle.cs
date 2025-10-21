using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Models;

namespace ThemModdingHerds.Levels.SGM;
public class Triangle(short x,short y,short z)
{
    public short X {get; set;} = x;
    public short Y {get; set;} = y;
    public short Z {get; set;} = z;
    public Triangle(): this(0,0,0)
    {

    }
    public override string ToString()
    {
        return $"[{X},{Y},{Z}]";
    }
    public Face GetFace()
    {
        Face f = new(X,Y,Z);
        f.SetNaive();
        return f;
    }
}
public static class TriangleExt
{
    public static Triangle ReadSGMTriangle(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        short x = reader.ReadShort();
        short y = reader.ReadShort();
        short z = reader.ReadShort();
        return new(x,y,z);
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