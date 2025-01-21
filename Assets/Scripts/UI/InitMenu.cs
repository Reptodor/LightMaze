using TMPro;
using UnityEngine;
using DG.Tweening;

public class InitMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private SceneLoadConfig _sceneLoadConfig;
    private SceneLoader _sceneLoader;
    private Tween _textAnimation;
    private bool _isActive;

    public void Initialize(SceneLoadConfig initSceneConfig)
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();
        _sceneLoadConfig = initSceneConfig;
    }

    private void OnEnable()
    {
        _isActive = true;
        AnimateText();
    }

    private void OnDisable()
    {
        StopAnimatingText();
    }

    private void Update()
    {
        if(Input.anyKeyDown && _isActive)
        {
            _isActive = false;
            Coroutines.StartRoutine(_sceneLoader.LoadScene(_sceneLoadConfig.OpeningSceneName, _sceneLoadConfig.LoadingTime));
        }
    }

    private void AnimateText()
    {
        _textAnimation = _text.DOFade(0.08f, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void StopAnimatingText()
    {
        _textAnimation.Kill();
    }
}
