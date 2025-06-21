using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraHandlerConfig", menuName = "Configs/Camera/CameraHandler")]
public class CameraHandlerConfig : ScriptableObject
{
    [SerializeField] private CameraMovementConfig _cameraMovementConfig;
    [SerializeField] private CameraFollowingConfig _cameraFollowingConfig;

    public CameraMovementConfig CameraMovementConfig => _cameraMovementConfig;
    public CameraFollowingConfig CameraFollowingConfig => _cameraFollowingConfig;

    private void OnValidate()
    {
        if(_cameraMovementConfig == null)
            throw new ArgumentNullException(nameof(_cameraMovementConfig), "Camera movement config cannot be null");

        if(_cameraFollowingConfig == null)
            throw new ArgumentNullException(nameof(_cameraFollowingConfig), "Camera following config cannot be null");
    }
}
