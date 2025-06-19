using System;

public class SpeedBoostModel
{
    private readonly float _boostDuration;
    private float _remainingBoostDuration;

    public bool IsBoostActive { get; private set; }
    public float CooldownDuration { get; private set; }
    public float CurrentCooldown { get; private set; }

    public event Action BoostIsOver;

    public SpeedBoostModel(float boostDuration, float cooldownDuration)
    {
        _boostDuration = boostDuration;
        CooldownDuration = cooldownDuration;
    }

    public void ActivateBoost()
    {
        if (CurrentCooldown <= 0)
        {
            IsBoostActive = true;
            _remainingBoostDuration = _boostDuration;
            CurrentCooldown = CooldownDuration;
        }
    }

    public void Update(float deltaTime)
    {
        if (IsBoostActive)
        {
            _remainingBoostDuration -= deltaTime;

            if (_remainingBoostDuration <= 0)
            {
                IsBoostActive = false;
                BoostIsOver?.Invoke();
            }
        }
        else if (CurrentCooldown > 0)
        {
            CurrentCooldown -= deltaTime;
        }
    }
}
