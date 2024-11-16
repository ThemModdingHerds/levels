using System.Drawing;

namespace ThemModdingHerds.Levels;
public interface IColor
{
    public byte Red {get; set;}
    public byte Green {get; set;}
    public byte Blue {get; set;}
    public Color Color {get;set;}
}