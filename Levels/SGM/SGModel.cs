using System.Drawing;
using System.Numerics;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Models;

namespace ThemModdingHerds.Levels.SGM;
public class SGModel(string textureName,Unknown unknown,string format,ulong formatSize,IEnumerable<byte[]> verticesData,IEnumerable<Triangle> triangles,Vector3 pos,Vector3 rot,IEnumerable<string> bones,IEnumerable<Matrix4x4> boneProperties)
{
    public const string DEFAULT_FORMAT = "float p[3],n[3],uv[2]; uchar4 c;";
    public const string DEFAULT_FORMAT_EXT = $"{DEFAULT_FORMAT} uchar4 j[4],jw[4];";
    public const string VERSION = "2.0";
    public string Version {get; set;} = VERSION;
    public string TextureName {get; set;} = textureName;
    public Unknown Unknown {get; set;} = unknown;
    public string Format {get; set;} = format;
    public ulong FormatSize {get; set;} = formatSize;
    public List<byte[]> VerticesData {get; set;} = [..verticesData];
    public List<Triangle> Triangles {get; set;} = [..triangles];
    public Vector3 Pos {get;set;} = pos;
    public Vector3 Rot {get;set;} = rot;
    public List<string> Bones {get; set;} = [..bones];
    public List<Matrix4x4> BoneProperties {get; set;} = [..boneProperties];
    public SGModel(string textureName,Unknown unknown,string format,ulong formatSize,Vector3 pos,Vector3 rot): this(textureName,unknown,format,formatSize,[],[],pos,rot,[],[])
    {

    }
    public SGModel(): this(string.Empty,new(),string.Empty,0,Vector3.Zero,Vector3.Zero)
    {

    }
    public Matrix4x4 GetBoneProperties(string name)
    {
        for(int i = 0;i < Bones.Count;i++)
        {
            string bone = Bones[i];
            if(bone == name)
                return BoneProperties[i];
        }
        throw new Exception($"this model has no bone named '{name}'");
    }
    public List<Vertex> GetVertices()
    {
        List<Vertex> vertices = [];
        foreach(byte[] vertex in VerticesData)
        {
            Reader reader = new(vertex)
            {
                Endianness = IO.Endianness.Big
            };
            Vector3 p = reader.ReadVector3();
            Vector3 n = reader.ReadVector3();
            Vector2 uv = reader.ReadVector2();
            byte[] c = reader.ReadBytes(4);
            reader.Close();
            Color color = Color.FromArgb(c[3],c[0],c[1],c[2]);
            Vertex v = new(new(p,0),color,new(uv,0),n);
            vertices.Add(v);
        }
        return vertices;
    }
    public Model GetModel()
    {
        return new(GetVertices(),from triangle in Triangles select triangle.GetFace());
    }
    public WavefrontModel ToWavefrontModel()
    {
        return new(GetModel());
    }
    public Models.MTL.Material CreateMTLMaterial(string name,string texFolder)
    {
        return new(name,$"{texFolder}/{TextureName}.dds");
    }
}
public static class SGModelExt
{
    public static SGModel ReadSGMFile(this Reader reader)
    {
        reader.Endianness = IO.Endianness.Big;
        string version = reader.ReadPascal64String();
        string textureName = reader.ReadPascal64String();
        Unknown unknown = reader.ReadSGMUnknown();
        string format = reader.ReadPascal64String();
        ulong formatSize = reader.ReadULong();
        if(version != SGModel.VERSION)
            throw new Exception($"SGM version mismatch: got '{version}', expected '{SGModel.VERSION}'");
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
    public static void Write(this Writer writer,SGModel file)
    {
        writer.Endianness = IO.Endianness.Big;
        writer.WritePascal64String(file.Version);
        writer.WritePascal64String(file.TextureName);
        writer.Write(file.Unknown);
        writer.WritePascal64String(file.Format);
        writer.Write(file.FormatSize);
        writer.Write((ulong)file.VerticesData.Count);
        writer.Write((ulong)file.Triangles.Count);
        if(file.Bones.Count != file.BoneProperties.Count)
            throw new Exception($"Bones and BoneProperties don't have the same count, Bones is {file.Bones.Count} but BoneProperties is {file.BoneProperties.Count}");
        writer.Write((ulong)file.Bones.Count);
        foreach(byte[] data in file.VerticesData)
            writer.Write(data);
        writer.Write(file.Triangles);
        writer.WriteVector3(file.Pos);
        writer.WriteVector3(file.Rot);
        writer.WritePascal64Strings(file.Bones);
        writer.WriteMatrices4x4(file.BoneProperties);
    }
}