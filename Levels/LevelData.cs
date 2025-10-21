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
    public Dictionary<string,SGAnimation> Animations {get;set;} = [];
    public SGIndex? Background {get;set;} = new();
    public Dictionary<string,SGModel> Models {get;set;} = [];
    public Dictionary<string,SGSkeleton> Skeletons {get;set;} = [];
    public LevelData(): this(string.Empty,new())
    {

    }
    public SGModel? GetModelFromElement(Element element)
    {
        string key = $"{element.Name}.sgm.msb";
        if(Models.TryGetValue(key,out SGModel? sgm))
            return sgm;
        return null;
    }
    public SGAnimation? GetAnimationFromElementAnimation(Animation animation)
    {
        string key = $"{animation.FileName}.sga.msb";
        if(Animations.TryGetValue(key,out SGAnimation? sga))
            return sga;
        return null;
    }
    public List<SGAnimation> GetAnimationsFromElement(Element element)
    {
        return [..from animation in element.Animations select GetAnimationFromElementAnimation(animation)];
    }
    public SGSkeleton? GetSkeleton(SGModel model)
    {
        foreach(var pair in Skeletons)
        foreach(SGS.Bone bone in pair.Value.Bones)
        foreach(string boneName in model.Bones)
        {
            if(boneName == bone.Name)
                return pair.Value;
        }
        return null;
    }
    public void Save(string path,bool overwrite = true)
    {
        string folder = Path.Combine(path,Name);
        string lvlfile = $"{folder}.lvl";
        if(Background != null) 
        {
            Directory.CreateDirectory(folder);
            string filepath = Path.Combine(folder,SGIndex.FILENAME);
            Writer writer = new(filepath);
            writer.Write(Background);
            writer.Close();
            foreach(var pair in Animations)
            {
                filepath = Path.Combine(folder,pair.Key);
                writer = new(filepath);
                writer.Write(pair.Value);
                writer.Close();
            }
            foreach(var pair in Models)
            {
                filepath = Path.Combine(folder,pair.Key);
                writer = new(filepath);
                writer.Write(pair.Value);
                writer.Close();
            }
            foreach(var pair in Skeletons)
            {
                filepath = Path.Combine(folder,pair.Key);
                writer = new(filepath);
                writer.Write(pair.Value);
                writer.Close();
            }
        }
        Lvl.Save(lvlfile,overwrite);
    }
    public override string ToString()
    {
        return Name;
    }
    public static LevelData Read(string path)
    {
        string name = Path.GetFileName(path);
        Level level = Level.Read($"{path}.lvl");
        string[] files = Directory.Exists(path) ? Directory.GetFiles(path) : [];
        Dictionary<string,SGAnimation> animations = [];
        Dictionary<string,SGModel> models = [];
        Dictionary<string,SGSkeleton> skeletons = [];
        SGIndex? background = null;
        if(Directory.Exists(path))
        {
            string backgroundPath = Path.Combine(path,SGIndex.FILENAME);
            Reader backgroundReader = new(backgroundPath);
            background = backgroundReader.ReadSGIFile();
        }
        foreach(string file in files)
        {
            string filename = Path.GetFileName(file);
            Reader reader = new(file);
            if(file.EndsWith(".sga.msb"))
                animations.Add(filename,reader.ReadSGAFile());
            if(file.EndsWith(".sgm.msb"))
                models.Add(filename,reader.ReadSGMFile());
            if(file.EndsWith(".sgs.msb"))
                skeletons.Add(filename,reader.ReadSGSFile());
        }
        return new(name,level)
        {
            Animations = animations,
            Background = background,
            Models = models,
            Skeletons = skeletons
        };
    }
}