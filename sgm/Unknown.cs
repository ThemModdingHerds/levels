using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGM;
public class Unknown
{
    public float[] Data {get; set;} = new float[13];
}
public static class UnknownExt
{
    public static Unknown ReadSGMUnknown(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        float[] data = new float[13];
        for(int i = 0;i < 13;i++)
            data[i] = reader.ReadFloat();
        return new()
        {
            Data = data
        };
    }
    public static void Write(this Writer writer,Unknown unknown)
    {
        writer.Endianness = IO.Endianness.Big;
        foreach(float f in unknown.Data)
            writer.Write(f);
    }
}