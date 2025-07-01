using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleportAbilityView : MonoBehaviour
{
    [SerializeField] private Button _abilityButton;
    [SerializeField] private Image _cooldownOverlay;
    [SerializeField] private AudioSource _teleportAudioSource;
    [SerializeField] private TextMeshProUGUI _keyText;

    private const float _fillAmountAtStartOfShowing = 1f;

    public event Action AbilityRequested;

    private void OnValidate()
    {
        if (_abilityButton == null)
            throw new ArgumentNullException(nameof(_abilityButton), "AbilityButton cannot be null");

        if (_cooldownOverlay == null)
            throw new ArgumentNullException(nameof(_cooldownOverlay), "CooldownOverlay cannot be null");

        if (_teleportAudioSource == null)
            throw new ArgumentNullException(nameof(_teleportAudioSource), "TeleportAudioSource cannot be null");

        if (_keyText == null)
            throw new ArgumentNullException(nameof(_keyText), "Key text cannot be null");
    }

    public void Initialize(KeyCode teleportAbilityKey)
    {
        _keyText.text = teleportAbilityKey.ToString();
        _abilityButton.onClick.AddListener(() => AbilityRequested?.Invoke());
    }

    public void OnTeleportAbilityKeyPressed()
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

    public void PlayTeleportSound()
    {
        _teleportAudioSource.Play();
    }
}
