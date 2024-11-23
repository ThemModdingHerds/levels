using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace ThemModdingHerds.Levels;
public class DirLight(ushort red,ushort green,ushort blue,float x,float y,float z) : ILight, IColor
{
    [JsonPropertyName("type")]
    public string Type => "Dir";
    [JsonPropertyName("red")]
    public ushort Red {get; set;} = red;
    [JsonPropertyName("green")]
    public ushort Green {get; set;} = green;
    [JsonPropertyName("blue")]
    public ushort Blue {get; set;} = blue;
    [JsonPropertyName("x")]
    public float X {get;set;} = x;
    [JsonPropertyName("y")]
    public float Y {get;set;} = y;
    [JsonPropertyName("z")]
    public float Z {get;set;} = z;
    [JsonIgnore]
    public Vector3 Position {get => new(X,Y,Z);set{X = value[0];Y = value[1];Z = value[2];}}
    [JsonIgnore]
    public Color Color {get => Color.FromArgb(0,Red,Green,Blue);set{Red = value.R;Green = value.G;Blue = value.B;}}
    public DirLight(Color color,float x,float y,float z): this(color.R,color.G,color.B,x,y,z)
    {

    }
    public DirLight(ushort red,ushort green,ushort blue,Vector3 position): this(red,green,blue,position[0],position[1],position[3])
    {

    }
    public DirLight(Color color,Vector3 position): this(color.R,color.G,color.B,position)
    {

    }
    public DirLight(): this(0,0,0,0,0,0)
    {

    }
    public override string ToString()
    {
        float red = (float)Red/0xff;
        float green = (float)Green/0xff;
        float blue = (float)Blue/0xff;
        return $"Light: Dir {red} {green} {blue} {X} {Y} {Z}";
    }
}