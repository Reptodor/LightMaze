using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedAbilityView : MonoBehaviour
{
    [SerializeField] private Button _abilityButton;
    [SerializeField] private Image _cooldownOverlay;
    [SerializeField] private AudioSource _runAudioSource;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private TextMeshProUGUI _keyText;

    private float _defaultPitch;

    private const string _animatorParameterName = "SpeedAbility";
    private const float _fillAmountAtStartOfShowing = 1f;

    public event Action AbilityRequested;

    private void OnValidate()
    {
        if (_abilityButton == null)
            throw new ArgumentNullException(nameof(_abilityButton), "AbilityButton cannot be null");

        if (_cooldownOverlay == null)
            throw new ArgumentNullException(nameof(_cooldownOverlay), "CooldownOverlay cannot be null");

        if (_runAudioSource == null)
            throw new ArgumentNullException(nameof(_runAudioSource), "RunAudioSource cannot be null");

        if (_playerAnimator == null)
            throw new ArgumentNullException(nameof(_playerAnimator), "PlayerAnimator cannot be null");

        if (_keyText == null)
            throw new ArgumentNullException(nameof(_keyText), "Key text cannot be null");
    }

    public void Initialize(KeyCode speedAbilityKey)
    {
        _keyText.text = speedAbilityKey.ToString();
        _abilityButton.onClick.AddListener(() => AbilityRequested?.Invoke());
        _defaultPitch = _runAudioSource.pitch;
    }

    public void OnSpeedAbilityKeyPressed()
    {
        AbilityRequested?.Invoke();
    }

    public void ShowCooldownVisual()
    {
        _cooldownOverlay.fillAmount = _fillAmountAtStartOfShowing;
    }

    public void UpdateCooldownVisual(float cooldownProgress)
    {
        _cooldownOverlay.fillAmount = cooldownProgress;
    }

    public void PlayRunAnimationWithBoostPercent(float boostPercent)
    {
        _playerAnimator.SetFloat(_animatorParameterName, boostPercent);
    }

    public void PlayRunSoundWithBoostPercent(float boostPercent)
    {
        _runAudioSource.pitch = _defaultPitch * boostPercent;
    }
}
