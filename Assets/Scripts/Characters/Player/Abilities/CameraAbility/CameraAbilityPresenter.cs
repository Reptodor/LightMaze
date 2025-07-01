using Cinemachine;
using UnityEngine;

public class CameraAbilityPresenter
{
    private CameraAbilityView _view;
    private AbilityModel _model;
    private CinemachineVirtualCamera _unfollowingCinemachineVirtualCamera;


    public CameraAbilityPresenter(CameraAbilityView cameraAbilityView, AbilityModel cameraAbilityModel,
                                  CinemachineVirtualCamera unfollowingCinemachineVirtualCamera)
    {
        _view = cameraAbilityView;
        _model = cameraAbilityModel;
        _unfollowingCinemachineVirtualCamera = unfollowingCinemachineVirtualCamera;

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
        _unfollowingCinemachineVirtualCamera.Priority = 12;
    }

    private void OnAbilityIsOver()
    {
        _unfollowingCinemachineVirtualCamera.Priority = 8;
    }
}
