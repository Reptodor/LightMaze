using System;

public class HealthModel
{
    private int _maxHealth;
    private int _currentHealth;

    public int CurrentHealth => _currentHealth;

    public event Action<float, string> HealthChanged;
    public event Action Died;

    public HealthModel(int maxValue)
    {
        _maxHealth = maxValue;
        _currentHealth = _maxHealth;
    }

    public void Heal(int healAmount)
    {
        if (healAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(healAmount), "HealAmount must be greater than 0");

        _currentHealth += healAmount;
        HealthChanged?.Invoke(GetCurrentHealthPercentage(), "Heal");

        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0 || damage == 0)
            return;

        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Damage cannot be below zero");

        _currentHealth -= damage;
        HealthChanged?.Invoke(GetCurrentHealthPercentage(), "Damage");

        if (_currentHealth <= 0)
            Died?.Invoke();
    }

    private float GetCurrentHealthPercentage()
    {
        return (float)_currentHealth / _maxHealth;
    }
}