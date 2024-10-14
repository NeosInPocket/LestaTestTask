using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileViewConfig viewConfig;
    [SerializeField] private TileBehaviourConfig behaviourConfig;
    [SerializeField] private TileView view;
    [SerializeField] private TileBehaviour behaviour;
    [SerializeField] private TileType tileType;

    public TileView View => view;
    public TileBehaviour Behaviour => behaviour;

    public TileType Type
    {
        get => tileType;
        set => tileType = value;
    }

    public void Initialize()
    {
        view.Initialize(viewConfig);
    }
}