using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArrowConfig", menuName = "Configs/Traps/Arrow")]
public class ArrowConfig : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _pauseDuration = 2f;

    public int Damage => _damage;
    public float Speed => _speed;
    public float PauseDuration => _pauseDuration;

    private void OnValidate()
    {
        if (_damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(_damage), "Damage must be greater than zero");

        if (_speed <= 0)
            throw new ArgumentOutOfRangeException(nameof(_speed), "Speed must be greater than zero");

        if (_pauseDuration < 0)
            throw new ArgumentOutOfRangeException(nameof(_pauseDuration), "PauseDuration cannot be below zero");
    }
}
