using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextBlinking
{
    private TextMeshProUGUI _text;
    private TextBlinkingConfig _config;
    private Tween _textBlinkingAnimation;
    private Color _textColor;

    public TextBlinking(TextMeshProUGUI text, TextBlinkingConfig textBlinkingConfig)
    {
        _text = text;
        _config = textBlinkingConfig;
        _textColor = _text.color;
    }

    public void OnEnable()
    {
        StartTextBlinking();
    }

    public void OnDisable()
    {
        _textBlinkingAnimation.Kill();
        _text.color = _textColor;
    }
    
    private void StartTextBlinking()
    {
        Color endColor = new Color(_textColor.r, _textColor.g, _textColor.b, _config.MinAlpha);

        _textBlinkingAnimation = _text.DOColor(endColor, _config.BlinkDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
