using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Levels;

if (args.Length != 1)
{
    Console.WriteLine("usage: Levels.Test.exe <file>");
    Environment.Exit(1);
}

string path = args[0];
Reader reader = new(path);

if(path.EndsWith(".sgi.msb"))
{
    SGIFile file = reader.ReadSGIFile();
    Console.WriteLine(file);
}
if(path.EndsWith(".sga.msb"))
{
    SGAFile file = reader.ReadSGAFile();
    Console.WriteLine(file);
}
if(path.EndsWith(".sgm.msb"))
{
    SGMFile file = reader.ReadSGMFile();
    Console.WriteLine(file);
}
if(path.EndsWith(".sgs.msb"))
{
    SGSFile file = reader.ReadSGSFile();
    Console.WriteLine(file);
}
throw new NotImplementedException("can't handle the provided file");