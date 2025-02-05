using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Key : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    private ShakeAnimationHandler _shakeAnimationHandler;
    private Tween _enableLightTween;
    private bool _isInitialized;

    private void OnValidate()
    {
        if(_light2D == null)
            _light2D = GetComponent<Light2D>();
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
        _enableLightTween = DOTween.To(SetLightIntesity, 0, 1f, 1f);
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

            Destroy(gameObject);
        }
    }
}
