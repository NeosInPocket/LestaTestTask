using System.Collections.Generic;
using UnityEngine;

public class GameObjectFactory : MonoBehaviour
{
    public List<Tile> Tiles { get; set; }

    private void Start()
    {
        Tiles = GameContext.Instance.assetsLoadingOperation.LoadedTiles;
    }

    public List<T> CreateInstances<T>(T prefab, List<TileFactoryInstanceData> datas)
        where T : Tile
    {
        var instances = new List<T>();

        foreach (var data in datas)
        {
            var instance = CreateInstance(prefab, data);
            instance.Initialize();
            instances.Add(instance);
        }

        return instances;
    }

    public T CreateInstance<T>(T prefab, TileFactoryInstanceData data) where T : Tile
    {
        var instance = Instantiate(prefab, transform);
        instance.transform.position = data.Position;
        instance.transform.rotation = data.Rotation;
        instance.transform.localScale = data.Scale;
        instance.Type = data.Type;
        instance.Initialize();

        return instance;
    }
}