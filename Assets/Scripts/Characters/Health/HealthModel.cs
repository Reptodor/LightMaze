using System;
using UnityEngine;

public class HealthModel
{
    private HealthConfig _healthConfig;
    private int _maxValue;
    private int _currentValue;

    public int CurrentValue => _currentValue;

    public event Action<float> ValueChanged;

    public HealthModel(HealthConfig healthConfig)
    {
        if (healthConfig == null)
            throw new ArgumentNullException(nameof(healthConfig), "Health config cannot be null");

        _healthConfig = healthConfig;
        _maxValue = _healthConfig.MaxValue;
        _currentValue = _maxValue;
    }

    public void ReduceValue(int damage)
    {
        _currentValue -= damage;
        Debug.Log($"{_currentValue},{_maxValue},{GetCurrentValuePercentage()}");
        ValueChanged?.Invoke(GetCurrentValuePercentage());
    }

    private float GetCurrentValuePercentage()
    {
        return (float)_currentValue / _maxValue;
    }
}