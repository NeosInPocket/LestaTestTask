using System;
using UnityEngine;

public class CharacterMovement
{
    private readonly Transform characterTransform;

    private readonly CharacterMovementConfig config;
    private readonly InputHandler inputHandler;
    private readonly Rigidbody rigidbody;
    private Vector3 additionalMovement;
    public bool isGrounded;
    private Vector3 moveDirection;
    public bool slipped;
    private float turnSmoothVelocity;
    private Vector3 velocity;

    public CharacterMovement(Rigidbody rigidbody, InputHandler inputHandler,
        CharacterMovementConfig config)
    {
        this.rigidbody = rigidbody;
        this.inputHandler = inputHandler;
        characterTransform = rigidbody.transform;

        this.config = config;
    }

    public Action OnGrounded { get; set; }
    public Action OnJump { get; set; }
    public Action OnSlip { get; set; }

    public bool IsRunning { get; private set; }
    public bool IsFalling { get; private set; }

    public void Update()
    {
        Move();
        TryJump();
        CheckSlip();
    }

    private void Move()
    {
        rigidbody.angularVelocity = Vector3.zero;
        var input = inputHandler.GetMovementInput();
        moveDirection = new Vector3(input.x, 0, input.y) * config.MovementSpeed;

        if (moveDirection.magnitude > 0)
        {
            IsRunning = true;
            RotateTowards(moveDirection);
        }
        else
        {
            IsRunning = false;
        }

        if (!isGrounded && !inputHandler.IsJumping())
            IsFalling = true;
        else
            IsFalling = false;

        moveDirection.y = rigidbody.velocity.y;
        rigidbody.velocity = moveDirection + additionalMovement;
    }

    private void CheckSlip()
    {
        if (slipped) return;

        if (rigidbody.transform.position.y < -5f)
        {
            OnSlip?.Invoke();
            slipped = true;
        }
    }

    public void SetAdditionalForce(Vector3 force)
    {
        additionalMovement = force;
    }

    private void TryJump()
    {
        if (isGrounded && inputHandler.IsJumping())
        {
            OnJump?.Invoke();
            rigidbody.AddForce(Vector3.up * config.JumpHeight, ForceMode.Impulse);
        }
    }

    private void RotateTowards(Vector3 moveDirection)
    {
        var targetRotation = Quaternion.LookRotation(moveDirection);
        rigidbody.rotation = Quaternion.Slerp(characterTransform.rotation, targetRotation,
            config.RotationSpeed * Time.deltaTime);
    }

    ~CharacterMovement()
    {
        OnGrounded = null;
        OnSlip = null;
        OnJump = null;
    }
}