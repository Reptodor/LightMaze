using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoostHandler : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _movementAudioSource;
    private MovementHandler _playerMovementHandler;
    private Sequence _animation;
    private Sequence _cooldownAnimation;
    private BoostConfig _boostConfig;
    private float _defaultSpeed;
    private float _defaultPitch;
    private bool _canUse = true;
    private bool _isInitialized;

    public void Initialize(BoostConfig boostConfig, MovementHandler playerMovementHandler)
    {
        _playerMovementHandler = playerMovementHandler;
        _boostConfig = boostConfig;
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

        if(Input.GetKeyDown(_boostConfig.Key) && _canUse)
        {
            Use();
            _cooldownAnimation.Kill();
        }

        if(_cooldownImage.fillAmount <= 0.15f)
        {
            _canUse = true;
        }
    }

    public void Use()
    {
        if(!_canUse)
            return;

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
        _animator.SetFloat("SpeedBoost", _boostConfig.BoostPercent);
        _movementAudioSource.pitch = _defaultPitch * _boostConfig.BoostPercent;
    }

    private void ResetValues()
    {
        _playerMovementHandler.SetSpeed(_defaultSpeed);
        _animator.SetFloat("SpeedBoost", 1);
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
