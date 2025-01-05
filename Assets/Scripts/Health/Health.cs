using System;
using System.Collections;
using UnityEngine;

public class Health
{
    private HealthConfig _healthConfig;
    private HealthView _healthView;
    private HealthModel _healthModel;
    private Coroutine _coroutine;
    private AudioSource _audioSource;
    private float _deathTime;

    public Health(HealthConfig healthConfig, HealthView healthView)
    {
        if (healthConfig == null)
            throw new ArgumentNullException(nameof(healthConfig), "Health config cannot be null");

        if(_healthView == null)
            throw new ArgumentNullException(nameof(healthView), "Health view cannot be null`");

        _healthView = healthView;
        _healthConfig = healthConfig;
        _healthModel = new HealthModel(healthConfig);

        _audioSource = _healthConfig.AudioSource;
        _deathTime = _healthConfig.DeathTime;
    }

    public void Subscribe()
    {
        _healthModel.ValueChanged += _healthView.Display;
    }

    public void Unsubscribe()
    {
        _healthModel.ValueChanged -= _healthView.Display;
    }

    public void RecieveDamage(int damage)
    {
        if(damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Damage must be greater than zero");
        
        _audioSource?.Play();
        _healthModel.ReduceValue(damage);

        if(_healthModel.CurrentValue <= 0)
            _coroutine = Coroutines.StartRoutine(Die());
    }

    private IEnumerator Die()
    {
        //Animation
        yield return new WaitForSeconds(_deathTime);

        Coroutines.StopRoutine(_coroutine);
    }
}
