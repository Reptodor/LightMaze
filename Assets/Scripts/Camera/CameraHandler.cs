using System;
using DG.Tweening;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CameraMovementConfig _cameraMovementConfig;
    [SerializeField] private Camera _camera;

    private CameraMovementHandler _cameraMovementHandler;
    private Sequence _animation;
    private bool _isInitialized = false;

    private const float _punchForce = 0.4f;
    private const float _punchAnimationDuration = 0.05f;

    public CameraMovementHandler CameraMovementHandler => _cameraMovementHandler;
    public Camera Camera => _camera;

    private void OnValidate()
    {
        if (_camera == null)
            throw new ArgumentNullException(nameof(_camera), "Camera component cannot be null");

        if (_cameraMovementConfig == null)
            throw new ArgumentNullException(nameof(_cameraMovementConfig), "CameraMovementConfig cannot be null");
    }

    public void Initialize(Player player)
    {
        _cameraMovementHandler = new CameraMovementHandler(this, player, _cameraMovementConfig);

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

    public void Shake()
    {
        _animation = DOTween.Sequence();

        _animation.Append(transform.DOPunchPosition(Vector2.right * _punchForce, _punchAnimationDuration));
    }
}
