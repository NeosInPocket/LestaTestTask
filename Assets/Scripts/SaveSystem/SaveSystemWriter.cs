using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveSystemWriter
{
    private readonly string savePath = Application.persistentDataPath + "/DataSaves/";

    public SaveSystemWriter()
    {
        CheckDirectory();
    }

    public void Write<T>(T asset)
    {
    }

    public async Task<T> CreateAsset<T>()
    {
        var instance = Activator.CreateInstance<T>();
        var assetContent = JsonUtility.ToJson(instance, true);

        using (var sw = new StreamWriter(savePath + typeof(T).Name + ".json"))
        {
            await sw.WriteAsync(assetContent);
        }

        // using (var fileStream = new FileStream(savePath + typeof(T).Name + ".json", FileMode.Create))
        // {
        //     fileStream.Write(Encoding.UTF8.GetBytes(assetContent), 0, Encoding.UTF8.GetByteCount(assetContent));
        // }

        return instance;
    }

    private void CheckDirectory()
    {
        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
    }
}