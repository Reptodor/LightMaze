using DG.Tweening;
using UnityEngine;

public class CameraAbilityPresenter
{
    private CameraAbilityView _view;
    private AbilityModel _model;
    private CameraAbilityConfig _config;

    private Player _player;
    private Vector3 _cameraFreePosition;
    private Camera _camera;
    private CameraMovementHandler _cameraMovementHandler;
    private float _cameraUnfollowingOrthoSize;
    private bool _isFollowing;

    public CameraAbilityPresenter(CameraAbilityView cameraAbilityView, AbilityModel speedAbilityModel, CameraAbilityConfig cameraAbilityConfig,
                                  Player player, Vector3 cameraFreePosition, Camera camera, CameraMovementHandler cameraMovementHandler,
                                  float cameraUnfollowingOrthoSize)
    {
        _view = cameraAbilityView;
        _model = speedAbilityModel;
        _config = cameraAbilityConfig;

        _player = player;
        _cameraFreePosition = cameraFreePosition;
        _camera = camera;
        _cameraMovementHandler = cameraMovementHandler;
        _cameraUnfollowingOrthoSize = cameraUnfollowingOrthoSize;
    }

    public void Subscribe()
    {
        _view.AbilityRequested += OnAbilityRequested;
        _model.AbilityIsOver += OnAbilityIsOver;
    }

    public void Unsubscribe()
    {
        _view.AbilityRequested -= OnAbilityRequested;
        _model.AbilityIsOver -= OnAbilityIsOver;
    }

    public void Update()
    {
        _model.Update(Time.deltaTime);

        if (!_model.IsAbilityActive && _model.CurrentCooldown > 0)
        {
            float cooldownProgress = _model.CurrentCooldown / _model.CooldownDuration;
            _view.UpdateCooldownVisual(cooldownProgress);
        }
    }

    private void OnAbilityRequested()
    {
        _model.ActivateAbility();
        _view.ShowCooldownVisual();

        if (_model.IsAbilityActive)
        {
            UseAbility();
        }
    }

    private void UseAbility()
    {
        _isFollowing = false;
        Sequence animation = DOTween.Sequence();

        animation.Append(_camera.transform.DOMove(_cameraFreePosition, _config.AnimationDuration))
                  .Join(_camera.DOOrthoSize(_cameraUnfollowingOrthoSize, _config.AnimationDuration))
                  .AppendCallback(() => _isFollowing = false)
                  .AppendCallback(() => _cameraMovementHandler.SetFollowing(_isFollowing));
    }

    private void OnAbilityIsOver()
    {
        _isFollowing = true;
        Sequence animation = DOTween.Sequence();

        animation.Append(_camera.transform.DOMove(GetEndPosition(), _config.AnimationDuration))
                  .Join(_camera.DOOrthoSize(_config.FollowingOrthoSize, _config.AnimationDuration))
                  .AppendCallback(() => _isFollowing = true)
                  .AppendCallback(() => _cameraMovementHandler.SetFollowing(_isFollowing));

    }

    private Vector3 GetEndPosition()
    {
        Vector3 endPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, _cameraFreePosition.z);

        return endPosition;
    }
}
