using System.Linq;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private SquarePlatformConfig squarePlatformConfig;
    [SerializeField] private PathwayConfig pathwayConfig;
    [SerializeField] private TileFactory tileFactory;
    [SerializeField] private WinTrigger winTrigger;
    private PathwayController pathwayController;

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (pathwayController != null)
        {
            Gizmos.color = Color.green;

            foreach (var pathway in
                     pathwayController.pathwayManager
                         .pathways) pathway.DrawGizmos();
        }
    }

    public void BuildLevel()
    {
        var spawnPlatform = new SpawnPlatform(Vector3.zero, tileFactory, squarePlatformConfig);
        pathwayController = new PathwayController(spawnPlatform, tileFactory, pathwayConfig);

        var lastGeneratedTileZPosition = pathwayController.pathwayManager.Tiles.Max(tile => tile.Position.z);
        var finalPlatformOrigin = new Vector3();
        var defaultTileSize = tileFactory.Tiles.First(x => x.Type == TileType.Default).View.Size.x;

        finalPlatformOrigin.z =
            lastGeneratedTileZPosition + defaultTileSize * (squarePlatformConfig.PlatformSize / 2f + 1f);

        var finalPlatform = new FinalPlatform(winTrigger, finalPlatformOrigin, tileFactory, squarePlatformConfig);
    }
}