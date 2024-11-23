using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGI;

namespace ThemModdingHerds.Levels.SGI;
public class SkullGirlsIndex(IEnumerable<Element> elements)
{
    public const string VERSION = "2.0";
    public const string FILENAME = "background.sgi.msb";
    public string Version {get; set;} = VERSION;
    public List<Element> Elements {get;set;} = [..elements];
    public SkullGirlsIndex(): this([])
    {

    }
    public override string ToString()
    {
        return FILENAME;
    }
}
public static class SkullGirlsIndexExt
{
    public static SkullGirlsIndex ReadSGIFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        List<Element> elements = [];
        if(version != SkullGirlsIndex.VERSION)
            throw new Exception($"SGI version mismatch: got '{version}', expected '{SkullGirlsIndex.VERSION}'");
        elements.AddRange(reader.ReadSGIElements(reader.ReadULong()));
        return new(elements);
    }
    public static void Write(this Writer writer,SkullGirlsIndex file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write((ulong)file.Elements.Count);
        writer.Write(file.Elements);
    }
}