using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGM;
public class Triangle
{
    public short FirstIndex {get; set;}
    public short SecondIndex {get; set;}
    public short ThirdIndex {get; set;}
}
public static class TriangleExt
{
    public static Triangle ReadSGMTriangle(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            FirstIndex = reader.ReadShort(),
            SecondIndex = reader.ReadShort(),
            ThirdIndex = reader.ReadShort()
        };
    }
    public static void Write(this Writer writer,Triangle Triangle)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.Write(Triangle.FirstIndex);
        writer.Write(Triangle.SecondIndex);
        writer.Write(Triangle.ThirdIndex);
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