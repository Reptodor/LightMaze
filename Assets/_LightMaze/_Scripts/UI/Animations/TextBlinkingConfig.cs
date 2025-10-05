using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTextBlinkingConfig", menuName = "Configs/UI/Animations/TextBlinking")]
public class TextBlinkingConfig : ScriptableObject
{
    [SerializeField] private float _blinkDuration = 0.5f;
    [SerializeField] private float _minAlpha = 0.2f;

    public float BlinkDuration => _blinkDuration;
    public float MinAlpha => _minAlpha;

    private void OnValidate()
    {
        if (_blinkDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_blinkDuration), "Blink duration must be greater than zero");

        if (_minAlpha < 0)
            throw new ArgumentOutOfRangeException(nameof(_minAlpha), "Min alfa cannot be below zero");
    }
}
