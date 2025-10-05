using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInputConfig", menuName = "Configs/Input")]
public class InputConfig : ScriptableObject
{
    [SerializeField] private KeyCode _speedAbilityKey;
    [SerializeField] private KeyCode _teleportAbilityKey;
    [SerializeField] private KeyCode _cameraAbilityKey;
    [SerializeField] private KeyCode _pauseMenuKey;

    public KeyCode SpeedAbilityKey => _speedAbilityKey;
    public KeyCode TeleportAbilityKey => _teleportAbilityKey;
    public KeyCode CameraAbilityKey => _cameraAbilityKey;
    public KeyCode PauseMenuKey => _pauseMenuKey;

    private void OnValidate()
    {
        if (_speedAbilityKey == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_speedAbilityKey), "SpeedAbilityKey cannot be none");

        if (_teleportAbilityKey == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_teleportAbilityKey), "TeleportAbilityKey cannot be none");

        if (_cameraAbilityKey == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_cameraAbilityKey), "CameraAbilityKey cannot be none");

        if (_pauseMenuKey == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_pauseMenuKey), "PauseMenuKey cannot be none");
    }
}
