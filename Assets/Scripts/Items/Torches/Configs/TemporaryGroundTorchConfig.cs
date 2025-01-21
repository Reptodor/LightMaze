using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTemporaryGroundTorchConfig", menuName = "Configs/Torches/Ground/TemporaryGroundTorch")]
public class TemporaryGroundTorchConfig : BaseGroundTorchConfig
{
    [SerializeField] private float _lifeTime;

    public float LifeTime => _lifeTime;

    public override void OnValidate()
    {
        base.OnValidate();

        if(_lifeTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(_lifeTime), "Life time must be greater than zero");
    }
}
