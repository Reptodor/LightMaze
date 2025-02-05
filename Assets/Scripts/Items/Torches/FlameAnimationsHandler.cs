using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlameAnimationsHandler
{
    private FlameAnimationsConfig _config;
    private Light2D _flame;
    private float _currentTime;
    private float _totalTime;

    public event Action FlameActivated;

    public FlameAnimationsHandler(FlameAnimationsConfig flameAnimationsConfig, Light2D flame)
    {
        _config = flameAnimationsConfig;
        _flame = flame;

        _totalTime = _config.FlameIntensityCurve.keys[_config.FlameIntensityCurve.length - 1].time;
    }

    public void HandleActivationAnimation()
    {
        Sequence animation = DOTween.Sequence();
        
        animation.AppendCallback(() => _flame.gameObject.SetActive(true))
                 .Append(_flame.transform.DOScale(new Vector2(1, 1), _config.AppearanceDuration).From(Vector2.zero).SetEase(Ease.Flash))
                 .Join(DOTween.To(SetFlameIntensity, 0, _config.FlameIntensity, _config.AppearanceDuration))
                 .AppendCallback(() => FlameActivated?.Invoke());
    }

    private void SetFlameIntensity(float intensity)
    {
        _flame.intensity = intensity;
    }

    public void HandleFlameIntensityAnimation()
    {
        _flame.intensity = _config.FlameIntensityCurve.Evaluate(_currentTime);
        _currentTime += Time.deltaTime;

        if(_currentTime > _totalTime)
            _currentTime = 0;
    }
}
