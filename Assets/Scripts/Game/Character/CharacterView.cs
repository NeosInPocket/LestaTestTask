using UnityEngine;

public class CharacterView
{
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Landing = Animator.StringToHash("Landing");
    private readonly Animator animator;

    public CharacterView(Animator animator)
    {
        this.animator = animator;
    }

    public void UpdateView(CharacterMovement characterMovement)
    {
        if (characterMovement.IsRunning)
            animator.SetBool(IsRunning, true);
        else
            animator.SetBool(IsRunning, false);
    }

    public void OnLanding()
    {
        animator.SetTrigger(Landing);
    }

    public void OnJump()
    {
        animator.SetTrigger(Jump);
    }
}