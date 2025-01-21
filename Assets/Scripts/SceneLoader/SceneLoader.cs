using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private LoadingMenu _loadingMenu;
    private string _currentSceneName;

    public void Initialize(LoadingMenu loadingMenu)
    {
        _loadingMenu = loadingMenu;
    }

    public IEnumerator LoadScene(string sceneName, float loadingTime)
    {
        _loadingMenu.gameObject.SetActive(true);
        _loadingMenu.Appear();

        yield return new WaitWhile(() => _loadingMenu.IsAppearing == true);

        if(_currentSceneName != null)
            SceneManager.UnloadSceneAsync(_currentSceneName);

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _currentSceneName = sceneName;

        yield return new WaitForSeconds(loadingTime);

        _loadingMenu.Disapear();
    }
}
