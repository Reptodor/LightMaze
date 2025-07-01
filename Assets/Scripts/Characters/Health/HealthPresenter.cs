using System.Collections;
using UnityEngine;

public class HealthPresenter
{
    private HealthView _healthView;
    private HealthModel _healthModel;

    private SceneLoader _sceneLoader;
    private AnimationSwitchingHandler _animationSwitchingHandler;
    private Coroutine _coroutine;
    private float _deathTime;
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

        _sceneLoader.RestartSceneWithLoadingScreen(_sceneLoader.ScenesLoadingTimeConfig.GameplayScenesLoadingTime);
        Coroutines.StopRoutine(_coroutine);
    }
}
