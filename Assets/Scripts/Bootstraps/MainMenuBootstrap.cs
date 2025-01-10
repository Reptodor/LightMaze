using System;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private MainMenuConfig _mainMenuConfig;

    private void OnValidate()
    {
        if(_mainMenu == null)
            throw new ArgumentNullException(nameof(_mainMenu), "Main menu cannot be null");

        if(_mainMenuConfig == null)
            throw new ArgumentNullException(nameof(_mainMenuConfig), "Main menu config cannot be null");
    }

    private void Awake()
    {
        _mainMenu.Initialize(_mainMenuConfig);
    }
}
