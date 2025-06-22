using System;
using UnityEngine;

public class InputSystem
{
    private readonly InputConfig _inputConfig;
    private Vector2 _moveDirection;

    public event Action SpeedAbilityKeyPressed;
    public event Action TeleportAbilityKeyPressed;
    public event Action CameraAbilityKeyPressed;

    public KeyCode SpeedAbilityKey => _inputConfig.SpeedAbilityKey;
    public KeyCode TeleportAbilityKey => _inputConfig.TeleportAbilityKey;
    public KeyCode CameraAbilityKey => _inputConfig.CameraAbilityKey;

    public InputSystem(InputConfig inputConfig)
    {
        _inputConfig = inputConfig;
    }

    public void Update()
    {
        if (Input.GetKeyDown(_inputConfig.SpeedAbilityKey))
            SpeedAbilityKeyPressed?.Invoke();

        if (Input.GetKeyDown(_inputConfig.TeleportAbilityKey))
            TeleportAbilityKeyPressed?.Invoke();

        if (Input.GetKeyDown(_inputConfig.CameraAbilityKey))
            CameraAbilityKeyPressed?.Invoke();
    }

    public Vector2 GetMoveDirection()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        return _moveDirection;
    }

}
