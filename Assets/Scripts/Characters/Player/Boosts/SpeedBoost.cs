using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private BoostConfig _boostConfig;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _movementAudioSource;

    private MovementHandler _playerMovementHandler;
    private Sequence _animation;
    private Sequence _cooldownAnimation;
    private float _defaultSpeed;
    private float _defaultPitch;
    private bool _canUse = true;
    private bool _isInitialized;

    private const string _animatorParameterName = "SpeedBoost";
    private const float _animatorParameterBaseValue = 1f;

    private void OnValidate()
    {
        if (_boostConfig == null)
            throw new ArgumentNullException(nameof(_boostConfig), "Boost config cannot be null");

        if (_cooldownImage == null)
            throw new ArgumentNullException(nameof(_cooldownImage), "Cooldown image cannot be null");

        if (_animator == null)
            throw new ArgumentNullException(nameof(_animator), "Animator cannot be null");

        if (_movementAudioSource == null)
            throw new ArgumentNullException(nameof(_movementAudioSource), "Movement audio source cannot be null");
    }

    public void Initialize(MovementHandler playerMovementHandler)
    {
        _playerMovementHandler = playerMovementHandler;
        _defaultSpeed = _playerMovementHandler.Speed;
        _defaultPitch = _movementAudioSource.pitch;

        _isInitialized = true;
    }

    private void OnDisable()
    {
        _animation.Kill();
        _cooldownAnimation.Kill();
    }

    private void Update()
    {
        if(!_isInitialized)
            return;

        if(_cooldownImage.fillAmount <= 0.15f)
        {
            _canUse = true;
        }
    }

    public void Use()
    {
        if(!_canUse)
            return;

        _cooldownAnimation.Kill();

        _animation = DOTween.Sequence();

        _animation.AppendCallback(() => SetValues())
                  .JoinCallback(() => SetCooldown())
                  .AppendInterval(_boostConfig.Duration)
                  .AppendCallback(() => ResetValues())
                  .AppendCallback(() => AnimateCooldown());
    }

    private void SetValues()
    {
        _playerMovementHandler.SetSpeed(_defaultSpeed * _boostConfig.BoostPercent);
        _animator.SetFloat(_animatorParameterName, _boostConfig.BoostPercent);
        _movementAudioSource.pitch = _defaultPitch * _boostConfig.BoostPercent;
    }

    private void ResetValues()
    {
        _playerMovementHandler.SetSpeed(_defaultSpeed);
        _animator.SetFloat(_animatorParameterName, _animatorParameterBaseValue);
        _movementAudioSource.pitch = _defaultPitch;
    }

    private void SetCooldown()
    {
        _cooldownImage.fillAmount = 1;
        _canUse = false;
    }

    private void AnimateCooldown()
    {
        _cooldownAnimation = DOTween.Sequence();

        _cooldownAnimation.Append(DOTween.To(SetImageFillAmount, 1, 0, _boostConfig.Cooldown));
    }

    private void SetImageFillAmount(float value)
    {
        _cooldownImage.fillAmount = value;
    }
}
