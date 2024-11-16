using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class Unknown4
{
    public float A {get; set;}
    public float B {get; set;}
    public float C {get; set;}
}
public static class Unknown4Ext
{
    public static Unknown4 ReadSGAUnknown4(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            A = reader.ReadFloat(),
            B = reader.ReadFloat(),
            C = reader.ReadFloat()
        };
    }
    public static void Write(this Writer writer,Unknown4 unknown)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.Write(unknown.A);
        writer.Write(unknown.B);
        writer.Write(unknown.C);
    }
    public static List<Unknown4> ReadSGAUnknown4s(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Unknown4> unknowns = [];
        for(ulong i = 0;i < count;i++)
            unknowns.Add(ReadSGAUnknown4(reader));
        return unknowns;
    }
    public static void Write(this Writer writer,IEnumerable<Unknown4> unknowns)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Unknown4 unknown in unknowns)
            Write(writer,unknown);
    }
}