using UnityEngine;

[CreateAssetMenu(menuName = "Tile/Delayed damage tile/View Config",
    fileName = "DelayedDamageTileViewConfig")]
public class DelayedDamageTileViewConfig : ScriptableObject
{
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color defaultColor;
    public Color DamageColor => damageColor;
    public Color DefaultColor => defaultColor;
}