using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMaze._Scripts.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private float _mainMenuLoadingTime = 2f;
        [SerializeField] private float _gameplayLoadingTime = 3f;

        private GameScenes _gameScenes;

        private LoadingScreen _loadingMenu;
        private string _currentSceneName;

        public void Initialize(LoadingScreen loadingMenu)
        {
            _loadingMenu = loadingMenu;

            _gameScenes = new GameScenes();
        }

        public void LoadSplashScreen()
        {
            _currentSceneName = _gameScenes.SplashScreen;

            SceneManager.LoadSceneAsync(_currentSceneName, LoadSceneMode.Additive);
        }

        public void LoadMainMenu()
        {
            StartCoroutine(LoadScene(_gameScenes.MainMenu, _mainMenuLoadingTime));
        }

        public void LoadLevel(int levelNumber)
        {
            StartCoroutine(LoadScene(_gameScenes.Levels[levelNumber - 1], _gameplayLoadingTime));
        }

        public void RestartGameplayScene()
        {
            StartCoroutine(LoadScene(_currentSceneName, _gameplayLoadingTime));
        }

        private IEnumerator LoadScene(string sceneName, float loadingTime)
        {
            _loadingMenu.gameObject.SetActive(true);
            _loadingMenu.Appear();

            yield return new WaitWhile(() => _loadingMenu.IsAppearing == true);

            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            if (_currentSceneName != null)
                SceneManager.UnloadSceneAsync(_currentSceneName);

            _currentSceneName = sceneName;

            yield return new WaitForSeconds(loadingTime);
            _loadingMenu.Disapear();
        }
    }
}