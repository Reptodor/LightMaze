using System;

public class HealthModel
{
    private readonly int _maxHealth;

    public int CurrentHealth { get; private set; }

    public event Action<float, string> HealthChanged;
    public event Action Died;

    public HealthModel(int maxValue)
    {
        _maxHealth = maxValue;
        CurrentHealth = _maxHealth;
    }

    public void Heal(int healAmount)
    {
        if (healAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(healAmount), "HealAmount must be greater than 0");

        CurrentHealth += healAmount;
        HealthChanged?.Invoke(GetCurrentHealthPercentage(), "Heal");

        if (CurrentHealth >= _maxHealth)
            CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0 || damage == 0)
            return;

        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Damage cannot be below zero");

        CurrentHealth -= damage;
        HealthChanged?.Invoke(GetCurrentHealthPercentage(), "Damage");

        if (CurrentHealth <= 0)
            Died?.Invoke();
    }

    private float GetCurrentHealthPercentage()
    {
        return (float)CurrentHealth / _maxHealth;
    }
}