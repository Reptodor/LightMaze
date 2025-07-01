using System;

public class AbilityModel
{
    private readonly float _abilityDuration;
    private float _remainingAbilityDuration;

    public bool IsAbilityActive { get; private set; }
    public float CooldownDuration { get; private set; }
    public float CurrentCooldown { get; private set; }

    public event Action AbilityIsOver;

    public AbilityModel(float abilityDuration, float cooldownDuration)
    {
        _abilityDuration = abilityDuration;
        CooldownDuration = cooldownDuration;
    }

    public void ActivateAbility()
    {
        if (CurrentCooldown <= 0)
        {
            IsAbilityActive = true;
            _remainingAbilityDuration = _abilityDuration;
            CurrentCooldown = CooldownDuration;
        }
    }

    public void Update(float deltaTime)
    {
        if (IsAbilityActive)
        {
            _remainingAbilityDuration -= deltaTime;

            if (_remainingAbilityDuration <= 0)
            {
                IsAbilityActive = false;
                AbilityIsOver?.Invoke();
            }
        }
        else if (CurrentCooldown > 0)
        {
            CurrentCooldown -= deltaTime;
        }
    }
}
