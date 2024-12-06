using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels.Worlds;
public class WorldEntry(string type,string name,Dictionary<string,string> props) : IParsable<WorldEntry>
{
    public const string DESCRIPTION_KEY = "desc";
    public const string DLC_KEY = "dlc";
    public const string IMAGE_KEY = "img";
    public const string TAGS_KEY = "tags";
    [JsonPropertyName("type")]
    public string Type {get; set;} = type;
    [JsonPropertyName("name")]
    public string Name {get; set;} = name;
    [JsonPropertyName("properties")]
    public Dictionary<string,string> Properties {get; set;} = props;
    [JsonIgnore]
    public string Description {get => Properties[DESCRIPTION_KEY]; set => Properties[DESCRIPTION_KEY] = value;}
    [JsonIgnore]
    public string DLC {
        get
        {
            Properties.TryGetValue(DLC_KEY,out string? dlc);
            return dlc ?? string.Empty;
        }
        set => Properties[DLC_KEY] = value;
    }
    [JsonIgnore]
    public string Image {
        get
        {
            Properties.TryGetValue(IMAGE_KEY,out string? img);
            return img ?? string.Empty;
        }
        set => Properties[IMAGE_KEY] = value;
    }
    [JsonIgnore]
    public string Tags {
        get
        {
            Properties.TryGetValue(TAGS_KEY,out string? tags);
            return tags ?? string.Empty;
        }
        set => Properties[TAGS_KEY] = value;
    }
    public WorldEntry(string type,string name,string desc): this(type,name,[])
    {
        Description = desc;
    }
    public override string ToString()
    {
        return ToString(false);
    }
    public string ToString(bool stage)
    {
        return stage ? BuildStageEntry() : BuildWorldEntry();
    }
    public string BuildWorldEntry()
    {
        return $"{Type} {Name} {string.Join(' ',from x in Properties select $"[{x.Key}] {x.Value}")}".Trim();
    }
    public string BuildStageEntry()
    {
        return $"{Type} {Name} {Image} {string.Join(' ',from x in Properties select $"{x.Key}>{x.Value}")}".Trim();
    }
    public static WorldEntry Parse(string s,IFormatProvider? provider)
    {
        Dictionary<string,string> props = [];
        string[] parts = Strings.ParseParts(s);
        string type = parts[0];
        string name = parts[1];
        if(parts.Length > 2)
        {
            if(s.Contains('['))
            for(int i = 2;i < parts.Length;i++)
            {
                string key = parts[i];
                i++;
                string value = parts[i];
                props.Add(key[1..key.IndexOf(']')],value);
            }
            else if(s.Contains('>'))
            {
                props.Add(IMAGE_KEY,parts[2]);
                for(int i = 3;i < parts.Length;i++)
                {
                    string[] prop = parts[i].Split('>');
                    props.Add(prop[0],prop[1]);
                }
            }
        }
        return new(type,name,props);
    }
    public static WorldEntry Parse(string s) => Parse(s,null);
    public static bool TryParse([NotNullWhen(true)] string? s,IFormatProvider? provider,[MaybeNullWhen(false)] out WorldEntry result)
    {
        try
        {
            WorldEntry entry = Parse(s ?? throw new Exception(),provider);
            result = entry;
            return true;
        }
        catch(Exception)
        {
            result = null;
            return false;
        }
    }
    public static bool TryParse([NotNullWhen(true)] string? s,[MaybeNullWhen(false)] out WorldEntry result) => TryParse(s,null,out result);
}