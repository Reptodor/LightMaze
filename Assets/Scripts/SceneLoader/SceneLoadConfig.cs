using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLoadSceneConfig", menuName = "Configs/LoadScene")]
public class SceneLoadConfig : ScriptableObject
{
    [SerializeField] private float _loadingTime;
    [SerializeField] private string _openingSceneName;

    public float LoadingTime => _loadingTime;
    public string OpeningSceneName => _openingSceneName;

    private void OnValidate()
    {
        if(_loadingTime < 0)
            throw new ArgumentOutOfRangeException(nameof(_loadingTime), "Loading time cannot be below zero");

        if(_openingSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_openingSceneName), "Opening scene name cannot be empty");
    }
}
