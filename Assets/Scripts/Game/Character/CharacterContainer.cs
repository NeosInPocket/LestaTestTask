using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContainer : MonoBehaviour
{
    [SerializeField] private Rigidbody rigibody;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterMovementConfig characterMovementConfig;
    [SerializeField] private CapsuleCollider characterCollider;
    private CharacterView characterView;
    private InputHandler inputHandler;

    public CharacterMovement Movement { get; private set; }
    public CharacterState State { get; private set; }
    public Action OnWin { get; set; }

    private void Awake()
    {
        transform.position = Vector3.zero;
        transform.localPosition = new Vector3(0, characterCollider.height, 0);
        State = new CharacterState();

        inputHandler = new InputHandler();
        characterView = new CharacterView(animator);

        var subscribers = new List<Action>();
        subscribers.Add(characterView.OnLanding);

        Movement = new CharacterMovement(rigibody, inputHandler, characterMovementConfig);
        Movement.OnJump += characterView.OnJump;
        Movement.OnGrounded += characterView.OnLanding;
    }

    private void Update()
    {
        Movement.Update();
        characterView.UpdateView(Movement);
    }

    private void OnDestroy()
    {
        OnWin = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Movement.isGrounded = true;
        characterView.OnLanding();
    }

    private void OnCollisionExit(Collision collision)
    {
        Movement.isGrounded = false;
    }

    private void OnCollisionStay(Collision other)
    {
        Movement.isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WinTrigger trigger)) OnWin?.Invoke();
    }
}