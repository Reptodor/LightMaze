using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private TextBlinkingConfig _textBlinkingConfig;

    private TextBlinking _textBlinking;
    private Sequence _appearAnimation;
    private Sequence _disapperAnimation;
    private bool _isAppearing;

    public bool IsAppearing => _isAppearing;

    private void OnValidate()
    {
        if(_background == null)
            _background = GetComponentInChildren<Image>();

        if(_loadingText == null)
            _loadingText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Awake()
    {
        _textBlinking = new TextBlinking(_loadingText, _textBlinkingConfig);
    }

    private void OnEnable()
    {
        _textBlinking.OnEnable();
    }

    private void OnDisable()
    {
        _textBlinking.OnDisable();
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
}
