using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthConfig", menuName = "Configs/HealthConfig")]
public class HealthConfig : ScriptableObject
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _maxValue;
    [SerializeField] private float _deathTime;

    public AudioSource AudioSource => _audioSource;
    public int MaxValue => _maxValue;
    public float DeathTime => _deathTime;

    private void OnValidate()
    {
        if (_audioSource == null)
            throw new ArgumentNullException(nameof(_audioSource), "Audio source cannot be null");

        if(_maxValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(_maxValue), "Max value must be greater than zero");

        if(_deathTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(_deathTime), "Death time must be greater than zero");
    }
}
