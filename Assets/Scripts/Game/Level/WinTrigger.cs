using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    public BoxCollider Collider => boxCollider;
}