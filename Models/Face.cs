namespace ThemModdingHerds.Models;
public class Face(int x,int y,int z,int? w)
{
    public int X {get;set;} = x;
    public int Y {get;set;} = y;
    public int Z {get;set;} = z;
    public int? W {get;set;} = w;
    public int? UVX {get;set;}
    public int? UVY {get;set;}
    public int? UVZ {get;set;}
    public int? UVW {get;set;}
    public int? NX {get;set;}
    public int? NY {get;set;}
    public int? NZ {get;set;}
    public int? NW {get;set;}
    public static Face Create(int x,int y,int z,int? w)
    {
        return new(x,y,z,w)
        {
            UVX = x,
            UVY = y,
            UVZ = z,
            UVW = w,
            NX = x,
            NY = y,
            NZ = z,
            NW = w,
        };
    }
    public static Face Create(int x,int y,int z) => Create(x,y,z,null);
    public Face(int x,int y,int z): this(x,y,z,null)
    {

    }
    public static List<Face> FromVertices(IEnumerable<Vertex> vertices)
    {
        List<Face> faces = [];
        for(int i = 0;i < vertices.Count();i += 3)
        {
            faces.Add(new(i,i+1,i+2));
        }
        return faces;
    }
    public override string ToString()
    {
        return $"[{X},{Y},{Z}]";
    }
    public void Reverse()
    {
        (Z, X) = (X, Z);
    }
    public void SetNaive()
    {
        UVX = X;
        UVY = Y;
        UVZ = Z;
        UVW = W;
        NX = X;
        NY = Y;
        NZ = Z;
        NW = W;
    }
    public const int BYTE_SIZE = 12;
    public byte[] ToBytes()
    {
        return [
            ..BitConverter.GetBytes(X),
            ..BitConverter.GetBytes(Y),
            ..BitConverter.GetBytes(Z)
        ];
    }
}