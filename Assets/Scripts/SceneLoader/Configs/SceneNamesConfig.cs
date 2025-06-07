using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneNamesConfig", menuName = "Configs/LoadingScenes/SceneNames")]
public class SceneNamesConfig : ScriptableObject
{
    [SerializeField] private string[] _gameplayScenesNames;
    [SerializeField] private string _bootMenuSceneName;
    [SerializeField] private string _mainMenuSceneName;
    [SerializeField] private string _settingsMenuSceneName;
    [SerializeField] private string _pauseMenuSceneName;
    [SerializeField] private string _loadingScreenSceneName;
    [SerializeField] private string _bootAndMainMenuBackgroundSceneName;

    public string[] GameplayScenesNames => _gameplayScenesNames;
    public string BootMenuSceneName => _bootMenuSceneName;
    public string MainMenuSceneName => _mainMenuSceneName;
    public string SettingsMenuSceneName => _settingsMenuSceneName;
    public string PauseMenuSceneName => _pauseMenuSceneName;
    public string LoadingScreenSceneName => _loadingScreenSceneName;
    public string BootAndMainMenuBackgroundSceneName => _bootAndMainMenuBackgroundSceneName;

    private void OnValidate()
    {
        if (_gameplayScenesNames.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(_gameplayScenesNames.Length), "Gameplay scenes names length must be greater than zero");

        if (_bootMenuSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_bootMenuSceneName), "Boot menu scene name cannot be empty");

        if (_mainMenuSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_mainMenuSceneName), "Main menu scene name cannot be empty");

        if (_settingsMenuSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_settingsMenuSceneName), "Settings menu scene name cannot be empty");

        if (_pauseMenuSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_pauseMenuSceneName), "Pause menu scene name cannot be empty");

        if (_loadingScreenSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_loadingScreenSceneName), "Loading screen scene name cannot be empty");

        if (_bootAndMainMenuBackgroundSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_bootAndMainMenuBackgroundSceneName), "Boot and main menu background scene name cannot be emptry"); 
    }
}
