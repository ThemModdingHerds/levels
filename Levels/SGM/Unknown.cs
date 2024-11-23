using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGM;
public class Unknown(float[] data)
{
    public const int DATA_SIZE = 13;
    public float[] Data {get; set;} = data;
    public Unknown(): this([0,0,0,1,1,1,1,1,1,0,0,0,90])
    {

    }
}
public static class UnknownExt
{
    public static Unknown ReadSGMUnknown(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        float[] data = new float[Unknown.DATA_SIZE];
        for(int i = 0;i < Unknown.DATA_SIZE;i++)
            data[i] = reader.ReadFloat();
        return new(data);
    }
    public static void Write(this Writer writer,Unknown unknown)
    {
        writer.Endianness = IO.Endianness.Big;
        if(unknown.Data.Length != Unknown.DATA_SIZE)
            throw new Exception($"expected {Unknown.DATA_SIZE}, got {unknown.Data.Length}");
        foreach(float f in unknown.Data)
            writer.Write(f);
    }
}