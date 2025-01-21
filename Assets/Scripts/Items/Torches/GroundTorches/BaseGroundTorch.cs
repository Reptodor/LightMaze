using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class BaseGroundTorch : MonoBehaviour
{
    private BaseGroundTorchConfig _baseGroundTorchConfig;
    private Sequence _animation;
    private bool _activated;
    protected bool IsInitialized = false;
    
    [SerializeField] protected Light2D Flame;
    protected bool IsPlayerInside;

    private void OnValidate()
    {
        if(Flame == null)
            Flame = GetComponentInChildren<Light2D>();
    }

    public virtual void Initialize(BaseGroundTorchConfig config, TemporaryGroundTorchConfig temporaryGroundTorchConfig)
    {
        _baseGroundTorchConfig = config;
        Flame.gameObject.SetActive(false);
        StartShakeAnimation();

        IsInitialized = true;
    }

    private void OnDisable()
    {
        _animation.Kill();
    }

    private void StartShakeAnimation()
    {
        _animation = DOTween.Sequence();

        _animation.Append(transform.DOPunchPosition(Vector2.right * 0.10f, 0.4f)).AppendInterval(3f).SetLoops(-1, LoopType.Restart);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!IsInitialized || _activated)
            return;
            
        if(other.GetComponent<Player>() != null)
        {
            Activate();
            _animation.Kill();
            IsPlayerInside = true;
        }
    }

    private void Activate()
    {
        Sequence animation = DOTween.Sequence();
        
        animation.AppendCallback(() => Flame.gameObject.SetActive(true))
                 .Append(Flame.transform.DOScale(new Vector2(1, 1), _baseGroundTorchConfig.AppearanceDuration).From(Vector2.zero).SetEase(Ease.Flash))
                 .Join(DOTween.To(SetFlameIntensity, 0, _baseGroundTorchConfig.FlameIntensity, _baseGroundTorchConfig.AppearanceDuration))
                 .AppendCallback(() => _activated = true);
        
    }
    
    private void SetFlameIntensity(float intensity)
    {
        Flame.intensity = intensity;
    }

}
