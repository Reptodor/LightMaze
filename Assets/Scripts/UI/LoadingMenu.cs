using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingMenu : MonoBehaviour
{
    [SerializeField] private Image _loadingImage;
    private Sequence _animation;

    private void OnValidate()
    {
        if(_loadingImage == null)
            throw new ArgumentNullException(nameof(_loadingImage), "Loading image cannot be null");
    }

    private void OnEnable()
    {
        _animation = DOTween.Sequence();

        _animation.Append(_loadingImage.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 181), 1f).From(Quaternion.Euler(0, 0, 1)))
                 .Append(_loadingImage.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 360), 1f))
                 .SetLoops(-1, LoopType.Restart);
    }

    private void OnDisable()
    {
        _animation.Kill();
    }
}
