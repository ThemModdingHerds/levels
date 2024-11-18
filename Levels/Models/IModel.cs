namespace ThemModdingHerds.Levels.Models;
public interface IModel
{
    public List<IFace> Faces {get;set;}
    public List<IVertex> Vertices {get;set;}
}