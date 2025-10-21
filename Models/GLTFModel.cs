using System.Text.Json;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.Models.GLTF;
using ThemModdingHerds.Models.GLTF.Binary;
using Buffer = ThemModdingHerds.Models.GLTF.Buffer;

namespace ThemModdingHerds.Models;
public class GLTFModel : Property
{
    [JsonPropertyName("extensionsUsed")]
    public HashSet<string>? ExtensionsUsed {get;set;}
    [JsonPropertyName("extensionRequired")]
    public HashSet<string>? ExtensionRequired {get;set;}
    [JsonPropertyName("accessors")]
    public List<Accessor>? Accessors {get;set;}
    [JsonPropertyName("animations")]
    public List<Animation>? Animations {get;set;}
    [JsonPropertyName("asset")]
    public Asset Asset {get;set;} = new();
    [JsonPropertyName("buffers")]
    public List<Buffer>? Buffers {get;set;}
    [JsonPropertyName("cameras")]
    public List<GLTF.Camera>? Cameras {get;set;}
    [JsonPropertyName("bufferViews")]
    public List<BufferView>? BufferViews {get;set;}
    [JsonPropertyName("images")]
    public List<Image>? Images {get;set;}
    [JsonPropertyName("materials")]
    public List<Material>? Materials {get;set;}
    [JsonPropertyName("meshes")]
    public List<Mesh>? Meshes {get;set;}
    [JsonPropertyName("nodes")]
    public List<Node>? Nodes {get;set;}
    [JsonPropertyName("samplers")]
    public List<Sampler>? Samplers {get;set;}
    [JsonPropertyName("scene")]
    public uint? Scene {get;set;}
    [JsonIgnore]
    public Scene? DefaultScene => Scene == null ? null : Scenes?[(int)Scene];
    [JsonPropertyName("scenes")]
    public List<Scene>? Scenes {get;set;}
    [JsonPropertyName("skins")]
    public List<Skin>? Skins {get;set;}
    [JsonPropertyName("textures")]
    public List<Texture>? Textures {get;set;}
    [JsonIgnore]
    public List<byte[]> Data {get;set;} = [];
    public static GLTFModel ReadBinary(string filepath)
    {
        Reader reader = new(filepath);
        Header header = reader.ReadGLTFHeader();
        List<Chunk> chunks = [];
        while(reader.Offset < header.Length)
            chunks.Add(reader.ReadGLTFChunk());
        Chunk json = chunks.Find((chunk) => chunk.Type == Chunk.JSON) ?? throw new Exception("no JSON chunk found in glb");
        GLTFModel model = JsonSerializer.Deserialize<GLTFModel>(json.Data) ?? throw new Exception("couldn't parse JSON chunk");
        foreach(Chunk binary in chunks.Where((chunk) => chunk.Type == Chunk.BIN))
            model.Data.Add(binary.Data);
        return model;
    }
    public void AddModel(Model model)
    {
        
    }
    public void AddUsedExtension(string ext)
    {
        ExtensionsUsed ??= [];
        ExtensionsUsed.Add(ext);
    }
    public void AddRequiredExtension(string ext)
    {
        AddUsedExtension(ext);
        ExtensionRequired ??= [];
        ExtensionRequired.Add(ext);
    }
}