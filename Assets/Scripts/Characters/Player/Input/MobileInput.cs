using UnityEngine;

public class MobileInput : IInput
{
    private readonly Joystick _joystick;

    public MobileInput(Joystick joystick)
    {
        _joystick = joystick;
    }

    public Vector2 GetMoveDirection()
    {
        Vector2 moveDirection = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        return moveDirection;
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
