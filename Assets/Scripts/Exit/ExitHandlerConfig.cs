using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewExitConfig", menuName = "Configs/Exit")]
public class ExitHandlerConfig : ScriptableObject
{
    [SerializeField] private string _nextSceneName;
    [SerializeField] private float _loadingTime;

    public string NextSceneName => _nextSceneName;
    public float LoadingTime => _loadingTime;

    private void OnValidate()
    {
        if(_nextSceneName == "")
            throw new ArgumentOutOfRangeException(nameof(_nextSceneName), "Next scene name cannot be empty");

        if(_loadingTime < 0)
            throw new ArgumentOutOfRangeException(nameof(_loadingTime), "Loading time cannot be below zero");
    }
}
