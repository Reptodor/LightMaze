using System;

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
        ValueChanged.Invoke(GetCurrentValuePercentage());
    }

    private float GetCurrentValuePercentage()
    {
        return _currentValue / _maxValue;
    }
}
