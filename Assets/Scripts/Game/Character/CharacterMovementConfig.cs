using UnityEngine;

[CreateAssetMenu(menuName = "Player/Movement config", fileName = "Character movement config")]
public class CharacterMovementConfig : ScriptableObject
{
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 0.1f;

    public float Gravity => gravity;
    public float JumpHeight => jumpHeight;
    public float MovementSpeed => movementSpeed;
    public float RotationSpeed => rotationSpeed;
}