using System;
using DG.Tweening;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialConfig _tutorialConfig;

    private Sequence _sequence;
    private Tween _tween;

    private void OnValidate()
    {
        if (_tutorialConfig == null)
            throw new ArgumentNullException(nameof(_tutorialConfig), "Tutorial config cannot be null");
    }

    private void OnDestroy()
    {
        _sequence.Kill();
        _tween.Kill();
    }

    public void Open()
    {
        _tween = transform.DOScale(_tutorialConfig.TutorialMaxSize, _tutorialConfig.TutorialAnimationDuration)
                 .From(_tutorialConfig.TutorialMinSize).SetEase(Ease.OutBounce);
    }

    public void Close()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(_tutorialConfig.TutorialMinSize, _tutorialConfig.TutorialAnimationDuration)
                 .From(_tutorialConfig.TutorialMaxSize).SetEase(Ease.InBack)).AppendCallback(() => gameObject.SetActive(false));
    }
}
