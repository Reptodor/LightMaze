using UnityEngine;

public class Bootstrap : MonoBehaviour
{   
    [SerializeField] private SceneNamesConfig _sceneNamesConfig;
    [SerializeField] private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader.LoadSceneWithOutLoadingScreen(_sceneNamesConfig.LoadingScreenSceneName);
        _sceneLoader.LoadSceneWithOutLoadingScreen(_sceneNamesConfig.BootMenuSceneName);
    }

    private void Start()
    {
        LoadingScreen loadingMenu = FindObjectOfType<LoadingScreen>();
        loadingMenu.gameObject.SetActive(false);
        _sceneLoader.Initialize(loadingMenu);
    } 
}
