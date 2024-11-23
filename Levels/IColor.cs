using System.Drawing;

namespace ThemModdingHerds.Levels;
public interface IColor
{
    public ushort Red {get; set;}
    public ushort Green {get; set;}
    public ushort Blue {get; set;}
    public Color Color {get;set;}
}