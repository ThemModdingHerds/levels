using ThemModdingHerds.Levels;

string levels = "G:\\SteamLibrary\\steamapps\\common\\Them's Fightin' Herds\\data01\\levels\\temp\\levels";

string path = "world_velvet";

string fullpath = $"{levels}\\{path}";

LevelData levelData = LevelData.Read(fullpath);
Console.WriteLine();