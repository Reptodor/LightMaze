using UnityEngine;

public class PlayerInputHandler
{
    private float _horizontalInput;
    private float _verticalInput;

    public Vector2 GetInputDirection()
    {
        GetInput();
        Vector2 moveDirection = new Vector2(_horizontalInput, _verticalInput);

        return moveDirection;
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
}
