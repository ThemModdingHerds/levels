using System.Drawing;
using System.Numerics;
using ThemModdingHerds.Models.Utils;

namespace ThemModdingHerds.Models.Wavefront;
public class WavefrontVertex(Vector4 position,Color? color,Vector3? textureCoords,Vector3? normal) : Vertex(position,color,textureCoords,normal)
{
    public string PositionString {
        get
        {
            string result = $"v {Strings.FloatString(Position.X)} {Strings.FloatString(Position.Y)} {Strings.FloatString(Position.Z)}";
            if(Position.W != 0) result += $" {Strings.FloatString(Position.W)}";
            if(Color != null && !Colors.IsWhite(Color.Value)) result += $" {Strings.FloatString(Color.Value.R/255.0f)} {Strings.FloatString(Color.Value.G/255.0f)} {Strings.FloatString(Color.Value.B/255.0f)}";
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
                Color = System.Drawing.Color.FromArgb(255,(byte)(float.Parse(parts[3])*255),(byte)(float.Parse(parts[4])*255),(byte)(float.Parse(parts[5])*255));
            if(parts.Length == 7)
            {
                Position = new(float.Parse(parts[0]),float.Parse(parts[1]),float.Parse(parts[2]),float.Parse(parts[3]));
                Color = System.Drawing.Color.FromArgb(255,(byte)(float.Parse(parts[4])*255),(byte)(float.Parse(parts[5])*255),(byte)(float.Parse(parts[6])*255));
            }
        }
    }
    public string UVString {
        get
        {
            if(!UV.HasValue) throw new Exception("no texture coords");
            string result = $"vt {Strings.FloatString(UV.Value.X)} {Strings.FloatString(UV.Value.Y)}";
            if(UV.Value.Z != 0 || !float.IsNaN(UV.Value.Z)) result += $" {Strings.FloatString(UV.Value.Z)}";
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
    public WavefrontVertex(Vector4 position): this(position,null,null,null)
    {

    }
    public WavefrontVertex(Vector3 position): this(new Vector4(position.X,position.Y,position.Z,0))
    {

    }
    public WavefrontVertex(): this(Vector3.Zero)
    {

    }
}