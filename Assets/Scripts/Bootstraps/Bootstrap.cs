using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{   
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private LoadingMenu _loadingMenu;
    [SerializeField] private SceneLoadConfig _sceneLoadConfig;

    private void OnValidate()
    {
        if(_sceneLoader == null)
            throw new ArgumentNullException(nameof(_sceneLoader), "Scene loader cannot be null");

        if(_loadingMenu == null)
            throw new ArgumentNullException(nameof(_loadingMenu), "Loading menu cannot be null");

        if(_sceneLoadConfig == null)
            throw new ArgumentNullException(nameof(_sceneLoadConfig), "Scene load config cannot be null");
    }

    private void Awake()
    {
        _sceneLoader.Initialize(_loadingMenu);
        StartCoroutine(_sceneLoader.LoadScene(_sceneLoadConfig.OpeningSceneName, _sceneLoadConfig.LoadingTime));
    }
}
