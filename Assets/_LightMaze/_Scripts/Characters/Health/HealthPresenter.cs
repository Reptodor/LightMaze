using System.Collections;
using LightMaze._Scripts.SceneLoader;
using UnityEngine;

public class HealthPresenter
{
    private readonly HealthView _healthView;
    private readonly HealthModel _healthModel;

    private readonly SceneLoader _sceneLoader;
    private readonly AnimationSwitchingHandler _animationSwitchingHandler;
    private readonly float _deathTime;
    
    private Coroutine _coroutine;
    private bool _isAlive = true;

    public bool IsAlive => _isAlive;

    public HealthPresenter(HealthModel healthModel, HealthView healthView, SceneLoader sceneLoader,
                           AnimationSwitchingHandler animationSwitchingHandler, float deathTime)
    {
        _healthModel = healthModel;
        _healthView = healthView;
        _sceneLoader = sceneLoader;
        _animationSwitchingHandler = animationSwitchingHandler;
        _deathTime = deathTime;
    }

    public void Subscribe()
    {
        _healthModel.HealthChanged += _healthView.OnHealthChanged;
        _healthModel.Died += OnDied;
    }

    public void Unsubscribe()
    {
        _healthModel.HealthChanged -= _healthView.OnHealthChanged;
        _healthModel.Died -= OnDied;
    }

    public void OnDamaged(int damage)
    {
        _healthModel.TakeDamage(damage);
    }

    public void OnHealed(int healAmount)
    {
        _healthModel.Heal(healAmount);
    }

    private void OnDied()
    {
        _coroutine = Coroutines.StartRoutine(Die());
    }

    private IEnumerator Die()
    {
        _isAlive = false;
        _animationSwitchingHandler.ChangeAnimation("Death");

        yield return new WaitForSeconds(_deathTime);

        _sceneLoader.RestartGameplayScene();
        Coroutines.StopRoutine(_coroutine);
    }
}
