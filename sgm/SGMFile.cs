using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGM;
using ThemModdingHerds.Levels.Common;

namespace ThemModdingHerds.Levels;
public class SGMFile
{
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
    public string TextureName {get; set;} = string.Empty;
    public Unknown Unknown {get; set;} = new();
    public string Format {get; set;} = string.Empty;
    public ulong FormatSize {get; set;}
    public List<byte[]> Polygones {get; set;} = [];
    public List<Triangle> Triangles {get; set;} = [];
    public float XPos {get; set;}
    public float YPos {get; set;}
    public float ZPos {get; set;}
    public float XRot {get; set;}
    public float YRot {get; set;}
    public float ZRot {get; set;}
    public List<string> Bones {get; set;} = [];
    public List<float[]> BoneProperties {get; set;} = [];
}
public static class SGMFileExt
{
    public static SGMFile ReadSGMFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        SGMFile file = new()
        {
            Version = reader.ReadPascal64String(),
            TextureName = reader.ReadPascal64String(),
            Unknown = reader.ReadSGMUnknown(),
            Format = reader.ReadPascal64String(),
            FormatSize = reader.ReadULong()
        };
        if(file.Version != SGMFile.VERSION)
            throw new Exception($"SGM version mismatch: got '{file.Version}', expected '{SGMFile.VERSION}'");
        ulong polygones = reader.ReadULong();
        ulong triangles = reader.ReadULong();
        ulong bones = reader.ReadULong();
        for(ulong i = 0;i < polygones;i++)
        {
            byte[] data = new byte[file.FormatSize];
            for(ulong j = 0;j < file.FormatSize;j++)
                data[j] = reader.ReadByte();
            file.Polygones.Add(data);
        }
        file.Triangles = reader.ReadSGMTriangles(triangles);
        file.XPos = reader.ReadFloat();
        file.YPos = reader.ReadFloat();
        file.ZPos = reader.ReadFloat();
        file.XRot = reader.ReadFloat();
        file.YRot = reader.ReadFloat();
        file.ZRot = reader.ReadFloat();
        file.Bones = reader.ReadPascal64Strings(bones);
        file.BoneProperties = reader.ReadMatrices(bones);
        return file;
    }
    public static void Write(this Writer writer,SGMFile file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.WritePascal64String(file.TextureName);
        writer.Write(file.Unknown);
        writer.WritePascal64String(file.Format);
        writer.Write(file.FormatSize);
        writer.Write((ulong)file.Polygones.Count);
        writer.Write((ulong)file.Triangles.Count);
        if(file.Bones.Count != file.BoneProperties.Count)
            throw new Exception($"Bones and BoneProperties don't have the same count, Bones is {file.Bones.Count} but BoneProperties is {file.BoneProperties.Count}");
        writer.Write((ulong)file.Bones.Count);
        foreach(byte[] data in file.Polygones)
            writer.Write(data);
        writer.Write(file.Triangles);
        writer.Write(file.XPos);
        writer.Write(file.YPos);
        writer.Write(file.ZPos);
        writer.Write(file.XRot);
        writer.Write(file.YRot);
        writer.Write(file.ZRot);
        writer.WritePascal64Strings(file.Bones);
        writer.WriteMatrices(file.BoneProperties);
    }
}