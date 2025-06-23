using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    private SettingsMenu _settingsMenu;
    private SceneLoader _sceneLoader;
    private bool _isActive;

    private void OnValidate()
    {
        if (_pauseMenuPanel == null)
            throw new ArgumentNullException(nameof(_pauseMenuPanel), "PauseMenuPanel cannot be null");
    }

    public void Initialize(SceneLoader sceneLoader, SettingsMenu settingsMenu)
    {
        _sceneLoader = sceneLoader;
        _settingsMenu = settingsMenu;
    }

    public void OnPauseMenuKeyPressed()
    {
        if (_isActive)
            _pauseMenuPanel.SetActive(true);
        else
            _pauseMenuPanel.SetActive(false);
    }

    public void Resume()
    {
        _pauseMenuPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        _settingsMenu.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        _sceneLoader.LoadSceneWithLoadingScreen(_sceneLoader.SceneNamesConfig.MainMenuSceneName,
                                                _sceneLoader.ScenesLoadingTimeConfig.MainMenuSceneLoadingTime);
    }
}
