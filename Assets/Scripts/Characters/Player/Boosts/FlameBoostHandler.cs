using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class FlameBoostHandler : MonoBehaviour
{
    [SerializeField] private BoostConfig _boostConfig;
    [SerializeField] private Image _cooldownImage;

    private Light2D _flame;
    private Sequence _animation;
    private Sequence _cooldownAnimation;
    private float _defaultOuter;
    private bool _canUse = true;
    private bool _isInitialized;
    
    private void OnValidate()
    {
        if (_boostConfig == null)
            throw new ArgumentNullException(nameof(_boostConfig), "Boost config cannot be null");

        if (_cooldownImage == null)
            throw new ArgumentNullException(nameof(_cooldownImage), "Cooldown image cannot be null");
    }

    public void Initialize(HandTorch handTorch)
    {
        _flame = handTorch.Flame;
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
