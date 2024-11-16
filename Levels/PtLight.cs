using System.Drawing;
using System.Numerics;
using System.Text.Json.Serialization;

namespace ThemModdingHerds.Levels;
public class PtLight(byte red,byte green,byte blue,int x1,int y1,int x2,int y2) : ILight, IColor
{
    [JsonPropertyName("type")]
    public string Type => "Pt";
    [JsonPropertyName("red")]
    public byte Red {get; set;} = red;
    [JsonPropertyName("green")]
    public byte Green {get; set;} = green;
    [JsonPropertyName("blue")]
    public byte Blue {get; set;} = blue;
    [JsonPropertyName("x1")]
    public int X1 {get; set;} = x1;
    [JsonPropertyName("y1")]
    public int Y1 {get; set;} = y1;
    [JsonIgnore]
    public Vector2 From {get => new(X1,Y1);set{X1 = (int)value[0];Y1 = (int)value[1];}}
    [JsonPropertyName("x2")]
    public int X2 {get; set;} = x2;
    [JsonPropertyName("y2")]
    public int Y2 {get; set;} = y2;
    [JsonIgnore]
    public Vector2 To {get => new(X2,Y2);set{X2 = (int)value[0];Y2 = (int)value[1];}}
    [JsonIgnore]
    public Color Color {get => Color.FromArgb(0,Red,Green,Blue);set{Red = value.R;Green = value.G;Blue = value.B;}}
    public PtLight(Color color,int x1,int y1,int x2,int y2): this(color.R,color.G,color.B,x1,y1,x2,y2)
    {

    }
    public PtLight(byte red,byte green,byte blue,Vector2 from,Vector2 to): this(red,green,blue,(int)from.X,(int)from.Y,(int)to.X,(int)to.Y)
    {

    }
    public PtLight(Color color,Vector2 from,Vector2 to): this(color.R,color.G,color.B,from,to)
    {

    }
    public PtLight(): this(0,0,0,0,0,0,0)
    {

    }
    public override string ToString()
    {
        return $"Light: Pt {Red} {Green} {Blue} {X1} {Y1} {X2} {Y2}";
    }
    public static PtLight Parse(string s)
    {
        int[] values = LevelParsers.ParseSameMultiple<int>(s,"Light: Pt",7);
        return new((byte)values[0],(byte)values[1],(byte)values[2],values[3],values[4],values[5],values[6]);
    }
}