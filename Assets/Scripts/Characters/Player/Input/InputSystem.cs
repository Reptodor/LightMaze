using System;
using UnityEngine;

public class InputSystem
{
    private readonly InputConfig _inputConfig;
    private Vector2 _moveDirection;

    public event Action SpeedBoostKeyPressed;
    public event Action FlameBoostKeyPressed;

    public KeyCode SpeedBoostKey => _inputConfig.SpeedBoostKey;

    public InputSystem(InputConfig inputConfig)
    {
        _inputConfig = inputConfig;
    }

    public void Update()
    {
        if (Input.GetKeyDown(_inputConfig.SpeedBoostKey))
            SpeedBoostKeyPressed?.Invoke();

        if (Input.GetKeyDown(_inputConfig.FlameBoostKey))
            FlameBoostKeyPressed?.Invoke();
    }

    public Vector2 GetMoveDirection()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        return _moveDirection;
    }

}
