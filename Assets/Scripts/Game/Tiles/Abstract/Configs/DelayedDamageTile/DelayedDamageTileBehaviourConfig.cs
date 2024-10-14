using UnityEngine;

[CreateAssetMenu(menuName = "Tile/Delayed damage tile/Behaviour Config",
    fileName = "DelayedDamageTileBehaviourConfig")]
public class DelayedDamageTileBehaviourConfig : ScriptableObject
{
    [SerializeField] private float damageTime;
    [SerializeField] private int damageAmount;
    [SerializeField] private float reloadTime;
    public float DamageTime => damageTime;
    public int DamageAmount => damageAmount;
    public float ReloadTime => reloadTime;
}