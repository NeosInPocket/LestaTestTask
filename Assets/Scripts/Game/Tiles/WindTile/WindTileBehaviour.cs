using UnityEngine;

public class WindTileBehaviour : TileBehaviour
{
    [SerializeField] private WindTileBehaviourConfig behaviourConfig;
    [SerializeField] private BoxCollider enterTrigger;
    private CharacterMovement characterMovement;

    private void Update()
    {
        if (characterMovement != null)
            characterMovement.SetAdditionalForce(Vector3.right * behaviourConfig.WindStrength * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out CharacterContainer character))
            characterMovement = character.GetComponent<CharacterContainer>().Movement;

        Debug.Log("enter");
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out CharacterContainer character))
        {
            characterMovement.SetAdditionalForce(Vector3.zero);
            characterMovement = null;
        }

        Debug.Log("exit");
    }
}