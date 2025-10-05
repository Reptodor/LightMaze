using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthConfig", menuName = "Configs/Characters/HealthConfig")]
public class HealthConfig : ScriptableObject
{
    [SerializeField] private AudioClip _healAudioClip;
    [SerializeField] private AudioClip _damageAudioClip;
    [SerializeField] private Color _healEffectColor = Color.green;
    [SerializeField] private Color _damageEffectColor = Color.red;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _deathTime;
    [SerializeField] private float _hitAnimationDuration;

    public AudioClip HealAudioClip => _healAudioClip;
    public AudioClip DamageAudioClip => _damageAudioClip;
    public Color HealEffectColor => _healEffectColor;
    public Color DamageEffectColor => _damageEffectColor;
    public int MaxHealth => _maxHealth;
    public float DeathTime => _deathTime;
    public float HitAnimationDuration => _hitAnimationDuration;

    private void OnValidate()
    {
        if (_healAudioClip == null)
            throw new ArgumentNullException(nameof(_healAudioClip), "HealAudioClip cannot be null");

        if (_damageAudioClip == null)
            throw new ArgumentNullException(nameof(_damageAudioClip), "DamageAudioClip cannot be null");

        if (_maxHealth <= 0)
            throw new ArgumentOutOfRangeException(nameof(_maxHealth), "Max health must be greater than zero");

        if(_deathTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(_deathTime), "Death time must be greater than zero");

        if(_hitAnimationDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_hitAnimationDuration), "Hit animation duration must be greater than null");
    }
}
