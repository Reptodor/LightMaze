using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private MainMenuConfig _mainMenuConfig;

    private SettingsMenu _settingsMenu;
    private SceneLoader _sceneLoader;
    private bool _hasLoadingStarted;
    private bool _isInitialized;

    private void OnValidate()
    {
        if (_buttons.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(_buttons.Length), "ButtonsLength cannot be zero");

        if (_mainMenuConfig == null)
            throw new ArgumentNullException(nameof(_mainMenuConfig), "MainMenuConfig cannot be null");
    }

    public void Initialize(SceneLoader sceneLoader, SettingsMenu settingsMenu)
    {
        _sceneLoader = sceneLoader;
        _settingsMenu = settingsMenu;
        _isInitialized = true;

        OnEnable();
    }

    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        _sceneLoader.LoadSceneWithOutLoadingScreen(_sceneLoader.SceneNamesConfig.BootAndMainMenuBackgroundSceneName);

        _hasLoadingStarted = false;
        Show();
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync(_sceneLoader.SceneNamesConfig.BootAndMainMenuBackgroundSceneName);
    }

    public void Play()
    {
        if (_hasLoadingStarted)
            return;

        _hasLoadingStarted = true;
        StartCoroutine(Hide());
    }

    public void OpenSettings()
    {
        _settingsMenu.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
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

    private IEnumerator Hide()
    {
        Sequence animation = DOTween.Sequence();

        yield return animation.Append(_buttons[0].transform.DOScale(0, _mainMenuConfig.AppearanceDuration).From(_mainMenuConfig.ButtonsSize).SetEase(Ease.InBack)).
                AppendInterval(_mainMenuConfig.Interval).
                Append(_buttons[1].transform.DOScale(0, _mainMenuConfig.AppearanceDuration).From(_mainMenuConfig.ButtonsSize).SetEase(Ease.InBack)).
                AppendInterval(_mainMenuConfig.Interval).
                Append(_buttons[2].transform.DOScale(0, _mainMenuConfig.AppearanceDuration).From(_mainMenuConfig.ButtonsSize).SetEase(Ease.InBack)).
                AppendCallback(() => _sceneLoader.LoadSceneWithLoadingScreen(
                    _sceneLoader.SceneNamesConfig.GameplayScenesNames[0], _sceneLoader.ScenesLoadingTimeConfig.GameplayScenesLoadingTime));
    }
}
