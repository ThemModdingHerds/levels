using ThemModdingHerds.Levels.Worlds;

namespace ThemModdingHerds.Levels;
public class LevelPack(string folder,IEnumerable<string> textures,WorldData worlds,IEnumerable<LevelData> levels)
{
    public const string TEXTURES_FOLDER = "textures";
    public HashSet<string> Textures {get;set;} = [..textures];
    public string Folder {get;set;} = folder;
    public WorldData Worlds {get;set;} = worlds;
    public List<LevelData> Levels {get;set;} = [.. levels];
    public LevelPack(string folder,WorldData worlds): this(folder,[],worlds,[])
    {

    }
    public LevelPack(string folder): this(folder,new())
    {

    }
    public void Save(string folder,bool overwrite = true)
    {
        if(Directory.Exists(folder))
        {
            if(!overwrite) throw new Exception($"{folder} already exists!");
            Directory.Delete(folder,true);
        }
        Directory.CreateDirectory(folder);
        string worldspath = Path.Combine(folder,Worlds.FileName);
        Worlds.Save(worldspath,overwrite);
        foreach(LevelData level in Levels)
            level.Save(folder,overwrite);
        string thisTexturesFolder = Path.Combine(Folder,TEXTURES_FOLDER);
        string outputTexturesFolder = Path.Combine(folder,TEXTURES_FOLDER);
        foreach(string texture in Textures)
        {
            string relTexturePath = Path.GetRelativePath(thisTexturesFolder,texture);
            string output = Path.Combine(outputTexturesFolder,relTexturePath);
            Directory.CreateDirectory(Path.GetDirectoryName(output) ?? throw new Exception("couldn't get folder"));
            File.Copy(texture,output,overwrite);
        }
    }
    public void Save(bool overwrite = true) => Save(Folder,overwrite);
    public static LevelPack Read(string folder)
    {
        string worldspath = Path.Combine(folder,WorldData.FILENAME);
        string stagespath = Path.Combine(folder,WorldData.SG_FILENAME);
        string? worldpath = null;
        if(worldpath == null && File.Exists(stagespath))
            worldpath = stagespath;
        if(worldpath == null &&  File.Exists(worldspath))
            worldpath = worldspath;
        worldpath ??= worldspath;
        if(!File.Exists(worldpath))
            File.WriteAllText(worldpath,new WorldData().ToString());
        WorldData worlds = WorldData.Read(worldpath);
        List<LevelData> levels = [];
        foreach(WorldEntry world in worlds.Entries)
        {
            string levelPath = Path.Combine(folder,world.Name);
            if(!File.Exists($"{levelPath}.lvl")) continue;
            levels.Add(LevelData.Read(levelPath));
        }
        string texturesPath = Path.Combine(folder,TEXTURES_FOLDER);
        IEnumerable<string> textures = Directory.Exists(texturesPath) ? Directory.EnumerateFiles(texturesPath,"*.*",SearchOption.AllDirectories) : [];
        return new(folder,textures,worlds,levels);
    }
    public void Add(params LevelPack[] packs)
    {
        foreach(LevelPack p in packs)
        {
            foreach(string ptexture in p.Textures)
                Textures.Add(ptexture);
            Worlds.Add(p.Worlds);
            foreach(LevelData data in p.Levels)
                Levels.Add(data);
        }
    }
}