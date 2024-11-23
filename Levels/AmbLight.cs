using System.Drawing;
using System.Text.Json.Serialization;

namespace ThemModdingHerds.Levels;
public class AmbLight(ushort red,ushort green,ushort blue) : ILight, IColor
{
    [JsonPropertyName("type")]
    public string Type => "Amb";
    [JsonPropertyName("red")]
    public ushort Red {get; set;} = red;
    [JsonPropertyName("green")]
    public ushort Green {get; set;} = green;
    [JsonPropertyName("blue")]
    public ushort Blue {get; set;} = blue;
    [JsonIgnore]
    public Color Color {get => Color.FromArgb(0,Red,Green,Blue);set{Red = value.R;Green = value.G;Blue = value.B;}}
    public AmbLight(Color color): this(color.R,color.G,color.B)
    {

    }
    public AmbLight(): this(0,0,0)
    {

    }
    public override string ToString()
    {
        return $"Light: Amb {Red} {Green} {Blue}";
    }
    public static AmbLight Parse(string s)
    {
        ushort[] values = LevelParsers.ParseSameMultiple<ushort>(s,"Light: Amb",3);
        return new(values[0],values[1],values[2]);
    }
}