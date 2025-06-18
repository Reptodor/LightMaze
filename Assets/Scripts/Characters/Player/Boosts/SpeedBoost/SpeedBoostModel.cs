public class SpeedBoostModel
{
    private float _boostDuration;

    public bool IsBoostActive { get; private set; }
    public float CooldownDuration { get; private set; }
    public float CurrentCooldown { get; private set; }

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
            CurrentCooldown = CooldownDuration;
        }
    }

    public void Update(float deltaTime)
    {
        if (IsBoostActive)
        {
            _boostDuration -= deltaTime;
            if (_boostDuration <= 0)
            {
                IsBoostActive = false;
                _boostDuration = 3.0f;
            }
        }
        else if (CurrentCooldown > 0)
        {
            CurrentCooldown -= deltaTime;
        }
    }
}
