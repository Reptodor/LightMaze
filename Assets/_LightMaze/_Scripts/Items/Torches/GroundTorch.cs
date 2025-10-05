using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GroundTorch : MonoBehaviour
{
    [SerializeField] private Light2D _flame;
    private ShakeAnimationHandler _shakeAnimationHandler;
    private FlameAnimationsHandler _flameAnimationsHandler;
    private bool _isActivated = false;
    private bool _isInitialized = false;
    

    private void OnValidate()
    {
        if(_flame == null)
            _flame = GetComponentInChildren<Light2D>();
    }


    public virtual void Initialize(FlameAnimationsConfig flameAnimationsConfig, ShakeAnimationConfig shakeAnimationConfig)
    {
        _flame.gameObject.SetActive(false);
        _shakeAnimationHandler = new ShakeAnimationHandler(shakeAnimationConfig, transform);
        _shakeAnimationHandler.Start();
        _flameAnimationsHandler = new FlameAnimationsHandler(flameAnimationsConfig, _flame);

        _isInitialized = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if(!_isInitialized)
            return;

        _flameAnimationsHandler.FlameActivated += OnFlameActivated;
    }

    private void OnDisable()
    {
        _flameAnimationsHandler.FlameActivated -= OnFlameActivated;
        _shakeAnimationHandler.Stop();
    }

    private void Update()
    {
        if(!_isInitialized || !_isActivated)
            return;

        _flameAnimationsHandler.HandleFlameIntensityAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_isInitialized || _isActivated)
            return;
            
        if(other.GetComponent<Player>() != null)
        {
            _flameAnimationsHandler.HandleActivationAnimation();
            _shakeAnimationHandler.Stop();
        }
    }

    private void OnFlameActivated()
    {
        _isActivated = true;
    }
}
