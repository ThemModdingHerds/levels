using System.Drawing;
using System.Numerics;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGM;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels.Models.Wavefront;
public class Vertex(Vector4 position,Color? color,Vector3? textureCoords,Vector3? normal) : IVertex
{
    public Vector4 Position {get;set;} = position;
    public Color? Color {get;set;} = color;
    public string PositionString {
        get
        {
            string result = $"v {Strings.FloatString(Position.X)} {Strings.FloatString(Position.Y)} {Strings.FloatString(Position.Z)}";
            if(Position.W != 0) result += $" {Position.W}";
            if(Color != null) result += $" {Color.Value.R/255.0f} {Color.Value.G/255.0f} {Color.Value.B/255.0f}";
            return result;
        }
        set
        {
            if(!value.StartsWith("v ")) throw new Exception("not starting with v");
            string[] parts = Strings.RemoveComments(value[2..]).Trim().Split(' ');
            Position = new(float.Parse(parts[0]),float.Parse(parts[1]),float.Parse(parts[2]),0);
            if(parts.Length == 4)
                Position = new(float.Parse(parts[0]),float.Parse(parts[1]),float.Parse(parts[2]),float.Parse(parts[3]));
            if(parts.Length == 6)
                Color = System.Drawing.Color.FromArgb(0,(byte)(float.Parse(parts[3])*255),(byte)(float.Parse(parts[4])*255),(byte)(float.Parse(parts[5])*255));
            if(parts.Length == 7)
            {
                Position = new(float.Parse(parts[0]),float.Parse(parts[1]),float.Parse(parts[2]),float.Parse(parts[3]));
                Color = System.Drawing.Color.FromArgb(0,(byte)(float.Parse(parts[4])*255),(byte)(float.Parse(parts[5])*255),(byte)(float.Parse(parts[6])*255));
            }
        }
    }
    public Vector3? UV {get;set;} = textureCoords;
    public string UVString {
        get
        {
            if(!UV.HasValue) throw new Exception("no texture coords");
            string result = $"vt {Strings.FloatString(UV.Value.X)} {Strings.FloatString(UV.Value.Y)}";
            if(UV.Value.Z != 0 || !float.IsNaN(UV.Value.Z)) result += $" {UV.Value.Z}";
            return result;
        }
        set
        {
            if(!value.StartsWith("vt ")) throw new Exception("not starting with vt");
            string[] parts = Strings.RemoveComments(value[3..]).Trim().Split(' ');
            UV = new(float.Parse(parts[0]),float.Parse(parts[1]),0);
            if(parts.Length == 3)
                UV = new(float.Parse(parts[0]),float.Parse(parts[1]),float.Parse(parts[2]));
        }
    }
    public Vector3? Normal {get;set;} = normal;
    public string NormalString {
        get
        {
            if(!Normal.HasValue) throw new Exception("no normal");
            float length = Normal.Value.LengthSquared();
            float x = Normal.Value.X/length;
            float y = Normal.Value.Y/length;
            float z = Normal.Value.Z/length;
            return $"vn {Strings.FloatString(x)} {Strings.FloatString(y)} {Strings.FloatString(z)}";
        }
        set
        {
            if(!value.StartsWith("vn ")) throw new Exception("not starting with vn");
            string[] parts = Strings.RemoveComments(value[3..]).Trim().Split(' ');
            Normal = new(float.Parse(parts[0]),float.Parse(parts[1]),float.Parse(parts[2]));
        }
    }
    public Vertex(Vector4 position): this(position,null,null,null)
    {

    }
    public Vertex(Vector3 position): this(new Vector4(position.X,position.Y,position.Z,0))
    {

    }
    public Vertex(): this(Vector3.Zero)
    {

    }
    public static List<Vertex> FromSGM(SGMFile sgm)
    {
        List<Vertex> vertices = [];
        // make some kind of parser for this shit :skull:
        if(sgm.Format == "float p[3],n[3],uv[2]; uchar4 c;")
        {
            foreach(byte[] polygon in sgm.Polygones)
            {
                Reader reader = new(polygon);
                reader.Endianness = IO.Endianness.Big;
                Vector3 p = reader.ReadVector3();
                Vector3 n = reader.ReadVector3();
                Vector2 uv = reader.ReadVector2();
                byte[] c = reader.ReadBytes(4);
                reader.Close();
                Color color = System.Drawing.Color.FromArgb(c[3],c[0],c[1],c[2]);
                Vertex vertex = new(new Vector4(p,0),color,n,new Vector3(uv,0));
                vertices.Add(vertex);
            }
        }
        if(sgm.Format == "float p[3],n[3],uv[2]; uchar4 c; uchar4 j[4],jw[4];")
        {
            foreach(byte[] polygon in sgm.Polygones)
            {
                Reader reader = new(polygon);
                reader.Endianness = IO.Endianness.Big;
                Vector3 p = reader.ReadVector3(); // float p[3]
                Vector3 n = reader.ReadVector3(); // float n[3]
                Vector2 uv = reader.ReadVector2(); // float uv[2]
                byte[] c = reader.ReadBytes(4); // uchar4 c
                // next are joints and joint weights
                reader.Close();
                Color color = System.Drawing.Color.FromArgb(c[3],c[0],c[1],c[2]);
                Vertex vertex = new(new Vector4(p,0),color,n,new Vector3(uv,0));
                vertices.Add(vertex);
            }
        }
        return vertices;
    }
}