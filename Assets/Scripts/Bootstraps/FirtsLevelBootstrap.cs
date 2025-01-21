using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirtsLevelBootstrap : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CameraHandler _cameraHandler;
    [SerializeField] private CameraHandlerConfig _cameraHandlerConfig;

    [Header("Player components")]
    [SerializeField] private Player _player;
    [SerializeField] private MovementConfig _movementConfig;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private MeleeCombatConfig _playerMeleeCombatConfig;
    [SerializeField] private BagConfig _bagConfig;
    [SerializeField] private GameObject _spikesTilemap;

    [Header("Torches")]
    [SerializeField] private BaseHandTorch _baseTorch;
    [SerializeField] private HandTorchConfig _handTorchConfig;

    [Space(20)]
    [SerializeField] private List<BaseGroundTorch> _baseGroundTorches;
    [SerializeField] private BaseGroundTorchConfig _baseGroundTorchConfig;
    [SerializeField] private TemporaryGroundTorchConfig _temporaryGroundTorchConfig;

    [Header("Exit")]
    [SerializeField] private ExitHandler _exitHandler;
    [SerializeField] private ExitHandlerConfig _exitHandlerConfig;
    private SceneLoader _sceneLoader;

    [Header("Quests")]
    [SerializeField] private QuestHandler _questHandler;
    [SerializeField] private QuestAnimationHandlerConfig _questHandlerConfig;

    private void Awake()
    {
        StartCoroutine(nameof(Initialize));
    }

    private IEnumerator Initialize()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        yield return null;

        _cameraHandler.Initialize(_cameraHandlerConfig, _player);

        yield return null;

        _player.Initialize(_movementConfig, _healthConfig, _healthView, _playerMeleeCombatConfig, _bagConfig, _spikesTilemap);

        yield return null;

        _baseTorch.Initialize(_handTorchConfig, _player);

        yield return null;

        foreach(var baseGroundTorch in _baseGroundTorches)
        {
            baseGroundTorch.Initialize(_baseGroundTorchConfig, _temporaryGroundTorchConfig);

            yield return null;
        }

        yield return null;

        _questHandler.Initialize(_questHandlerConfig, _player.BagHandler);

        yield return null;

        _exitHandler.Initialize(_exitHandlerConfig, _sceneLoader, _questHandler);
    }
}
