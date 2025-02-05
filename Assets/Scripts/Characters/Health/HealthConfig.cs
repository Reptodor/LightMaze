using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthConfig", menuName = "Configs/Characters/HealthConfig")]
public class HealthConfig : ScriptableObject
{
    [SerializeField] private int _maxValue;
    [SerializeField] private float _deathTime;
    [SerializeField] private float _hitAnimationDuration;

    public int MaxValue => _maxValue;
    public float DeathTime => _deathTime;
    public float HitAnimationDuration => _hitAnimationDuration;

    private void OnValidate()
    {
        if(_maxValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(_maxValue), "Max value must be greater than zero");

        if(_deathTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(_deathTime), "Death time must be greater than zero");

        if(_hitAnimationDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_hitAnimationDuration), "Hit animation duration must be greater than null");
    }
}
