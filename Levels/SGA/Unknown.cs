using System.Text;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGA;
public class Unknown(byte[] data)
{
    public const int DATA_SIZE = 4;
    public byte[] Data {get; set;} = data;
    public Unknown(): this(new byte[DATA_SIZE])
    {

    }
}
public static class UnknownExt
{
    public static Unknown ReadSGAUnknown(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        byte[] data = reader.ReadBytes(Unknown.DATA_SIZE);
        return new(data);
    }
    public static void Write(this Writer writer,Unknown unknown)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.Write(unknown.Data);
    }
    public static List<Unknown> ReadSGAUnknowns(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Unknown> unknowns = [];
        for(ulong i = 0;i < count;i++)
            unknowns.Add(ReadSGAUnknown(reader));
        return unknowns;
    }
    public static void Write(this Writer writer,IEnumerable<Unknown> unknowns)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Unknown unknown in unknowns)
            Write(writer,unknown);
    }
}