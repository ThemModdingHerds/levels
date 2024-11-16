using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGI;
public class Animation
{
    public string Name {get; set;} = string.Empty;
    public string FileName {get; set;} = string.Empty;
}
public static class AnimationExt
{
    public static Animation ReadSGIAnimation(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        return new Animation()
        {
            Name = reader.ReadPascal64String(),
            FileName = reader.ReadPascal64String()
        };
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