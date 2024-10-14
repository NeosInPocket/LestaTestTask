using UnityEngine;

public class DelayedDamageTileView : TileView
{
    [SerializeField] private DelayedDamageTileViewConfig viewConfig;
    private readonly int colorKey = Shader.PropertyToID("_Color");

    private void Start()
    {
        UpdateView(0f);
    }

    public void UpdateView(float damageValue)
    {
        var newColor = Color.Lerp(viewConfig.DamageColor, viewConfig.DefaultColor, damageValue);
        meshRenderer.material.SetColor(colorKey, newColor);
    }
}