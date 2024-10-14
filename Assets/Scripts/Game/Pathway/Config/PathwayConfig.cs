using UnityEngine;

[CreateAssetMenu(menuName = "Level/Pathway", fileName = "Pathway config")]
public class PathwayConfig : ScriptableObject
{
    [SerializeField] private float minDistanceBetweenPoints = 3f;
    [SerializeField] private float maxDistanceBetweenPoints = 10f;
    [SerializeField] private float maxSpread;
    [SerializeField] private float length;
    [Range(0, 1f)] [SerializeField] private float customTileAppearChance;
    public float MinDistanceBetweenPoints => minDistanceBetweenPoints;
    public float MaxDistanceBetweenPoints => maxDistanceBetweenPoints;
    public float MaxSpread => maxSpread;
    public float Length => length;
    public float CustomTileAppearChance => customTileAppearChance;
}