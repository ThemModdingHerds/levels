using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGI;

namespace ThemModdingHerds.Levels.SGI;
public class SGIFile(IEnumerable<Element> elements)
{
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
    public List<Element> Elements {get;set;} = [..elements];
    public SGIFile(): this([])
    {

    }
}
public static class SGIFileExt
{
    public static SGIFile ReadSGIFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        List<Element> elements = [];
        if(version != SGIFile.VERSION)
            throw new Exception($"SGI version mismatch: got '{version}', expected '{SGIFile.VERSION}'");
        elements.AddRange(reader.ReadSGIElements(reader.ReadULong()));
        return new(elements);
    }
    public static void Write(this Writer writer,SGIFile file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write((ulong)file.Elements.Count);
        writer.Write(file.Elements);
    }
}