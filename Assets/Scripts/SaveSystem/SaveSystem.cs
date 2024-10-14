using System.IO;
using System.Threading.Tasks;

public class SaveSystem<T> where T : ISaveable
{
    private readonly SaveSystemReader reader;
    private readonly SaveSystemWriter writer;

    public SaveSystem()
    {
        reader = new SaveSystemReader();
        writer = new SaveSystemWriter();
    }

    public async Task<T> LoadAsset()
    {
        try
        {
            var result = await reader.ReadFile<T>();
            return result;
        }
        catch (FileNotFoundException ex)
        {
            var result = await writer.CreateAsset<T>();
            return result;
        }
    }
}