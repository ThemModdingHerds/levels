using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class Unknown3
{
    public float A {get; set;}
    public float B {get; set;}
    public float C {get; set;}
    public float D {get; set;}
}
public static class Unknown3Ext
{
    public static Unknown3 ReadSGAUnknown3(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new()
        {
            A = reader.ReadFloat(),
            B = reader.ReadFloat(),
            C = reader.ReadFloat(),
            D = reader.ReadFloat()
        };
    }
    public static void Write(this Writer writer,Unknown3 unknown)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.Write(unknown.A);
        writer.Write(unknown.B);
        writer.Write(unknown.C);
        writer.Write(unknown.D);
    }
    public static List<Unknown3> ReadSGAUnknown3s(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Unknown3> unknowns = [];
        for(ulong i = 0;i < count;i++)
            unknowns.Add(ReadSGAUnknown3(reader));
        return unknowns;
    }
    public static void Write(this Writer writer,IEnumerable<Unknown3> unknowns)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Unknown3 unknown in unknowns)
            Write(writer,unknown);
    }
}