using UnityEngine;

public class SpeedAbilityPresenter
{
    private SpeedAbilityView _view;
    private AbilityModel _model;
    private MovementHandler _playerMovementHandler;

    private float _boostPercent;
    private float _defaultSpeed;
    private float _currentBoostPercent;

    private const float _baseBoostPercent = 1f;

    public SpeedAbilityPresenter(SpeedAbilityView speedAbilityView, AbilityModel speedAbilityModel,
                                 MovementHandler playerMovementHandler, float boostPercent)
    {
        _view = speedAbilityView;
        _model = speedAbilityModel;
        _boostPercent = boostPercent;

        _playerMovementHandler = playerMovementHandler;
        _defaultSpeed = _playerMovementHandler.Speed;
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
            _currentBoostPercent = _boostPercent;
        }

        ApplyBoostPercent();
    }

    private void OnAbilityIsOver()
    {
        _currentBoostPercent = _baseBoostPercent;

        ApplyBoostPercent();
    }

    private void ApplyBoostPercent()
    {
        _view.PlayRunSoundWithBoostPercent(_currentBoostPercent);
        _view.PlayRunAnimationWithBoostPercent(_currentBoostPercent);
        SetRunSpeedWithBoostPercent();
    }

    private void SetRunSpeedWithBoostPercent()
    {
        _playerMovementHandler.SetSpeed(_defaultSpeed * _currentBoostPercent);
    }
}
