using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGS;

namespace ThemModdingHerds.Levels;
public class SGSFile : List<Bone>
{
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
}
public static class SGSFileExt
{
    public static SGSFile ReadSGSFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        SGSFile file = new()
        {
            Version = reader.ReadPascal64String()
        };
        if(file.Version != SGSFile.VERSION)
            throw new Exception($"SGS version mismatch: got '{file.Version}', expected '{SGSFile.VERSION}'");
        file.AddRange(reader.ReadSGSBones(reader.ReadULong()));
        return file;
    }
    public static void Write(this Writer writer,SGSFile file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.Write((ulong)file.Count);
        writer.Write(file.ToArray());
    }
}