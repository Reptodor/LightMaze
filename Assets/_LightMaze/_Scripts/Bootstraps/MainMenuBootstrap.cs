using System.Collections;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingsMenu _settingsMenuPrefab;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        StartCoroutine(nameof(Initialize));
    }


    public virtual IEnumerator Initialize()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        yield return null;

        SettingsMenu settingsMenu = Instantiate(_settingsMenuPrefab);

        yield return null;

        _mainMenu.Initialize(_sceneLoader, settingsMenu);
    }
}
