using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingMenu : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _loadingText;
    private Sequence _appearAnimation;
    private Sequence _disapperAnimation;
    private Tween _textAnimation;
    private bool _isAppearing;

    public bool IsAppearing => _isAppearing;

    private void OnValidate()
    {
        if(_background == null)
            _background = GetComponent<Image>();

        if(_loadingText == null)
            _loadingText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SetTextAnimation();
    }

    private void OnDisable()
    {
        _textAnimation.Kill();
        _disapperAnimation.Kill();
    }

    public void Appear()
    {
        _isAppearing = true;

        _appearAnimation = DOTween.Sequence();

        _appearAnimation.Append(_background.DOFade(1, 1).From(0).SetEase(Ease.Linear))
                        .AppendCallback(() => _isAppearing = false);
    }

    public void Disapear()
    {
        _disapperAnimation = DOTween.Sequence();

        _disapperAnimation.Append(_background.DOFade(0, 1).From(1).SetEase(Ease.Linear))
                          .Join(_loadingText.DOFade(0, 1)).SetEase(Ease.Linear)
                          .AppendCallback(() => gameObject.SetActive(false));
    }

    private void SetTextAnimation()
    {
        _textAnimation = _loadingText.DOFade(0.08f, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
