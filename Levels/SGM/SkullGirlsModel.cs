using System.Drawing;
using System.Numerics;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.SGM;
public class SkullGirlsModel(string textureName,Unknown unknown,string format,ulong formatSize,IEnumerable<byte[]> polygons,IEnumerable<Triangle> triangles,Vector3 pos,Vector3 rot,IEnumerable<string> bones,IEnumerable<Matrix4x4> boneProperties)
{
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
    public string TextureName {get; set;} = textureName;
    public Unknown Unknown {get; set;} = unknown;
    public string Format {get; set;} = format;
    public ulong FormatSize {get; set;} = formatSize;
    public List<byte[]> Polygons {get; set;} = [..polygons];
    public List<Triangle> Triangles {get; set;} = [..triangles];
    public Vector3 Pos {get;set;} = pos;
    public Vector3 Rot {get;set;} = rot;
    public List<string> Bones {get; set;} = [..bones];
    public List<Matrix4x4> BoneProperties {get; set;} = [..boneProperties];
    public SkullGirlsModel(string textureName,Unknown unknown,string format,ulong formatSize,Vector3 pos,Vector3 rot): this(textureName,unknown,format,formatSize,[],[],pos,rot,[],[])
    {

    }
    public SkullGirlsModel(): this(string.Empty,new(),string.Empty,0,Vector3.Zero,Vector3.Zero)
    {

    }
}
public static class SkullGirlsModelExt
{
    public static SkullGirlsModel ReadSGMFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        string textureName = reader.ReadPascal64String();
        Unknown unknown = reader.ReadSGMUnknown();
        string format = reader.ReadPascal64String();
        ulong formatSize = reader.ReadULong();
        if(version != SkullGirlsModel.VERSION)
            throw new Exception($"SGM version mismatch: got '{version}', expected '{SkullGirlsModel.VERSION}'");
        ulong polygonsC = reader.ReadULong();
        ulong trianglesC = reader.ReadULong();
        ulong bonesC = reader.ReadULong();
        List<byte[]> polygons = [];
        for(ulong i = 0;i < polygonsC;i++)
        {
            byte[] data = reader.ReadBytes((int)formatSize);
            polygons.Add(data);
        }
        List<Triangle> triangles = reader.ReadSGMTriangles(trianglesC);
        Vector3 pos = reader.ReadVector3();
        Vector3 rot = reader.ReadVector3();
        List<string> bones = reader.ReadPascal64Strings(bonesC);
        List<Matrix4x4> boneProperties = reader.ReadMatrices4x4(bonesC);
        return new(textureName,unknown,format,formatSize,polygons,triangles,pos,rot,bones,boneProperties);
    }
    public static void Write(this Writer writer,SkullGirlsModel file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.WritePascal64String(file.TextureName);
        writer.Write(file.Unknown);
        writer.WritePascal64String(file.Format);
        writer.Write(file.FormatSize);
        writer.Write((ulong)file.Polygons.Count);
        writer.Write((ulong)file.Triangles.Count);
        if(file.Bones.Count != file.BoneProperties.Count)
            throw new Exception($"Bones and BoneProperties don't have the same count, Bones is {file.Bones.Count} but BoneProperties is {file.BoneProperties.Count}");
        writer.Write((ulong)file.Bones.Count);
        foreach(byte[] data in file.Polygons)
            writer.Write(data);
        writer.Write(file.Triangles);
        writer.WriteVector3(file.Pos);
        writer.WriteVector3(file.Rot);
        writer.WritePascal64Strings(file.Bones);
        writer.WriteMatrices4x4(file.BoneProperties);
    }
}