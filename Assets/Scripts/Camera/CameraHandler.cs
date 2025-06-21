using System;
using DG.Tweening;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CameraHandlerConfig _cameraHandlerConfig;
    [SerializeField] private Camera _camera;

    private CameraMovementHandler _cameraMovementHandler;
    private CameraFollowingHandler _cameraFollowingHandler;
    private Sequence _animation;
    private bool _isInitialized = false;

    private const float _punchForce = 0.4f;
    private const float _punchAnimationDuration = 0.05f;

    private void OnValidate()
    {
        if (_camera == null)
            throw new ArgumentNullException(nameof(_camera), "Camera component cannot be null");

        if (_cameraHandlerConfig == null)
            throw new ArgumentNullException(nameof(_cameraHandlerConfig), "Camera handler config cannot be null");
    }

    public void Initialize(Player player, Vector3 cameraFreePosition, float cameraUnfollowingOrthoSize)
    {
        _cameraMovementHandler = new CameraMovementHandler(this, player, _cameraHandlerConfig.CameraMovementConfig);
        _cameraFollowingHandler = new CameraFollowingHandler(_camera, player, _cameraHandlerConfig.CameraFollowingConfig, _cameraMovementHandler, cameraFreePosition, cameraUnfollowingOrthoSize);
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
    }

    public void OnCameraStateSwitchKeyPressed()
    {
        _cameraFollowingHandler.Switch();
    }

    public void Shake()
    {
        _animation = DOTween.Sequence();

        _animation.Append(transform.DOPunchPosition(Vector2.right * _punchForce, _punchAnimationDuration));
    }
}
