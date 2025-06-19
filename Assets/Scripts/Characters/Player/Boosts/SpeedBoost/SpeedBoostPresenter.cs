using UnityEngine;

public class SpeedBoostPresenter
{
    private SpeedBoostView _view;
    private SpeedBoostModel _model;
    private MovementHandler _playerMovementHandler;

    private float _boostPercent;
    private float _defaultSpeed;
    private float _currentBoostPercent;

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
        _model.BoostIsOver += OnBoostIsOver;
    }

    public void Unsubscribe()
    {
        _view.BoostRequested -= OnBoostRequested;
        _model.BoostIsOver -= OnBoostIsOver;
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

        if (_model.IsBoostActive)
        {
            _currentBoostPercent = _boostPercent;
        }

        ApplyBoostPercent();
    }

    private void OnBoostIsOver()
    {
        _currentBoostPercent = _baseBoostPercent;

        ApplyBoostPercent();
    }

    private void ApplyBoostPercent()
    {
        _view.PlayRunSoundWithBoostPercent(_currentBoostPercent);
        _view.PlayRunAnimationWithBoostPercent(_currentBoostPercent);
        SetRunSpeedWithBoostPercent(_currentBoostPercent);
    }

    private void SetRunSpeedWithBoostPercent(float boostPercent)
    {
        _playerMovementHandler.SetSpeed(_defaultSpeed * boostPercent);
    }
}
