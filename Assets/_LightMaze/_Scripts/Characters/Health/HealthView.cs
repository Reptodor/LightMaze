using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private Image _barFilling;

    private CameraShake _cameraShake;
    private Color _effectColor = Color.white;
    private Sequence _animation; 

    private void OnValidate()
    {
        if (_playerSpriteRenderer == null)
            throw new ArgumentNullException(nameof(_playerSpriteRenderer), "PlayerSpriteRenderer cannot be null");

        if (_audioSource == null)
            throw new ArgumentNullException(nameof(_audioSource), "AudioSource cannot be null");

        if (_healthConfig == null)
            throw new ArgumentNullException(nameof(_healthConfig), "HealthConfig cannot be null");

        if (_barFilling == null)
            throw new ArgumentNullException(nameof(_barFilling), "HealthBarFilling cannot be null");
    }

    public void Initialize(CameraShake cameraShake)
    {
        _cameraShake = cameraShake;
    }

    public void OnHealthChanged(float healthPercentage, string changeTypeName)
    {
        switch (changeTypeName)
        {
            case "Damage":
                _audioSource.clip = _healthConfig.DamageAudioClip;
                _effectColor = _healthConfig.DamageEffectColor;
                _cameraShake.ShakeCamera();
                break;
            case "Heal":
                _audioSource.clip = _healthConfig.HealAudioClip;
                _effectColor = _healthConfig.HealEffectColor;
                break;
        }

        UpdateHealthUI(healthPercentage);
        PlaySound();
        ShowEffect(_effectColor);
    }

    private void UpdateHealthUI(float healthPercentage)
    {
        _barFilling.fillAmount = healthPercentage;
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }

    private void ShowEffect(Color effectColor)
    {
        _animation = DOTween.Sequence();

        _animation.Append(_playerSpriteRenderer.DOColor(effectColor, _healthConfig.HitAnimationDuration))
                  .Append(_playerSpriteRenderer.DOColor(Color.white, _healthConfig.HitAnimationDuration));
    }
}
