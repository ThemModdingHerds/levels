using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGI;

namespace ThemModdingHerds.Levels;
public class SGIFile : List<Element>
{
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
}
public static class SGIFileExt
{
    public static SGIFile ReadSGIFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        SGIFile file = new()
        {
            Version = reader.ReadPascal64String()
        };
        if(file.Version != SGIFile.VERSION)
            throw new Exception($"SGI version mismatch: got '{file.Version}', expected '{SGIFile.VERSION}'");
        file.AddRange(reader.ReadSGIElements(reader.ReadULong()));
        return file;
    }
    public static void Write(this Writer writer,SGIFile file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write((ulong)file.Count);
        writer.Write(file.ToArray());
    }
}