namespace ThemModdingHerds.Models.MTL;
public class Material(string name,string diffuse)
{
    public string Name {get;set;} = name;
    public string DiffuseTexture {get;set;} = diffuse;
    public override string ToString()
    {
        return string.Join('\n',[
            $"newmtl {Name}",
            $"\tmap_Kd {DiffuseTexture}",
            $"\tmap_d {DiffuseTexture}"
        ]);
    }
    public string WithModel(WavefrontModel model,string name,string matpath)
    {
        return string.Join('\n',[
            "# material",
            $"mtllib {matpath}",
            $"usemtl {Name}",
            model.ToString(name),
        ]);
    }
}