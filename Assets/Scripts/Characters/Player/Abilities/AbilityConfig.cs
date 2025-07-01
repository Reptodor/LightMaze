using System;
using UnityEngine;

public class AbilityConfig : ScriptableObject
{
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;

    public float Duration => _duration;
    public float Cooldown => _cooldown;

    protected virtual void OnValidate()
    {
        if(_duration < 0)
            throw new ArgumentOutOfRangeException(nameof(_duration), "Duration cannot be below zero");

        if(_cooldown <= 0)
            throw new ArgumentOutOfRangeException(nameof(_cooldown), "Cooldown must be greater than null");
    }
}
