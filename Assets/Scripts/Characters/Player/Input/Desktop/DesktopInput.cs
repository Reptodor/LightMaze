using System;
using UnityEngine;

public class DesktopInput : IInput
{
    private readonly DesktopInputConfig _desktopInputConfig;
    private Vector2 _moveDirection;

    public event Action SpeedBoostKeyPressed;
    public event Action FlameBoostKeyPressed;

    public DesktopInput(DesktopInputConfig desktopInputConfig)
    {
        _desktopInputConfig = desktopInputConfig;
    }

    public void Update()
    {
        if (Input.GetKeyDown(_desktopInputConfig.SpeedBoostKey))
            SpeedBoostKeyPressed?.Invoke();

        if (Input.GetKeyDown(_desktopInputConfig.FlameBoostKey))
            FlameBoostKeyPressed?.Invoke();
    }

    public Vector2 GetMoveDirection()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        return _moveDirection;
    }

}
