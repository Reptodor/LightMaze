using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private CameraHandlerConfig _cameraHandlerConfig;
    private CameraMovementHandler _cameraMovementHandler;
    private CameraFollowingHandler _cameraFollowingHandler;

    private bool _isInitialized = false;

    private void OnValidate()
    {
        if(_camera == null)
            _camera = GetComponent<Camera>();
    }

    public void Initialize(CameraHandlerConfig cameraHandlerConfig, Player player)
    {
        _cameraHandlerConfig = cameraHandlerConfig;
        _cameraMovementHandler = new CameraMovementHandler(this, player, _cameraHandlerConfig.CameraMovementConfig);
        _cameraFollowingHandler = new CameraFollowingHandler(_camera, player, _cameraHandlerConfig.CameraFollowingConfig, _cameraMovementHandler);
        _cameraFollowingHandler.Switch();

        _isInitialized = true;
    }

    private void Update()
    {
        if(!_isInitialized)
            return;

        _cameraMovementHandler.HandleMovement();

        if(Input.GetKeyDown(_cameraHandlerConfig.KeyCode))
        {
            _cameraFollowingHandler.Switch();
        }
    }
}
