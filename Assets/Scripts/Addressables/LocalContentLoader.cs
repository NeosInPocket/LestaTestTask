using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class LocalContentLoader
{
    public async Task<T> LoadAssetWithComponent<T>(string assetId) where T : Component
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(assetId);
        var loadedObject = await handle.Task;

        if (loadedObject == null) throw new NullReferenceException($"Loaded asset of type {typeof(T)} is null");

        if (loadedObject.TryGetComponent(out T component))
            return component;
        throw new NullReferenceException($"There is no component of type {typeof(T)} on the asset {assetId}");
    }

    public async Task<T> LoadAndInstantiate<T>(string gameObjectId) where T : Object
    {
        var handle = Addressables.InstantiateAsync(gameObjectId);
        var loadedObject = await handle.Task;

        if (loadedObject == null) throw new NullReferenceException($"Loaded asset of type {typeof(T)} is null");

        if (loadedObject.TryGetComponent(out T component)) return component;

        throw new NullReferenceException($"Component of type {typeof(T)} is not found on {loadedObject.name}");
    }
}