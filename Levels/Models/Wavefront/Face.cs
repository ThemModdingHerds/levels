using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using ThemModdingHerds.Levels.SGM;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels.Models.Wavefront;
public class Face(int x,int y,int z,int? tx,int? ty,int? tz,int? nx,int? ny,int? nz) : IParsable<Face>, IFace
{
    public int X {get;set;} = x;
    public int Y {get;set;} = y;
    public int Z {get;set;} = z;
    public int? XT {get;set;} = tx;
    public int? YT {get;set;} = ty;
    public int? ZT {get;set;} = tz;
    public int? XN {get;set;} = nx;
    public int? YN {get;set;} = ny;
    public int? ZN {get;set;} = nz;
    public Face(int x,int y,int z): this(x,y,z,null,null,null,null,null,null)
    {

    }
    public static Face Parse(string s,IFormatProvider? provider)
    {
        if(!s.StartsWith("f ")) throw new Exception("does not start with f");
        string[] parts = Strings.RemoveComments(s[2..]).Trim().Split(' ');
        string sx = parts[0];string sy = parts[1];string sz = parts[2];
        string[] xp = sx.Split('/');string[] yp = sy.Split('/');string[] zp = sz.Split('/');
        int x = int.Parse(xp[0]);int y = int.Parse(yp[0]);int z = int.Parse(zp[0]);
        int? tx = xp[1] == string.Empty ? null : int.Parse(xp[1]);
        int? ty = yp[1] == string.Empty ? null : int.Parse(yp[1]);
        int? tz = zp[1] == string.Empty ? null : int.Parse(zp[1]);
        int? nx = xp[2] == string.Empty ? null : int.Parse(xp[2]);
        int? ny = yp[2] == string.Empty ? null : int.Parse(yp[2]);
        int? nz = zp[2] == string.Empty ? null : int.Parse(zp[2]);
        return new(x,y,z,tx,ty,tz,nx,ny,nz);
    }
    public static Face Parse(string s) => Parse(s,null);
    public static bool TryParse([NotNullWhen(true)] string? s,IFormatProvider? provider,[MaybeNullWhen(false)] out Face result)
    {
        result = null;
        try
        {
            if(s == null) throw new Exception();
            result = Parse(s,provider);
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }
    public static bool TryParse([NotNullWhen(true)] string? s,[MaybeNullWhen(false)] out Face result) => TryParse(s,null,out result);
    public override string ToString()
    {
        // f vx vy vz
        // f vx/vtx vy/vty vz/vtz
        // f vx/vtx/vnx vy/vty/vny vz/vtz/vnz
        // t vx//vnx vy//vny vz//vnz
        static string Format(int vertex,int? textureCoords,int? normal)
        {
            string result = $"{vertex}";
            if(textureCoords != null) result += $"/{textureCoords}";
            if(normal != null) result += (textureCoords == null ? '/' : string.Empty) + $"/{normal}";
            return result;
        }
        return $"f {Format(X,XT,XN)} {Format(Y,YT,YN)} {Format(Z,ZT,ZN)}";
    }
}