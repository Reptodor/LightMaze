using LightMaze._Scripts.SceneLoader;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{   
    [SerializeField] private LoadingScreen _loadingScreenPrefab;
    [SerializeField] private SceneLoader _sceneLoader;

    private void Awake()
    {
        LoadingScreen loadingScreen = Instantiate(_loadingScreenPrefab);
        loadingScreen.gameObject.SetActive(false);

        _sceneLoader.Initialize(loadingScreen);
        _sceneLoader.LoadSplashScreen();
    }
}
