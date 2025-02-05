using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoostConfig", menuName = "Configs/Boost")]
public class BoostConfig : ScriptableObject
{
    [SerializeField] private KeyCode _key;
    [SerializeField] private float _boostPercent;
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;

    public KeyCode Key => _key;
    public float BoostPercent => _boostPercent;
    public float Duration => _duration;
    public float Cooldown => _cooldown;

    private void OnValidate()
    {
        if(_key == KeyCode.None)
            throw new ArgumentOutOfRangeException(nameof(_key), "Key cannot be null");

        if(_boostPercent <= 0)
            throw new ArgumentOutOfRangeException(nameof(_boostPercent), "Range must be greater than null");
        
        if(_duration < 0)
            throw new ArgumentOutOfRangeException(nameof(_duration), "Duration cannot be below zero");

        if(_cooldown <= 0)
            throw new ArgumentOutOfRangeException(nameof(_cooldown), "Cooldown must be greater than null");
    }
}
