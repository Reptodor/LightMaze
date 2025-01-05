using TMPro;
using UnityEngine;
using DG.Tweening;

public class BootstrapMenu : MonoBehaviour
{
    [SerializeField] private StateSwitch _stateSwitch;
    [SerializeField] private TextMeshProUGUI _text;
    private Tween _textAnimation;

    private void OnEnable()
    {
        AnimateText();
    }

    private void OnDisable()
    {
        StopAnimatingText();
    }

    private void Update()
    {
        if(Input.anyKey)
            _stateSwitch.EnterIn<MainMenuState>();
    }

    private void AnimateText()
    {
        _textAnimation = _text.DOFade(0.08f, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void StopAnimatingText()
    {
        _textAnimation.Kill();
    }
}
