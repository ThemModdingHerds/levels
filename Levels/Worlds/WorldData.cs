using System.Data;
using System.Diagnostics.CodeAnalysis;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels.Worlds;

public class WorldData(IEnumerable<WorldEntry> entries) : IParsable<WorldData>
{
    public const string FILENAME = "worlds.ini";
    public List<WorldEntry> Entries {get;set;} = [..entries];
    public WorldData(): this([])
    {

    }
    public override string ToString()
    {
        return Level.HEADER + string.Join('\n',from x in Entries select x.ToString());
    }
    public void Save(string file,bool overwrite = true)
    {
        if(Path.GetFileName(file) != FILENAME)
            throw new Exception($"file be called '{FILENAME}'");
        if(File.Exists(file))
        {
            if(!overwrite)
                throw new Exception($"{file} already exists");
            File.Delete(file);
        }
        File.WriteAllText(file,ToString());
    }
    public static WorldData Combine(params WorldData[] data)
    {
        WorldData worlds = new();
        foreach(WorldData world in data)
        foreach(WorldEntry entry in world.Entries)
        {
            if(worlds.Entries.Find((e) => e.Name == entry.Name) != null)
                throw new Exception($"{entry.Name} already exists");
            worlds.Entries.Add(entry);
        }
        return worlds;
    }
    public static WorldData Read(string path) => Parse(File.ReadAllText(path));
    public static WorldData Parse(string s,IFormatProvider? provider)
    {
        IEnumerable<string> lines = Strings.RemoveComments(s).Split('\n');
        List<WorldEntry> entries = [];
        foreach(string line in lines)
            entries.Add(WorldEntry.Parse(line));
        return new(entries);
    }
    public static WorldData Parse(string s) => Parse(s,null);
    public static bool TryParse([NotNullWhen(true)] string? s,IFormatProvider? provider,[MaybeNullWhen(false)] out WorldData result)
    {
        try
        {
            WorldData worlds = Parse(s ?? throw new Exception(),provider);
            result = worlds;
            return true;
        }
        catch(Exception)
        {
            result = null;
            return false;
        }
    }
    public static bool TryParse([NotNullWhen(true)] string? s,[MaybeNullWhen(false)] out WorldData result) => TryParse(s,null,out result);
}
