using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMainMenuConfig", menuName = "Configs/UI/MainMenu")]
public class MainMenuConfig : ScriptableObject
{
    [SerializeField] private float _buttonsSize;
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private float _interval;
    [SerializeField] private float _loadingTime;
    [SerializeField] private string _gameplaySceneName;
    [SerializeField] private string _settingsSceneName;

    public float ButtonsSize => _buttonsSize;
    public float AppearanceDuration => _appearanceDuration;
    public float Interval => _interval;
    public float LoadingTime => _loadingTime;
    public string GameplaySceneName => _gameplaySceneName;
    public string SettingsSceneName => _settingsSceneName;




    private void OnValidate()
    {
        if(_buttonsSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(_buttonsSize), "Buttons size must be greater than zero");
            
        if(_appearanceDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_appearanceDuration), "Appearance duration must be greater than zero");

        if(_loadingTime < 0)
            throw new ArgumentOutOfRangeException(nameof(_loadingTime), "Loading time cannot be below zero");

        if(_gameplaySceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_gameplaySceneName), "Gameplay scene name cannot be empty");

        if(_settingsSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_settingsSceneName), "Settings scene name cannot be empty");
    }
}
