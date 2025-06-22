using UnityEngine;

public class CameraMovementHandler
{
    private CameraHandler _camera;
    private Player _player;
    private CameraMovementConfig _cameraMovementConfig;
    private bool _isFollowing = true;
    
    public CameraMovementHandler(CameraHandler cameraHandler, Player player, CameraMovementConfig cameraMovementConfig)
    {
        _camera = cameraHandler;
        _player = player;
        _cameraMovementConfig = cameraMovementConfig;
    }

    public void SetFollowing(bool isFollowing)
    {
        _isFollowing = isFollowing;
    }

    public void HandleMovement()
    {
        if(!_isFollowing)
            return;

        _camera.transform.position = Vector3.Lerp(_camera.transform.position, GetNextPosition(), _cameraMovementConfig.SmoothFactor * Time.deltaTime);
    }

    private Vector3 GetNextPosition()
    {
        Vector3 position = _player.transform.position + _cameraMovementConfig.Offset;
        
        return position;
    }
}
