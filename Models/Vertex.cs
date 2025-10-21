using System.Drawing;
using System.Numerics;

namespace ThemModdingHerds.Models;
public class Vertex(Vector4 position,Color? color,Vector3? uv,Vector3? normal)
{
    public Vector4 Position {get;set;} = position;
    public Color? Color {get;set;} = color;
    public Vector3? UV {get;set;} = uv;
    public Vector3? Normal {get;set;} = normal;
    public Vertex(Vector4 position): this(position,null,null,null)
    {
        
    }
    public Vertex(): this(Vector4.Zero)
    {
        
    }
    public override string ToString()
    {
        return $"{Position}";
    }
    public void Transform(Matrix4x4 matrix)
    {
        Position = Vector4.Transform(Position,matrix);
    }
    public void Offset(Vector4 offset)
    {
        Position += offset;
    }
    public void Rotate(Vector3 rot)
    {
        Matrix4x4 x = Matrix4x4.CreateRotationX(rot.X);
        Matrix4x4 y = Matrix4x4.CreateRotationY(rot.Y);
        Matrix4x4 z = Matrix4x4.CreateRotationZ(rot.Z);
        Transform(x * y * z);
    }
    public void Offset(Vector3 offset) => Offset(new Vector4(offset,0));
    public void Offset(Vector2 offset) => Offset(new Vector4(offset,0,0));
    public const int BYTE_SIZE = 36;
    public byte[] ToBytes()
    {
        byte[] p = [
            ..BitConverter.GetBytes(Position.X),
            ..BitConverter.GetBytes(Position.Y),
            ..BitConverter.GetBytes(Position.Z)
        ];
        byte[] n = [
            ..BitConverter.GetBytes(Normal?.X ?? 0.5f),
            ..BitConverter.GetBytes(Normal?.Y ?? 0.5f),
            ..BitConverter.GetBytes(Normal?.Z ?? 1.0f)
        ];
        byte[] uv = [
            ..BitConverter.GetBytes(UV?.X ?? 0.0f),
            ..BitConverter.GetBytes(UV?.Y ?? 0.0f)
        ];
        byte red = Color.HasValue ? Color.Value.R : (byte)0xff;
        byte green = Color.HasValue ? Color.Value.G : (byte)0xff;
        byte blue = Color.HasValue ? Color.Value.B : (byte)0xff;
        byte alpha = Color.HasValue ? Color.Value.A : (byte)0xff;
        byte[] c = [
            red,
            green,
            blue,
            alpha
        ];
        return [..p,..n,..uv,..c];
    }
}