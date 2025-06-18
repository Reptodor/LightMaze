using UnityEngine;

public class SpeedBoostPresenter
{
    private SpeedBoostView _view;
    private SpeedBoostModel _model;
    private MovementHandler _playerMovementHandler;

    private float _boostPercent;
    private float _defaultSpeed;

    private const float _baseBoostPercent = 1f;

    public SpeedBoostPresenter(SpeedBoostView speedBoostView, SpeedBoostModel speedBoostModel,
                               MovementHandler playerMovementHandler, float boostPercent)
    {
        _view = speedBoostView;
        _model = speedBoostModel;
        _boostPercent = boostPercent;

        _playerMovementHandler = playerMovementHandler;
        _defaultSpeed = _playerMovementHandler.Speed;
    }

    public void Subscribe()
    {
        _view.BoostRequested += OnBoostRequested;
    }

    public void Unsubscribe()
    {
        _view.BoostRequested -= OnBoostRequested;
    }

    public void Update()
    {
        _model.Update(Time.deltaTime);

        if (!_model.IsBoostActive && _model.CurrentCooldown > 0)
        {
            float cooldownProgress = _model.CurrentCooldown / _model.CooldownDuration;
            _view.UpdateCooldownVisual(cooldownProgress);
        }
    }

    private void OnBoostRequested()
    {
        _model.ActivateBoost();
        _view.ShowCooldownVisual();

        float currentBoostPercent;

        if (_model.IsBoostActive)
        {
            currentBoostPercent = _boostPercent;
        }
        else
        {
            currentBoostPercent = _baseBoostPercent;
        }

        _view.PlayRunSoundWithBoostPercent(currentBoostPercent);
        _view.PlayRunAnimationWithBoostPercent(currentBoostPercent);
        SetRunSpeedWithBoostPercent(currentBoostPercent);
    }

    private void SetRunSpeedWithBoostPercent(float boostPercent)
    {
        _playerMovementHandler.SetSpeed(_defaultSpeed * boostPercent);
    }
}
