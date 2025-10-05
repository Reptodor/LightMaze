using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShakeAnimationConfig", menuName = "Configs/ShakeAnimation")]
public class ShakeAnimationConfig : ScriptableObject
{
    [SerializeField] private float _forceAmount = 0.10f;
    [SerializeField] private float _duration = 0.4f;
    [SerializeField] private float _interval = 3f;
    
    public float ForceAmount => _forceAmount;
    public float Duration => _duration;
    public float Interval => _interval;

    private void OnValidate()
    {
        if(_forceAmount == 0)
            throw new ArgumentOutOfRangeException(nameof(_forceAmount), "Force amount cannot be null");

        if(_duration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_duration), "Duration must be greater than null");

        if(_interval < 0)
            throw new ArgumentOutOfRangeException(nameof(_interval), "Interval cannot be below zero");
    }
}
