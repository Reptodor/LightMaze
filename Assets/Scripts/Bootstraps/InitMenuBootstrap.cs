using System;
using UnityEngine;

public class InitMenuBootstrap : MonoBehaviour
{
    [SerializeField] private InitMenu _initMenu;
    [SerializeField] private SceneLoadConfig _initMenuConfig;

    private void OnValidate()
    {
        if(_initMenu == null)
            throw new ArgumentNullException(nameof(_initMenu), "Init menu cannot be null");

        if(_initMenuConfig == null)
            throw new ArgumentNullException(nameof(_initMenuConfig), "Init menu config cannot be null");
    }

    private void Awake()
    {
        _initMenu.Initialize(_initMenuConfig);
    }
}
