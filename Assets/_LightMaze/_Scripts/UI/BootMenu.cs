using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextBlinkingConfig _textBlinkingConfig;

    private TextBlinking _textBlinking;
    private SceneLoader _sceneLoader;
    private bool _hasLoadingStarted;
    private bool _isInitialized;

    private void Start()
    {
        _textBlinking = new TextBlinking(_text, _textBlinkingConfig);
        _sceneLoader = FindAnyObjectByType<SceneLoader>();
        _isInitialized = true;

        OnEnable();
    }

    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        _sceneLoader.LoadSceneWithOutLoadingScreen(_sceneLoader.SceneNamesConfig.BootAndMainMenuBackgroundSceneName);

        _textBlinking.OnEnable();
        _hasLoadingStarted = false;
    }

    private void OnDisable()
    {
        _textBlinking.OnDisable();

        SceneManager.UnloadSceneAsync(_sceneLoader.SceneNamesConfig.BootAndMainMenuBackgroundSceneName);
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_hasLoadingStarted)
        {
            _hasLoadingStarted = true;
            _sceneLoader.LoadSceneWithLoadingScreen(_sceneLoader.SceneNamesConfig.MainMenuSceneName,
                                                    _sceneLoader.ScenesLoadingTimeConfig.MainMenuSceneLoadingTime);
        }
    }
}
