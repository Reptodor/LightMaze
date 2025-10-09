using LightMaze._Scripts.SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
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

        SceneManager.LoadSceneAsync("UiBackground", LoadSceneMode.Additive);
        _textBlinking.OnEnable();
        _hasLoadingStarted = false;
    }

    private void OnDisable()
    {
        SceneManager.UnloadSceneAsync("UiBackground");
        _textBlinking.OnDisable();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_hasLoadingStarted)
        {
            _hasLoadingStarted = true;
            _sceneLoader.LoadMainMenu();
        }
    }
}
