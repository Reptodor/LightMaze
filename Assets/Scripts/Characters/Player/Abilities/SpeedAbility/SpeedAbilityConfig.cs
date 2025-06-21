using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedAbilityConfig", menuName = "Configs/Abilities/Speed")]
public class SpeedAbilityConfig : AbilityConfig
{
    [SerializeField] private float _boostPercent;

    public float BoostPercent => _boostPercent;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (_boostPercent <= 0)
            throw new ArgumentOutOfRangeException(nameof(_boostPercent), "Boost percent must be greater than null");
    }
}
