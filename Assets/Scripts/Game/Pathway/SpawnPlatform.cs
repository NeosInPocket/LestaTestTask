using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPlatform
{
    private readonly Vector3 origin;
    protected SquarePlatformConfig config;

    public SpawnPlatform(Vector3 origin, TileFactory tileFactory, SquarePlatformConfig config)
    {
        this.origin = origin;

        var defaultTile = tileFactory.Tiles.First(x => x.Type == TileType.Default);
        var datas = GetTilesInfo(config);

        for (var i = 0; i < config.PlatformSize; i++)
            tileFactory.CreateInstances(defaultTile, datas);
    }

    public List<TileFactoryInstanceData> Tiles { get; private set; }

    private List<TileFactoryInstanceData> GetTilesInfo(SquarePlatformConfig config)
    {
        Tiles = new List<TileFactoryInstanceData>();
        var platformSize = config.PlatformSize;
        var tileSize = config.TileSize;

        for (var x = -platformSize / 2; x < platformSize / 2; x++)
        for (var z = -platformSize / 2; z < platformSize / 2; z++)
        {
            var data = new TileFactoryInstanceData();
            data.Position = origin + new Vector3(x * tileSize, 0, z * tileSize);
            Tiles.Add(data);
        }

        return Tiles;
    }
}