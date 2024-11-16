using System.Data;
using System.Diagnostics.CodeAnalysis;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels.Worlds;

public class Worlds(IEnumerable<WorldEntry> entries) : IParsable<Worlds>
{
    public List<WorldEntry> Entries {get;set;} = [..entries];
    public Worlds(): this([])
    {

    }
    public override string ToString()
    {
        return Level.HEADER + string.Join('\n',from x in Entries select x.ToString());
    }
    public static Worlds Read(string path) => Parse(File.ReadAllText(path));
    public static Worlds Parse(string s,IFormatProvider? provider)
    {
        IEnumerable<string> lines = Strings.RemoveComments(s).Split('\n');
        List<WorldEntry> entries = [];
        foreach(string line in lines)
            entries.Add(WorldEntry.Parse(line));
        return new(entries);
    }
    public static Worlds Parse(string s) => Parse(s,null);
    public static bool TryParse([NotNullWhen(true)] string? s,IFormatProvider? provider,[MaybeNullWhen(false)] out Worlds result)
    {
        try
        {
            Worlds worlds = Parse(s ?? throw new Exception(),provider);
            result = worlds;
            return true;
        }
        catch(Exception)
        {
            result = null;
            return false;
        }
    }
    public static bool TryParse([NotNullWhen(true)] string? s,[MaybeNullWhen(false)] out Worlds result) => TryParse(s,null,out result);
}
