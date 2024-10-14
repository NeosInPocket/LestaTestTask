using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveSystemReader
{
    private readonly string savePath = Application.persistentDataPath + "/DataSaves/";

    public async Task<T> ReadFile<T>() where T : ISaveable
    {
        var assetContent = await GetTextAsset(typeof(T).Name);

        var result = JsonUtility.FromJson<T>(assetContent);
        return result;
    }

    private async Task<string> GetTextAsset(string assetId)
    {
        var filePath = savePath + assetId + ".json";

        if (File.Exists(filePath))
            using (var streamReader = new StreamReader(filePath))
            {
                return await streamReader.ReadToEndAsync();
            }

        throw new FileNotFoundException($"Asset file with type {assetId} is not found");
    }
}