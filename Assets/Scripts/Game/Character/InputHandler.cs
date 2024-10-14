using UnityEngine;

public class InputHandler
{
    public Vector2 GetMovementInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public bool IsJumping()
    {
        return Input.GetButtonDown("Jump");
    }
}