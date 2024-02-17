using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Levels.Common;
public static class IOExt
{
    public static List<string> ReadPascal64Strings(this Reader reader,ulong count)
    {
        List<string> strings = [];
        for(ulong i = 0;i < count;i++)
            strings.Add(reader.ReadPascal64String());
        return strings;
    }
    public static void WritePascal64Strings(this Writer writer,IEnumerable<string> strings)
    {
        foreach(string str in strings)
            writer.WritePascal64String(str);
    }
    public static float[] ReadMatrix(this Reader reader)
    {
        float[] matrix = new float[16];
        for(int index = 0;index < 16;index++)
            matrix[index] = reader.ReadFloat();
        return matrix;
    }
    public static void WriteMatrix(this Writer writer,float[] matrix)
    {
        for(int index = 0;index < 16;index++)
            writer.Write(matrix[index]);
    }
    public static List<float[]> ReadMatrices(this Reader reader,ulong count)
    {
        List<float[]> matrices = [];
        for(ulong i = 0;i < count;i++)
            matrices.Add(ReadMatrix(reader));
        return matrices;
    }
    public static void WriteMatrices(this Writer writer,IEnumerable<float[]> matrices)
    {
        foreach(float[] matrix in matrices)
            WriteMatrix(writer,matrix);
    }
}