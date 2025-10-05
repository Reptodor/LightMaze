using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTeleportAbilityConfig", menuName = "Configs/Abilities/Teleport")]
public class TeleportAbilityConfig : AbilityConfig
{
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _teleportDistance;

    public LayerMask ObstacleLayer => _obstacleLayer;
    public float TeleportDistance => _teleportDistance;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (_teleportDistance <= 0)
            throw new ArgumentOutOfRangeException(nameof(_teleportDistance), "TeleportDistance must be greater than null");
    }
}
