using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [Header("Health")]
    private Health _health;

    public Health Health => _health;

    public void Initialize(HealthConfig healthConfig, HealthView healthView)
    {
        if(healthConfig == null)
            throw new ArgumentNullException(nameof(healthConfig), "Health config cannot be null");

        if(healthView == null)
            throw new ArgumentNullException(nameof(healthView), "Health view cannot be null");  

        _health = new Health(healthConfig, healthView);
    }

    private void OnEnable()
    {
        _health.Subscribe();
    }

    private void OnDisable()
    {
        _health.Unsubscribe();
    }
}
