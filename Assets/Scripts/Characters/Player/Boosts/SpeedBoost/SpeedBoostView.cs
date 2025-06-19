using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoostView : MonoBehaviour
{
    [SerializeField] private Button _boostButton;
    [SerializeField] private Image _cooldownOverlay;
    [SerializeField] private AudioSource _runAudioSource;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private TextMeshProUGUI _keyText;

    private float _defaultPitch;

    private const string _animatorParameterName = "SpeedBoost";
    private const float _fillAmountAtStartOfShowing = 1f;

    public event Action BoostRequested;

    private void OnValidate()
    {
        if (_boostButton == null)
            throw new ArgumentNullException(nameof(_boostButton), "BoostButton cannot be null");

        if (_cooldownOverlay == null)
            throw new ArgumentNullException(nameof(_cooldownOverlay), "CooldownOverlay cannot be null");

        if (_runAudioSource == null)
            throw new ArgumentNullException(nameof(_runAudioSource), "RunAudioSource cannot be null");

        if (_playerAnimator == null)
            throw new ArgumentNullException(nameof(_playerAnimator), "PlayerAnimator cannot be null");

        if (_keyText == null)
            throw new ArgumentNullException(nameof(_keyText), "Key text cannot be null");
    }

    public void Initialize(KeyCode speedBoostKey)
    {
        _keyText.text = speedBoostKey.ToString();
        _boostButton.onClick.AddListener(() => BoostRequested?.Invoke());
        _defaultPitch = _runAudioSource.pitch;
    }

    public void OnSpeedBoostKeyPressed()
    {
        BoostRequested?.Invoke();
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
