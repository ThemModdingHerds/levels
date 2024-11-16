using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
        return $"{Type} {Name} {string.Join(' ',from x in Properties select $"[{x.Key}] {x.Value}")}";
    }
    public static WorldEntry Parse(string s,IFormatProvider? provider)
    {
        string buffer = string.Empty;
        string? type = null;
        string? name = null;
        Dictionary<string,string> props = [];
        for(int i = 0;i < s.Length;i++)
        {
            char letter = s[i];
            if(type == null)
            {
                if(letter == ' ')
                {
                    type = buffer.Trim();
                    buffer = string.Empty;
                    continue;
                }
                buffer += letter;
                continue;
            }
            if(name == null)
            {
                if(letter == ' ')
                {
                    name = buffer.Trim();
                    buffer = string.Empty;
                    continue;
                }
                buffer += letter;
                continue;
            }
            if(!s.Contains('['))
                break;
            if(letter == '[')
            {
                int keyStart = i + 1;
                int keyEnd = s.IndexOf(']',i);
                if(keyEnd == -1)
                    throw new Exception("no end tag was found!");
                string key = s[keyStart..keyEnd].Trim();
                i = keyEnd + 1;
                while(s[i] == ' ')
                    i++;
                int valueEnd = s.IndexOf(' ',i);
                if(valueEnd == -1)
                    valueEnd = s.Length;
                string value = s[i..valueEnd].Trim();
                props.Add(key,value);
            }
        }
        if(name == null && buffer.Length > 0)
            name = buffer;
        if(type == null)
            throw new Exception("couldn't get type");
        if(name == null)
            throw new Exception("couldn't get name");
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