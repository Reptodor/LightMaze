using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraHandlerConfig", menuName = "Configs/Camera/CameraHandler")]
public class CameraHandlerConfig : ScriptableObject
{
    [SerializeField] private CameraMovementConfig _cameraMovementConfig;
    [SerializeField] private CameraFollowingConfig _cameraFollowingConfig;
    [SerializeField] private  KeyCode _keyCode;

    public CameraMovementConfig CameraMovementConfig => _cameraMovementConfig;
    public CameraFollowingConfig CameraFollowingConfig => _cameraFollowingConfig;
    public KeyCode KeyCode => _keyCode;

    private void OnValidate()
    {
        if(_cameraMovementConfig == null)
            throw new ArgumentNullException(nameof(_cameraMovementConfig), "Camera movement config cannot be null");

        if(_cameraFollowingConfig == null)
            throw new ArgumentNullException(nameof(_cameraFollowingConfig), "Camera following config cannot be null");

        if(_keyCode == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_keyCode), "Key code cannot be none");
    }
}
