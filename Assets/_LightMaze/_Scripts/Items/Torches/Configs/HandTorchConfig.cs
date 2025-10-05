using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHandTorchConfig", menuName = "Configs/Torches/HandTorche")]
public class HandTorchConfig : ScriptableObject
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _radius = 1;

    public float Speed => _speed;
    public float Radius => _radius;

    private void OnValidate()
    {
        if(_speed <= 0)
            throw new ArgumentOutOfRangeException(nameof(_speed), "Speed must be greater than null");

        if(_radius <= 0)
            throw new ArgumentOutOfRangeException(nameof(_radius), "Radius must be greater than null");
    }
}
