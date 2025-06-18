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

    [Header("MobileComponents")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Canvas _mobileCanvas;

    [Header("Torches")]
    [SerializeField] private HandTorch _baseTorch;
    [SerializeField] private List<GroundTorch> _baseGroundTorches;
    [SerializeField] private HandTorchConfig _handTorchConfig;
    [SerializeField] private FlameAnimationsConfig _flameAnimationsConfig;

    [Header("Exit")]
    [SerializeField] private ExitHandler _exitHandler;
    private SceneLoader _sceneLoader;

    [Header("Quests")]
    [SerializeField] private QuestHandler _questHandler;
    [SerializeField] private QuestAnimationHandlerConfig _questHandlerConfig;

    [Header("Keys")]
    [SerializeField] protected Key[] Keys;

    [Header("ShakeAnimation")]
    [SerializeField] protected ShakeAnimationConfig ShakeAnimationConfig;

    // [Header("Boosts")]
    // [SerializeField] private SpeedBoost _speedBoost;
    // [SerializeField] private FlameBoostHandler _flameBoost;
    // [SerializeField] private TextMeshProUGUI _speedBoostKey;
    // [SerializeField] private TextMeshProUGUI _flameBoostKey;

    private void Awake()
    {
        StartCoroutine(nameof(Initialize));
    }

    // private void OnDisable()
    // {
    //     Unsubscribe();
    // }

    public virtual IEnumerator Initialize()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        yield return null;

        _cameraHandler.Initialize(_player, _levelConfig.CameraFreePosition, _levelConfig.CameraUnfollowingOrthoSize);

        yield return null;

        _player.Initialize(_spikesTilemap, _sceneLoader, _levelConfig.KeysCount);

        yield return null;

        _baseTorch.Initialize(_player, _flameAnimationsConfig);

        yield return null;

        foreach (var baseGroundTorch in _baseGroundTorches)
        {
            baseGroundTorch.Initialize(_flameAnimationsConfig, ShakeAnimationConfig);

            yield return null;
        }

        yield return null;

        _questHandler.Initialize(_questHandlerConfig, _player.BagHandler);

        yield return null;

        _exitHandler.Initialize(_sceneLoader, _questHandler);

        yield return null;

        foreach (Key key in Keys)
        {
            key.Initialize(ShakeAnimationConfig);

            yield return null;
        }

        // _speedBoost.Initialize(_player.MovementHandler);

        yield return null;

        // _flameBoost.Initialize(_baseTorch);

        yield return null;

        _tutorial.Open();

        yield return null;

        // InitializeBoosts();
    }

    // private void InitializeBoosts()
    // {
    //     Subscribe();
        
    //     _speedBoostKey.text = _player.InputSystem.InputConfig.SpeedBoostKey.ToString();
    //     _flameBoostKey.text = _player.InputSystem.InputConfig.FlameBoostKey.ToString();
    // }

    // private void Subscribe()
    // {
    //     _player.InputSystem.SpeedBoostKeyPressed += _speedBoost.Use;
    //     _player.InputSystem.FlameBoostKeyPressed += _flameBoost.Use;
    // }

    // private void Unsubscribe()
    // {
    //     _player.InputSystem.SpeedBoostKeyPressed -= _speedBoost.Use;
    //     _player.InputSystem.FlameBoostKeyPressed -= _flameBoost.Use;
    // }
}
