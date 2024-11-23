using System.Numerics;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGI;
public class Element(string name,string shape,Matrix4x4 matrix,byte isVisible,byte unknown,IEnumerable<Animation> animations)
{
    public string Name {get; set;} = name;
    public string Shape {get; set;} = shape;
    public Matrix4x4 Matrix {get; set;} = matrix;
    public byte IsVisible {get; set;} = isVisible;
    // TODO: find out what this is
    public byte Unknown {get; set;} = unknown;
    public List<Animation> Animations {get;set;} = [..animations];
    public Element(): this(string.Empty,string.Empty,Matrix4x4.Identity,0,0,[])
    {

    }
    public override string ToString()
    {
        return $"{Shape}:{Name}:{Animations.Count}";
    }
}
public static class ElementExt
{
    public static Element ReadSGIElement(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string name = reader.ReadPascal64String();
        string shape = reader.ReadPascal64String();
        Matrix4x4 matrix = reader.ReadMatrix4x4();
        byte isVisible = reader.ReadByte();
        byte unknown = reader.ReadByte();
        ulong animationsCount = reader.ReadULong();
        List<Animation> animations = reader.ReadSGIAnimations(animationsCount);
        return new(name,shape,matrix,isVisible,unknown,animations);
    }
    public static void Write(this Writer writer,Element element)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(element.Name);
        writer.WritePascal64String(element.Shape);
        writer.WriteMatrix4x4(element.Matrix);
        writer.Write(element.IsVisible);
        writer.Write(element.Unknown);
        writer.Write((ulong)element.Animations.Count);
        writer.Write(element.Animations);
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