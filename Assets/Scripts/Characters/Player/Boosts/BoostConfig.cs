using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoostConfig", menuName = "Configs/Boost")]
public class BoostConfig : ScriptableObject
{
    [SerializeField] private float _boostPercent;
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;

    public float BoostPercent => _boostPercent;
    public float Duration => _duration;
    public float Cooldown => _cooldown;

    private void OnValidate()
    {
        if(_boostPercent <= 0)
            throw new ArgumentOutOfRangeException(nameof(_boostPercent), "Boost percent must be greater than null");
        
        if(_duration < 0)
            throw new ArgumentOutOfRangeException(nameof(_duration), "Duration cannot be below zero");

        if(_cooldown <= 0)
            throw new ArgumentOutOfRangeException(nameof(_cooldown), "Cooldown must be greater than null");
    }
}
