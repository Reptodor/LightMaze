using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirtsLevelBootstrap : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private LevelConfig _levelConfig;

    [Header("Tutorial")]
    [SerializeField] private Tutorial _tutorial;

    [Header("Camera")]
    [SerializeField] private CameraHandler _cameraHandler;

    [Header("Player components")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _spikesTilemap;

    [Header("Quests")]
    [SerializeField] private QuestHandler _questHandler;
    [SerializeField] private QuestAnimationHandlerConfig _questHandlerConfig;

    [Header("Exit")]
    [SerializeField] private ExitHandler _exitHandler;
    private SceneLoader _sceneLoader;

    [Header("Torches")]
    [SerializeField] private HandTorch _baseTorch;
    [SerializeField] private List<GroundTorch> _groundTorches;
    [SerializeField] private HandTorchConfig _handTorchConfig;
    [SerializeField] private FlameAnimationsConfig _flameAnimationsConfig;

    [Header("Slimes")]
    [SerializeField] private Slime[] _slimes;
    [SerializeField] private MovementConfig _slimeMovementConfig;

    [Header("Arrows")]
    [SerializeField] private Arrow[] _arrows;
    [SerializeField] private ArrowConfig _arrowConfig;
    
    [Header("Keys")]
    [SerializeField] protected Key[] Keys;

    [Header("ShakeAnimation")]
    [SerializeField] protected ShakeAnimationConfig ShakeAnimationConfig;


    private void Awake()
    {
        StartCoroutine(nameof(Initialize));
    }

    public virtual IEnumerator Initialize()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        yield return null;

        _cameraHandler.Initialize(_player);

        yield return null;

        _player.Initialize(_spikesTilemap, _sceneLoader, _cameraHandler, _levelConfig);

        yield return null;

        _baseTorch.Initialize(_player, _flameAnimationsConfig);

        yield return null;

        _questHandler.Initialize(_questHandlerConfig, _player.BagHandler);

        yield return null;

        _exitHandler.Initialize(_sceneLoader, _questHandler);

        yield return null;

        _tutorial.Open();

        yield return null;

        foreach (Slime slime in _slimes)
        {
            slime.Initialize(_slimeMovementConfig);

            yield return null;
        }

        foreach (Arrow arrow in _arrows)
        {
            arrow.Initialize(_arrowConfig);

            yield return null;
        }

        foreach (GroundTorch groundTorch in _groundTorches)
            {
                groundTorch.Initialize(_flameAnimationsConfig, ShakeAnimationConfig);

                yield return null;
            }

        foreach (Key key in Keys)
        {
            key.Initialize(ShakeAnimationConfig);

            yield return null;
        }
    }
}
