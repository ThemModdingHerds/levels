using System.Text.Json.Serialization;

namespace ThemModdingHerds.Models.GLTF;
public abstract class Image : ChildOfRootProperty
{
    [JsonPropertyName("mimeType")]
    public string? MimeType {get;set;}
}
public class ImageURI : Image
{
    [JsonPropertyName("uri")]
    public string URI {get;set;} = string.Empty;
}
public class ImageBufferView : Image
{
    [JsonPropertyName("bufferView")]
    public uint BufferView {get;set;}
}
public static class ImageExt
{
    public static uint AddImage(this GLTFModel model,Image image)
    {
        model.Images ??= [];
        model.Images.Add(image);
        return (uint)model.Images.IndexOf(image);
    }
    public static uint AddImageURI(this GLTFModel model,string filepath)
    {
        if(!File.Exists(filepath)) throw new FileNotFoundException($"couldn't add '{filepath}' as Image",filepath);
        model.Data.Add(File.ReadAllBytes(filepath));
        return AddImage(model,new ImageURI()
        {
            URI = Path.GetFileName(filepath)
        });
    }
    public static uint AddImageBufferView(this GLTFModel model,string filepath)
    {
        if(!File.Exists(filepath)) throw new FileNotFoundException($"couldn't add '{filepath}' as Image",filepath);
        return AddImage(model,new ImageBufferView()
        {
            BufferView = model.AddBuffer(filepath)
        });
    }
    public static Image? GetTextureImage(this GLTFModel model,Texture texture)
    {
        if(model.Images == null || texture.Source == null) return null;
        return model.Images[(int)texture.Source];
    }
    public static Image? GetTextureImage(this GLTFModel model,uint texture)
    {
        if(model.Textures == null) return null;
        return GetTextureImage(model,model.Textures[(int)texture]);
    }
    public static Sampler? GetTextureSampler(this GLTFModel model,Texture texture)
    {
        if(model.Samplers == null || texture.Sampler == null) return null;
        return model.Samplers[(int)texture.Sampler];
    }
    public static Sampler? GetTextureSampler(this GLTFModel model,uint texture)
    {
        if(model.Textures == null) return null;
        return GetTextureSampler(model,model.Textures[(int)texture]);
    }
}