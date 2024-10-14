using UnityEngine;

[CreateAssetMenu(menuName = "Level/Spawn platform", fileName = "Spawn platform config")]
public class SquarePlatformConfig : ScriptableObject
{
    [SerializeField] private int platformSize;
    [SerializeField] private float platformTileSize;

    public int PlatformSize => platformSize;
    public float TileSize => platformTileSize;
}