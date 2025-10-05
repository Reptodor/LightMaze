using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Key : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    private ShakeAnimationHandler _shakeAnimationHandler;
    private Tween _enableLightTween;
    private bool _isInitialized;

    private const float _enableAnimationStartValue = 0f;
    private const float _enableAnimationEndValue = 1f;
    private const float _enableAnimationDuration = 1f;
    
    private void OnValidate()
    {
        if (_light2D == null)
            throw new ArgumentNullException(nameof(_light2D), "Light2D cannot be null");
    }

    public void Initialize(ShakeAnimationConfig shakeAnimationConfig)
    {
        _shakeAnimationHandler = new ShakeAnimationHandler(shakeAnimationConfig, transform);

        _isInitialized = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if(_isInitialized)
            _shakeAnimationHandler.Start();
    }

    private void OnDisable()
    {
        _shakeAnimationHandler.Stop();
        _enableLightTween.Kill();
    }

    public void EnableLight()
    {
        _light2D.enabled = true;
        _enableLightTween = DOTween.To(SetLightIntesity, _enableAnimationStartValue, _enableAnimationEndValue, _enableAnimationDuration);
    }

    private void SetLightIntesity(float intensity)
    {
        _light2D.intensity = intensity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            player.BagHandler.AddKey(1);

            gameObject.SetActive(false);
        }
    }
}
