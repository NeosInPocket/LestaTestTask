using UnityEngine;

[CreateAssetMenu(menuName = "Tile/ViewConfig", fileName = "Tile view config")]
public class TileViewConfig : ScriptableObject
{
    [SerializeField] private Material material;
    [SerializeField] private Vector3 defaultSize;
    public Material Material => material;
    public Vector3 DefaultSize => defaultSize;
}