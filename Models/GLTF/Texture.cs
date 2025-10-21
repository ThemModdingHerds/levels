using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public class Texture : ChildOfRootProperty
{
    [JsonPropertyName("sampler")]
    public uint? Sampler {get;set;}
    [JsonPropertyName("source")]
    public uint? Source {get;set;}
}
public static class TextureExt
{
    public static uint AddTexture(this GLTFModel model,Texture texture)
    {
        model.Textures ??= [];
        model.Textures.Add(texture);
        return (uint)model.Textures.IndexOf(texture);
    }
    public static uint AddTexture(this GLTFModel model,uint image,uint sampler)
    {
        return AddTexture(model,new Texture()
        {
            Sampler = sampler,
            Source = image
        });
    }
    public static uint AddTexture(this GLTFModel model,uint image)
    {
        return AddTexture(model,new Texture()
        {
            Source = image
        });
    }
    public static uint AddTexture(this GLTFModel model,Image image,Sampler sampler)
    {
        return AddTexture(model,
            model.AddSampler(sampler),
            model.AddImage(image)
        );
    }
    public static uint AddTexture(this GLTFModel model,Image image)
    {
        return AddTexture(model,
            model.AddImage(image)
        );
    }
    public static uint AddTextureURI(this GLTFModel model,string image)
    {
        return AddTexture(model,model.AddImageURI(image));
    }
    public static uint AddTextureBufferView(this GLTFModel model,string image)
    {
        return AddTexture(model,model.AddImageBufferView(image));
    }
}