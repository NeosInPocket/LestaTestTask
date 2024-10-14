using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private string levelBuilderAssetId;

    private async void Start()
    {
        var loader = new LocalContentLoader();
        var levelBuilder = await loader.LoadAndInstantiate<LevelBuilder>(levelBuilderAssetId);

        levelBuilder.BuildLevel();
    }
}