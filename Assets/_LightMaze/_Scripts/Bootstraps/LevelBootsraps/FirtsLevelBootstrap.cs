using System.Collections;
using Cinemachine;
using LightMaze._Scripts.SceneLoader;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirtsLevelBootstrap : MonoBehaviour
{
    [Header("GlobalLight")]
    [SerializeField] private Light2D _globalLight;

    [Header("Level")]
    [SerializeField] private LevelConfig _levelConfig;

    [Header("Player components")]
    [SerializeField] private Transform _spawnpoint;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GameObject _spikesTilemap;
    private Player _player;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera _followingCinemachineVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera _unfollowingCinemachineVirtualCamera;
    [SerializeField] private CameraShake _cameraShake;

    [Header("Quests")]
    [SerializeField] private QuestHandler _questHandler;
    [SerializeField] private QuestAnimationHandlerConfig _questHandlerConfig;

    [Header("Exit")]
    [SerializeField] private ExitHandler _exitHandler;
    private SceneLoader _sceneLoader;

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

        _globalLight.intensity = 0f;

        yield return null;

        PauseMenu pauseMenu = Instantiate(_pauseMenuPrefab, _interfaceParent);
        SettingsMenu settingsMenu = Instantiate(_settingsMenuPrefab, _interfaceParent);
        pauseMenu.Initialize(_sceneLoader, settingsMenu);

        yield return null;

        _player = Instantiate(_playerPrefab);
        _player.transform.position = _spawnpoint.position;
        _player.Initialize(_spikesTilemap, _sceneLoader, _levelConfig, _cameraShake, pauseMenu, _unfollowingCinemachineVirtualCamera);

        yield return null;

        _followingCinemachineVirtualCamera.Follow = _player.transform;

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
