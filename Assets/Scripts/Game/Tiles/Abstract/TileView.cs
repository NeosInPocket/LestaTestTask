using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] protected MeshFilter meshFilter;
    [SerializeField] protected MeshRenderer meshRenderer;
    protected TileBuilder builder;
    protected TileViewConfig config;

    public Vector3 Size => builder.Size;

    public void Initialize(TileViewConfig config)
    {
        this.config = config;

        meshRenderer.material = config.Material;

        builder = new TileBuilder(meshFilter, config.DefaultSize);
        boxCollider.size = builder.Size;
    }
}