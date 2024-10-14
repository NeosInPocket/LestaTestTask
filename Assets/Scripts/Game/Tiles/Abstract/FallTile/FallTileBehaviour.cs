using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/Delayed damage tile/Behaviour Config",
    fileName = "DelayedDamageTileBehaviourConfig")]
public class FallTileBehaviour : TileBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private FallTileBehaviourConfig config;

    protected override void HandleCollisionEnter(Collision other)
    {
        StartCoroutine(SetFall());
    }

    private IEnumerator SetFall()
    {
        yield return new WaitForSeconds(config.StartFallTime);
        rigidbody.constraints = RigidbodyConstraints.None;

        while (transform.position.y > config.YDestroyValue) yield return null;

        Destroy(gameObject);
    }
}