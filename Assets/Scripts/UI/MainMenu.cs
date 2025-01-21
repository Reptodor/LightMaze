using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    private MainMenuConfig _mainMenuConfig;
    private SceneLoader _sceneLoader;
    private bool _isActive;

    private void OnValidate()
    {
        if(_buttons == null)
            throw new ArgumentNullException(nameof(_buttons), "Buttons cannot be null");
    }

    public void Initialize(MainMenuConfig mainMenuConfig)
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();
        _mainMenuConfig = mainMenuConfig;
    }

    private void OnEnable()
    {
        _isActive = true;
        Show();
    }

    private void Show()
    {
        Sequence animation = DOTween.Sequence();

        animation.AppendInterval(2.1f).
                  Append(_buttons[0].transform.DOScale(_mainMenuConfig.ButtonsSize, _mainMenuConfig.AppearanceDuration).From(0).SetEase(Ease.OutBounce)).
                  AppendInterval(_mainMenuConfig.Interval).
                  Append(_buttons[1].transform.DOScale(_mainMenuConfig.ButtonsSize, _mainMenuConfig.AppearanceDuration).From(0).SetEase(Ease.OutBounce)).
                  AppendInterval(_mainMenuConfig.Interval).
                  Append(_buttons[2].transform.DOScale(_mainMenuConfig.ButtonsSize, _mainMenuConfig.AppearanceDuration).From(0).SetEase(Ease.OutBounce));

    }

    private IEnumerator Hide(string openingSceneName)
    {
        Sequence animation = DOTween.Sequence();

        yield return animation.Append(_buttons[0].transform.DOScale(0, _mainMenuConfig.AppearanceDuration).From(_mainMenuConfig.ButtonsSize).SetEase(Ease.InBack)).
                AppendInterval(_mainMenuConfig.Interval).
                Append(_buttons[1].transform.DOScale(0, _mainMenuConfig.AppearanceDuration).From(_mainMenuConfig.ButtonsSize).SetEase(Ease.InBack)).
                AppendInterval(_mainMenuConfig.Interval).
                Append(_buttons[2].transform.DOScale(0, _mainMenuConfig.AppearanceDuration).From(_mainMenuConfig.ButtonsSize).SetEase(Ease.InBack)).
                AppendCallback(() => Coroutines.StartRoutine(_sceneLoader.LoadScene(openingSceneName, _mainMenuConfig.LoadingTime)));
    }

    public void Play()
    {
        if(!_isActive)
            return;

        _isActive = false;
        StartCoroutine(Hide(_mainMenuConfig.GameplaySceneName));
    }

    public void OpenSettings()
    {
        if(!_isActive)
            return;

        _isActive = false;
        StartCoroutine(Hide(_mainMenuConfig.SettingsSceneName));
    }

    public void Quit()
    {
        if(!_isActive)
            return;

        _isActive = false;
        Application.Quit();
    }
}
