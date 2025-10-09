using System.Collections;
using System.Collections.Generic;
using LightMaze._Scripts.SceneLoader;
using UnityEngine;

public class FirtsLevelBootstrap : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private LevelConfig _levelConfig;

    [Header("Player components")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _spikesTilemap;

    [Header("Quests")]
    [SerializeField] private QuestHandler _questHandler;
    [SerializeField] private QuestAnimationHandlerConfig _questHandlerConfig;

    [Header("Exit")]
    [SerializeField] private ExitHandler _exitHandler;
    private SceneLoader _sceneLoader;

    [Header("Torch")]
    [SerializeField] private HandTorch _baseTorch;

    [Header("Slimes")]
    [SerializeField] private Slime[] _slimes;

    [Header("Menues")]
    [SerializeField] private Transform _interfaceParent;
    [SerializeField] private PauseMenu _pauseMenuPrefab;
    [SerializeField] private SettingsMenu _settingsMenuPrefab;

    private void Awake()
    {
        StartCoroutine(nameof(Initialize));
    }

    public virtual IEnumerator Initialize()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        yield return null;

        PauseMenu pauseMenu = Instantiate(_pauseMenuPrefab, _interfaceParent);
        SettingsMenu settingsMenu = Instantiate(_settingsMenuPrefab, _interfaceParent);
        pauseMenu.Initialize(_sceneLoader, settingsMenu);

        yield return null;

        _player.Initialize(_spikesTilemap, _sceneLoader, _levelConfig, pauseMenu);

        yield return null;

        _baseTorch.Initialize(_player);

        yield return null;

        _questHandler.Initialize(_questHandlerConfig, _player.BagHandler);

        yield return null;

        _exitHandler.Initialize(_sceneLoader, _questHandler);

        yield return null;

        foreach (Slime slime in _slimes)
        {
            slime?.Initialize(_player);

            yield return null;
        }
    }
}
