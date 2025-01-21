using DG.Tweening;
using UnityEngine;

public class TemporaryGroundTorch : BaseGroundTorch
{
    private TemporaryGroundTorchConfig _temporaryGroundTorchConfig;
    private float _unactivateTime;
    private bool _isUnactivating;

    public override void Initialize(BaseGroundTorchConfig uselessConfig, TemporaryGroundTorchConfig config)
    {
        _temporaryGroundTorchConfig = config;
        Flame.gameObject.SetActive(false);

        IsInitialized = true;
    }

    private void Update()
    {
        if(!IsInitialized)
            return;

        if(IsPlayerInside)
            return;
        
        if(Time.time > _unactivateTime)
        {
            Unactivate();
        }

        if(_isUnactivating)
        {
            Flame.intensity = Flame.transform.localScale.x;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!IsInitialized)
            return;

        if(other.GetComponent<Player>() != null)
        {
            _unactivateTime = Time.time + _temporaryGroundTorchConfig.LifeTime;
            IsPlayerInside = false;
        }
    }

    private void Unactivate()
    {
        if(!IsPlayerInside)
        {
            Sequence animation = DOTween.Sequence();
        
            animation.AppendCallback(() => Flame.gameObject.SetActive(true))
                 .Append(Flame.transform.DOScale(Vector2.zero, _temporaryGroundTorchConfig.AppearanceDuration).From(new Vector2(1, 1)).SetEase(Ease.Flash))
                 .AppendCallback(() => _isUnactivating = false).JoinCallback(() => Flame.gameObject.SetActive(false));
        }
    }
}
