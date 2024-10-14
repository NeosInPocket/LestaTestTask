using System.Linq;
using UnityEngine;

public class PathwayController
{
    public readonly PathwayManager pathwayManager;

    public PathwayController(SpawnPlatform spawnPlatform, TileFactory tileFactory,
        PathwayConfig config)
    {
        var defaultTile = tileFactory.Tiles.First(tile => tile.Type == TileType.Default);
        var tileSize = defaultTile.View.Size.x;

        pathwayManager = new PathwayManager(config);

        var startZOffset = spawnPlatform.Tiles.Max(tile => tile.Position.z) + tileSize * 1.5f;
        var xRange = new Vector2(
            spawnPlatform.Tiles.Min(tile => tile.Position.x),
            spawnPlatform.Tiles.Max(tile => tile.Position.x)
        );

        pathwayManager.CreatePathway(new Vector3(xRange.x, 0, startZOffset), config.Length, tileSize);
        pathwayManager.CreatePathway(new Vector3((xRange.x + xRange.y) / 2, 0, startZOffset), config.Length,
            tileSize);
        pathwayManager.CreatePathway(new Vector3(xRange.y, 0, startZOffset), config.Length, tileSize);
        pathwayManager.ResolveJunctions(defaultTile.View.Size);

        CreateInstances(tileFactory);
    }

    private void CreateInstances(TileFactory tileFactory)
    {
        foreach (var tileData in pathwayManager.Tiles)
        {
            var tilePrefab = tileFactory.Tiles.First(tile => tile.Type == tileData.Type);
            tileFactory.CreateInstance(tilePrefab, tileData);
        }
    }
}