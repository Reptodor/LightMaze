using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private AudioSource _hitAudioSource;
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private Image _barFilling;

    private void OnValidate()
    {
        if (_playerSpriteRenderer == null)
            throw new ArgumentNullException(nameof(_playerSpriteRenderer), "PlayerSpriteRenderer cannot be null");

        if (_hitAudioSource == null)
            throw new ArgumentNullException(nameof(_hitAudioSource), "HitAudioSource cannot be null");

        if (_healthConfig == null)
            throw new ArgumentNullException(nameof(_healthConfig), "HealthConfig cannot be null");

        if (_cameraShake == null)
            throw new ArgumentNullException(nameof(_cameraShake), "Camera shake cannot be null");

        if (_barFilling == null)
                throw new ArgumentNullException(nameof(_barFilling), "HealthBarFilling cannot be null");
    }

    public void OnHealthChanged(float healthPercentage)
    {
        UpdateHealthUI(healthPercentage);
        DamageEffect();
        DamageSound();
    }

    private void UpdateHealthUI(float healthPercentage)
    {
        _barFilling.fillAmount = healthPercentage;
    }

    private void DamageSound()
    {
        _hitAudioSource.Play();
    }

    private void DamageEffect()
    {
        _cameraShake.ShakeCamera();
        Sequence animation = DOTween.Sequence();

        animation.Append(_playerSpriteRenderer.DOColor(Color.red, _healthConfig.HitAnimationDuration)).
                 Append(_playerSpriteRenderer.DOColor(Color.white, _healthConfig.HitAnimationDuration));
    }
}
