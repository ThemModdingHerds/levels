using System.Drawing;
using System.Numerics;

namespace ThemModdingHerds.Levels.Models;
public interface IVertex
{
    public Vector4 Position {get;set;}
    public Color? Color {get;set;}
    public Vector3? UV {get;set;}
    public Vector3? Normal {get;set;}
}