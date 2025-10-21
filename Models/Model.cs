using System.Numerics;

namespace ThemModdingHerds.Models;
public class Model(IEnumerable<Vertex> vertices,IEnumerable<Face> faces)
{
    public List<Vertex> Vertices {get;set;} = [..vertices];
    public List<Face> Faces {get;set;} = [..faces];
    public Model(Model model): this(model.Vertices,model.Faces)
    {

    }
    public Model(): this([],[])
    {

    }
    public void Transform(Matrix4x4 matrix)
    {
        foreach(Vertex vertex in Vertices)
            vertex.Transform(matrix);
    }
    public void Rotate(Vector3 rot)
    {
        foreach(Vertex vertex in Vertices)
            vertex.Rotate(rot);
    }
    public void Offset(Vector4 offset)
    {
        foreach(Vertex vertex in Vertices)
            vertex.Offset(offset);
    }
    public void Offset(Vector3 offset) => Offset(new Vector4(offset,0));
    public void Offset(Vector2 offset) => Offset(new Vector4(offset,0,0));
    public bool Verify()
    {
        for(int i = 0;i < Faces.Count;i++)
        {
            Face face = Faces[i];
            Vertex? x = Vertices.ElementAtOrDefault(face.X);
            Vertex? y = Vertices.ElementAtOrDefault(face.Y);
            Vertex? z = Vertices.ElementAtOrDefault(face.Z);
            if(x == null)
            {
                Console.Error.WriteLine($"face@{i} x does not exist");
                return false;
            }
            if(y == null)
            {
                Console.Error.WriteLine($"face@{i} y does not exist");
                return false;
            }
            if(z == null)
            {
                Console.Error.WriteLine($"face@{i} z does not exist");
                return false;
            }
        }
        return true;
    }
    public byte[] ToBytes()
    {
        static byte[] Bytes(IEnumerable<byte[]> bytes)
        {
            byte[] data = [];
            foreach(byte[] a in bytes)
                data = [..data,..a];
            return data;
        }
        var vertices = Bytes(from vertex in Vertices select vertex.ToBytes());
        var faces = Bytes(from face in Faces select face.ToBytes());
        return [..vertices,..faces];
    }
}