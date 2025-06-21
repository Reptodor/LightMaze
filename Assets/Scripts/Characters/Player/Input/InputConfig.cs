using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInputConfig", menuName = "Configs/Input")]
public class InputConfig : ScriptableObject
{
    [SerializeField] private KeyCode _speedAbilityKey;
    [SerializeField] private KeyCode _teleportAbilityKey;
    [SerializeField] private KeyCode _cameraStateSwitchKey;

    public KeyCode SpeedAbilityKey => _speedAbilityKey;
    public KeyCode TeleportAbilityKey => _teleportAbilityKey;
    public KeyCode CameraStateSwitchKey => _cameraStateSwitchKey;

    private void OnValidate()
    {
        if (_speedAbilityKey == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_speedAbilityKey), "SpeedAbilityKey cannot be none");

        if (_teleportAbilityKey == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_teleportAbilityKey), "TeleportAbilityKey cannot be none");

        if (_cameraStateSwitchKey == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_cameraStateSwitchKey), "CameraStateSwitchKey cannot be none");
    }
}
