using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameAssetsLoadingOperation : ILoadingOperation
{
    private int loadedTilesCount;
    private Action<float> onProgressUpdate;
    public List<Tile> LoadedTiles { get; private set; }

    public string Description => "Loading tiles";

    public async Task Load(Action<float> onUpdate)
    {
        LoadedTiles = new List<Tile>();
        onProgressUpdate = onUpdate;

        var loader = new LocalContentLoader();

        foreach (var tileId in Enum.GetValues(typeof(TileType)))
        {
            var tileAssetId = tileId.ToString();
            var tile = await loader.LoadAssetWithComponent<Tile>(tileAssetId);
            tile.Initialize();
            LoadedTiles.Add(tile);

            loadedTilesCount++;
            onProgressUpdate.Invoke(loadedTilesCount / Enum.GetValues(typeof(TileType)).Length);
        }
    }
}