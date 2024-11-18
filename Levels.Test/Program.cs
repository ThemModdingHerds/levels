using ThemModdingHerds.Levels;
using ThemModdingHerds.Levels.Models;

string levels = @"C:\Program Files (x86)\Steam\steamapps\common\Them's Fightin' Herds\data01\levels\temp\levels";

string path = "world_velvet";

string fullpath = @$"{levels}\{path}";

LevelData levelData = LevelData.Read(fullpath);
if(Directory.Exists(path))
    Directory.Delete(path,true);
Directory.CreateDirectory(path);
foreach(var pair in levelData.SGM)
{
    string filename = Path.Combine(path,Path.ChangeExtension(pair.Key,".obj"));
    WavefrontModel model = pair.Value.WavefrontModel;
    model.Save(filename);
}