using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneNamesConfig _sceneNamesConfig;
    [SerializeField] private ScenesLoadingTimeConfig _scenesLoadingTimeConfig;

    private LoadingScreen _loadingMenu;
    private string _currentSceneName = "BootMenuScene";

    public SceneNamesConfig SceneNamesConfig => _sceneNamesConfig;
    public ScenesLoadingTimeConfig ScenesLoadingTimeConfig => _scenesLoadingTimeConfig;

    public void Initialize(LoadingScreen loadingMenu)
    {
        _loadingMenu = loadingMenu;
    }

    public void LoadSceneWithOutLoadingScreen(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneWithLoadingScreen(string sceneName, float loadingTime)
    {
        StartCoroutine(LoadScene(sceneName, loadingTime));
    }

    public void RestartSceneWithLoadingScreen(float restartingTime)
    {
        StartCoroutine(LoadScene(_currentSceneName, restartingTime));
    }

    private IEnumerator LoadScene(string sceneName, float loadingTime)
    {
        _loadingMenu.gameObject.SetActive(true);
        _loadingMenu.Appear();

        yield return new WaitWhile(() => _loadingMenu.IsAppearing == true);

        if (_currentSceneName != null)
            SceneManager.UnloadSceneAsync(_currentSceneName);

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _currentSceneName = sceneName;

        yield return new WaitForSeconds(loadingTime);
        _loadingMenu.Disapear();
    }    
}
