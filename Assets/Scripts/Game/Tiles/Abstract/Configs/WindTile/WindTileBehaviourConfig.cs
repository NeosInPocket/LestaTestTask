using UnityEngine;

[CreateAssetMenu(menuName = "Platform/Wind tile/Behaviour Config", fileName = "WindTileBehaviourConfig")]
public class WindTileBehaviourConfig : ScriptableObject
{
    [SerializeField] private float windStrength;
    public float WindStrength => windStrength;
}