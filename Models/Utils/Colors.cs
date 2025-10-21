using System.Drawing;

namespace ThemModdingHerds.Models.Utils;
public static class Colors
{
    public static bool IsWhite(Color color)
    {
        return color.R == 0xff && color.G == 0xff && color.B == 0xff;
    }
}