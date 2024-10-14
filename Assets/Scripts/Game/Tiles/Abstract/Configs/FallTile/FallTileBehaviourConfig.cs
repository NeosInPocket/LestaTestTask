using UnityEngine;

[CreateAssetMenu(menuName = "Tile/Fall tile/Behaviour Config",
    fileName = "FallTileBehaviourConfig")]
public class FallTileBehaviourConfig : ScriptableObject
{
    [SerializeField] private float startFallTime;
    [SerializeField] private float yDestroyValue;
    public float StartFallTime => startFallTime;
    public float YDestroyValue => yDestroyValue;
}