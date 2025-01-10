using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Health
{
    private HealthConfig _healthConfig;
    private HealthView _healthView;
    private HealthModel _healthModel;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _coroutine;
    private bool _isAlive = true;

    public bool IsAlive => _isAlive;

    public event Action Died;

    public Health(HealthConfig healthConfig, HealthView healthView, SpriteRenderer spriteRenderer)
    {
        if (healthConfig == null)
            throw new ArgumentNullException(nameof(healthConfig), "Health config cannot be null");

        if(healthView == null)
            throw new ArgumentNullException(nameof(healthView), "Health view cannot be null`");

        if(spriteRenderer == null)
            throw new ArgumentNullException(nameof(spriteRenderer), "Sprite renderer cannot be null");

        _healthView = healthView;
        _healthConfig = healthConfig;
        _spriteRenderer = spriteRenderer;
        _healthModel = new HealthModel(healthConfig);
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
        
        // _healthConfig.AudioSource?.Play();
        AnimateHit();
        _healthModel.ReduceValue(damage);

        if(_healthModel.CurrentValue <= 0)
            _coroutine = Coroutines.StartRoutine(Die());
    }

    public void AnimateHit()
    {
        Sequence animation = DOTween.Sequence();

        animation.Append(_spriteRenderer.DOColor(Color.red, _healthConfig.HitAnimationDuration)).
                 Append(_spriteRenderer.DOColor(Color.white, _healthConfig.HitAnimationDuration));

    }

    private IEnumerator Die()
    {
        _isAlive = false;
        Died?.Invoke();

        yield return new WaitForSeconds(_healthConfig.DeathTime);

        Coroutines.StopRoutine(_coroutine);
    }
}
