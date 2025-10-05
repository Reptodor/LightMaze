using UnityEngine;

[CreateAssetMenu(fileName = "NewScenesLoadingTimeConfig", menuName = "Configs/LoadingScenes/ScenesLoadingTime")]
public class ScenesLoadingTimeConfig : ScriptableObject
{
    [SerializeField] private float _gameplayScenesLoadingTime;
    [SerializeField] private float _bootMenuSceneLoadingTime;
    [SerializeField] private float _mainMenuSceneLoadingTime;
    [SerializeField] private float _settingsMenuSceneLoadingTime;
    [SerializeField] private float _pauseMenuSceneLoadingTime;
    [SerializeField] private float _loadingScreenSceneLoadingTime;

    public float GameplayScenesLoadingTime => _gameplayScenesLoadingTime;
    public float BootMenuSceneLoadingTime => _bootMenuSceneLoadingTime;
    public float MainMenuSceneLoadingTime => _mainMenuSceneLoadingTime;
    public float SettingsMenuSceneLoadingTime => _settingsMenuSceneLoadingTime;
    public float PauseMenuSceneLoadingTime => _pauseMenuSceneLoadingTime;
    public float LoadingScreenSceneLoadingTime => _loadingScreenSceneLoadingTime;
}
