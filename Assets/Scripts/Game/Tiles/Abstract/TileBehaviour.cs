using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        HandleCollisionEnter(other);
    }

    private void OnCollisionExit(Collision other)
    {
        HandleCollisionExit(other);
    }

    private void OnCollisionStay(Collision other)
    {
        HandleCollisionStay(other);
    }

    protected virtual void HandleCollisionEnter(Collision other)
    {
    }

    protected virtual void HandleCollisionExit(Collision other)
    {
    }

    protected virtual void HandleCollisionStay(Collision other)
    {
    }
}