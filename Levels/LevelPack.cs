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
        Directory.CreateDirectory(folder);
        string worldspath = Path.Combine(folder,Worlds.FileName);
        Worlds.Save(worldspath,overwrite);
        foreach(LevelData level in Levels)
            level.Save(folder,overwrite);
        foreach(string texture in Textures)
        {
            string path = Path.GetRelativePath(Folder,texture);
            string texturepath = Path.Combine(folder,path);
            Directory.CreateDirectory(Path.GetDirectoryName(texturepath) ?? throw new Exception("couldn't get folder"));
            File.Copy(texture,Path.Combine(folder,path),overwrite);
        }
    }
    public void Save() => Save(Folder);
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
        return new(folder,Directory.GetFiles(Path.Combine(folder,TEXTURES_FOLDER),"*.*",SearchOption.AllDirectories),worlds,levels);
    }
    public static LevelPack Combine(string folder,params LevelPack[] packs)
    {
        HashSet<string> textures = [];
        foreach(LevelPack p in packs)
        foreach(string ptexture in p.Textures)
            textures.Add(ptexture);
        List<LevelData> levels = [];
        foreach(LevelPack p in packs)
        foreach(LevelData data in p.Levels)
        {
            if(levels.Find((l) => l.Name == data.Name) != null)
                throw new Exception($"level '{data.Name}' already exists");
            levels.Add(data);
        }
        return new(folder,textures,WorldData.Combine([..from p in packs select p.Worlds]),levels);
    }
}