using DG.Tweening;
using UnityEngine;

public class ShakeAnimationHandler
{
    private ShakeAnimationConfig _shakeAnimationConfig;
    private Transform _transform;
    private Sequence _animation;

    public ShakeAnimationHandler(ShakeAnimationConfig shakeAnimationConfig, Transform transform)
    {
        _shakeAnimationConfig = shakeAnimationConfig;
        _transform = transform;
    }

    public void Start()
    {
        _animation = DOTween.Sequence();

        _animation.Append(_transform.DOPunchPosition(Vector2.right * _shakeAnimationConfig.ForceAmount,_shakeAnimationConfig.Duration))
                  .AppendInterval(_shakeAnimationConfig.Interval)
                  .SetLoops(-1, LoopType.Restart);
    }

    public void Stop()
    {
        _animation.Kill();
    }
}
