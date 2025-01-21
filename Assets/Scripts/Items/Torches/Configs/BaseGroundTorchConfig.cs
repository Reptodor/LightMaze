using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseGroundTorchConfig", menuName = "Configs/Torches/Ground/BaseGroundTorch")]
public class BaseGroundTorchConfig : ScriptableObject
{
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private float _flameIntensity = 1;

    public float AppearanceDuration => _appearanceDuration;
    public float FlameIntensity => _flameIntensity;

    public virtual void OnValidate()
    {
        if(_appearanceDuration < 0)
            throw new ArgumentOutOfRangeException(nameof(_appearanceDuration), "Appearance duration cannot be below zero");

        if(_flameIntensity < 0)
            throw new ArgumentOutOfRangeException(nameof(_flameIntensity), "Flame intensity cannot be below zero");
    }
}
