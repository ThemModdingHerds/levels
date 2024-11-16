using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class Unknown2
{
    public float A {get; set;}
    public float B {get; set;}
    public float C {get; set;}
}
public static class Unknown2Ext
{
    public static Unknown2 ReadSGAUnknown2(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            A = reader.ReadFloat(),
            B = reader.ReadFloat(),
            C = reader.ReadFloat()
        };
    }
    public static void Write(this Writer writer,Unknown2 unknown)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.Write(unknown.A);
        writer.Write(unknown.B);
        writer.Write(unknown.C);
    }
    public static List<Unknown2> ReadSGAUnknown2s(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Unknown2> unknowns = [];
        for(ulong i = 0;i < count;i++)
            unknowns.Add(ReadSGAUnknown2(reader));
        return unknowns;
    }
    public static void Write(this Writer writer,IEnumerable<Unknown2> unknowns)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Unknown2 unknown in unknowns)
            Write(writer,unknown);
    }
}