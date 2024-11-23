using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGI;
public class Animation(string name,string filename)
{
    public string Name {get; set;} = name;
    public string FileName {get; set;} = filename;
    public Animation(): this(string.Empty,string.Empty)
    {

    }
    public override string ToString()
    {
        return $"{Name} -> {FileName}";
    }
}
public static class AnimationExt
{
    public static Animation ReadSGIAnimation(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string name = reader.ReadPascal64String();
        string filename = reader.ReadPascal64String();
        return new(name,filename);
    }
    public static void Write(this Writer writer,Animation animation)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(animation.Name);
        writer.WritePascal64String(animation.FileName);
    }
    public static List<Animation> ReadSGIAnimations(this Reader reader,ulong count)
    {
        reader.Endianness = IO.Endianness.Big;
        List<Animation> animations = [];
        for(ulong index = 0;index < count;index++)
            animations.Add(ReadSGIAnimation(reader));
        return animations;
    }
    public static void Write(this Writer writer,IEnumerable<Animation> animations)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(Animation animation in animations)
            writer.Write(animation);
    }
}