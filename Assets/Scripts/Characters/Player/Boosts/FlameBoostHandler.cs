using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

public class FlameBoostHandler : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;
    private Light2D _flame;
    private Sequence _animation;
    private Sequence _cooldownAnimation;
    private BoostConfig _boostConfig;
    private float _defaultOuter;
    private bool _canUse = true;
    private bool _isInitialized;

    public void Initialize(BoostConfig boostConfig, HandTorch handTorch)
    {
        _flame = handTorch.Flame;
        _boostConfig = boostConfig;
        _defaultOuter = _flame.pointLightOuterRadius;

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

        _animation.Append(DOTween.To(SetOuter, _defaultOuter, _defaultOuter * _boostConfig.BoostPercent, 1f))
                  .JoinCallback(() => SetCooldown())
                  .AppendInterval(_boostConfig.Duration)
                  .Append(DOTween.To(SetOuter, _defaultOuter * _boostConfig.BoostPercent, _defaultOuter, 1f))
                  .AppendCallback(() => AnimateCooldown());
    }

    private void SetOuter(float value)
    {
        _flame.pointLightOuterRadius = value;
    }

    private void SetCooldown()
    {
        _cooldownImage.fillAmount = 1;
        _canUse = false;
    }

    public void AnimateCooldown()
    {
        _cooldownAnimation = DOTween.Sequence();

        _cooldownAnimation.Append(DOTween.To(SetImageFillAmount, 1, 0, _boostConfig.Cooldown));
    }

    private void SetImageFillAmount(float value)
    {
        _cooldownImage.fillAmount = value;
    }
}
