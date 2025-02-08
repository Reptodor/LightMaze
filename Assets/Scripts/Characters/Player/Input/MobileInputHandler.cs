using UnityEngine;

public class MobileInputHandler : IInput
{
    private Joystick _joystick;

    public MobileInputHandler(Joystick joystick)
    {
        _joystick = joystick;
    }

    public Vector2 GetInputDirection()
    {
        Vector2 moveDirection = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        return moveDirection;
    }
}
