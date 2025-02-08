using DG.Tweening;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private CameraHandlerConfig _cameraHandlerConfig;
    private CameraMovementHandler _cameraMovementHandler;
    private CameraFollowingHandler _cameraFollowingHandler;
    private Sequence _animation;

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

    private void OnDisable()
    {
        _animation.Kill();
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

    public void Switch()
    {
        _cameraFollowingHandler.Switch();
    }

    public void Shake()
    {
        _animation = DOTween.Sequence();

        _animation.Append(transform.DOPunchPosition(Vector2.right * 0.4f, 0.05f));
    }
}
