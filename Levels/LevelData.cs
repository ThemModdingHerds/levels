using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels.SGA;
using ThemModdingHerds.Levels.SGI;
using ThemModdingHerds.Levels.SGM;
using ThemModdingHerds.Levels.SGS;

namespace ThemModdingHerds.Levels;
public class LevelData(string name,Level lvl)
{
    public string Name {get;set;} = name;
    public Level Lvl {get;set;} = lvl;
    public Dictionary<string,SGAFile> SGA {get;set;} = [];
    public Dictionary<string,SGIFile> SGI {get;set;} = [];
    public Dictionary<string,SGMFile> SGM {get;set;} = [];
    public Dictionary<string,SGSFile> SGS {get;set;} = [];
    public LevelData(): this(string.Empty,new())
    {

    }
    public void Write(string path,string? name)
    {
        string folder = Path.Combine(path,name ?? Name);
        string lvl = $"{folder}.lvl";
        Directory.CreateDirectory(folder);
        File.WriteAllText(lvl,Lvl.ToString());
        foreach(var pair in SGA)
        {
            string filepath = pair.Key;
            Writer writer = new(filepath);
            writer.Write(pair.Value);
        }
        foreach(var pair in SGI)
        {
            string filepath = pair.Key;
            Writer writer = new(filepath);
            writer.Write(pair.Value);
        }
        foreach(var pair in SGM)
        {
            string filepath = pair.Key;
            Writer writer = new(filepath);
            writer.Write(pair.Value);
        }
        foreach(var pair in SGS)
        {
            string filepath = pair.Key;
            Writer writer = new(filepath);
            writer.Write(pair.Value);
        }
    }
    public static LevelData Read(string path)
    {
        string name = Path.GetFileName(path);
        Level level = Level.Read($"{path}.lvl");
        string[] files = Directory.GetFiles(path);
        Dictionary<string,SGAFile> sga = [];
        Dictionary<string,SGIFile> sgi = [];
        Dictionary<string,SGMFile> sgm = [];
        Dictionary<string,SGSFile> sgs = [];
        foreach(string file in files)
        {
            string filename = Path.GetFileName(file);
            Reader reader = new(file);
            if(file.EndsWith(".sga.msb"))
                sga.Add(filename,reader.ReadSGAFile());
            if(file.EndsWith(".sgi.msb"))
                sgi.Add(filename,reader.ReadSGIFile());
            if(file.EndsWith(".sgm.msb"))
                sgm.Add(filename,reader.ReadSGMFile());
            if(file.EndsWith(".sgs.msb"))
                sgs.Add(filename,reader.ReadSGSFile());
        }
        return new(name,level)
        {
            SGA = sga,
            SGI = sgi,
            SGM = sgm,
            SGS = sgs
        };
    }
}