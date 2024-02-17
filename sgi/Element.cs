using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.Common;

namespace ThemModdingHerds.Levels.SGI;
public class Element : List<Animation>
{
    public string Name {get; set;} = string.Empty;
    public string Shape {get; set;} = string.Empty;
    public float[] Matrix {get; set;} = new float[16];
    public byte IsVisible {get; set;}
    // TODO: find out what this is
    public byte Unknown {get; set;}
}
public static class ElementExt
{
    public static Element ReadSGIElement(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        Element elm = new()
        {
            Name = reader.ReadPascal64String(),
            Shape = reader.ReadPascal64String(),
            Matrix = reader.ReadMatrix(),
            IsVisible = reader.ReadByte(),
            Unknown = reader.ReadByte()
        };
        elm.AddRange(reader.ReadSGIAnimations(reader.ReadULong()));
        return elm;
    }
    public static void Write(this Writer writer,Element element)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(element.Name);
        writer.WritePascal64String(element.Shape);
        writer.WriteMatrix(element.Matrix);
        writer.Write(element.IsVisible);
        writer.Write(element.Unknown);
        writer.Write((ulong)element.Count);
        writer.Write(element.ToArray());
    }
    public static List<Element> ReadSGIElements(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Element> elms = [];
        for(ulong i = 0;i < count;i++)
            elms.Add(reader.ReadSGIElement());
        return elms;
    }
    public static void Write(this Writer writer,IEnumerable<Element> elements)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Element elm in elements)
            writer.Write(elm);
    }
}