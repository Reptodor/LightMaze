using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class QuestAnimationHandler
{
    private QuestAnimationHandlerConfig _questAnimationHandlerConfig;
    private TextMeshProUGUI _text;
    private Vector3 _startPosition;
    private Sequence _complitingSequence;
    private Sequence _appearingSequence;
    private bool _isAnimating;

    public bool IsAnimating => _isAnimating;
    public event Action AppearingAnimationCompleted;

    public QuestAnimationHandler(QuestAnimationHandlerConfig questAnimationHandlerConfig, TextMeshProUGUI text, Vector3 startPosition)
    {
        _questAnimationHandlerConfig = questAnimationHandlerConfig;
        _text = text;
        _startPosition = startPosition;

    }

    public void OnDisable()
    {
        _complitingSequence.Kill();
        _appearingSequence.Kill();
    }

    public void AnimateCompleting()
    {
        if(_isAnimating)
            return;
            
        _complitingSequence = DOTween.Sequence();

        _isAnimating = true;

        _complitingSequence.Append(_text.DOColor(Color.green, _questAnimationHandlerConfig.AnimationDuration))
                           .Join(_text.transform.DOMove(_text.transform.position + _questAnimationHandlerConfig.TextMovementOffset, _questAnimationHandlerConfig.AnimationDuration))
                           .Join(_text.DOFade(0, _questAnimationHandlerConfig.AnimationDuration))
                           .AppendCallback(() => _isAnimating = false)
                           .JoinCallback(() => AppearingAnimationCompleted?.Invoke());
    }

    public void AnimateAppearing()
    {
        _appearingSequence = DOTween.Sequence();

        _appearingSequence.AppendCallback(() => ResetText())
                          .Append(_text.DOFade(1, _questAnimationHandlerConfig.AnimationDuration));
    }
    

    private void ResetText()
    {
        _text.color = _questAnimationHandlerConfig.BaseColor;
        _text.transform.position = _startPosition;
    }
}
