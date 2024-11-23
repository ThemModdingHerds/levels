using System.Globalization;
using System.Text.Json.Serialization;
using ThemModdingHerds.Levels.Utils;

namespace ThemModdingHerds.Levels;
public class Reverb(ReverbOptions type,float wetness)
{
    [JsonPropertyName("type")]
    public ReverbOptions Type {get; set;} = type;
    [JsonPropertyName("wetness")]
    public float Wetness {get; set;} = wetness;
    public Reverb(): this(ReverbOptions.OFF,0)
    {

    }
    public override string ToString()
    {
        return $"Reverb: {Type} {Strings.FloatString(Wetness)}";
    }
    public static Reverb Parse(string s)
    {
        string rest = LevelParsers.ParseRest(s,"Reverb:");
        ReverbOptions? type = null;
        float? wetness = null;
        string buffer = string.Empty;
        for(int i = 0;i < rest.Length;i++)
        {
            char letter = rest[i];
            if(type == null)
            {
                if(letter == ' ')
                {
                    if(buffer.Length > 0)
                    {
                        type = Enum.Parse<ReverbOptions>(buffer.Trim());
                        buffer = string.Empty;
                    }
                    continue;
                }
            }
            if(wetness == null)
            {
                if(letter == ' ')
                {
                    if(buffer.Length > 0)
                    {
                        wetness = float.Parse(buffer.Trim(),NumberStyles.Any,CultureInfo.InvariantCulture);
                        buffer = string.Empty;
                    }
                    continue;
                }
            }
            buffer += letter;
        }
        if(buffer.Length > 0)
            wetness = float.Parse(buffer.Trim(),NumberStyles.Any,CultureInfo.InvariantCulture);
        if(type == null)
            throw new Exception("couldn't find reverb type");
        if(wetness == null)
            throw new Exception("couldn't find wetness");
        return new(type.Value,wetness.Value);
    }
}