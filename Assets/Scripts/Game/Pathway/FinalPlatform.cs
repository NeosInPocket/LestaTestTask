using UnityEngine;

public class FinalPlatform : SpawnPlatform
{
    private WinTrigger winTrigger;

    public FinalPlatform(WinTrigger winTrigger, Vector3 origin, TileFactory tileFactory, SquarePlatformConfig config) :
        base(origin, tileFactory, config)
    {
        this.winTrigger = winTrigger;
        winTrigger.Collider.size = Vector3.one * config.PlatformSize * config.TileSize;
        winTrigger.transform.position = origin;
    }
}