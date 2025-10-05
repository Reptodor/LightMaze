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
        
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Resume()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
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
