using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private HealthView _healthView;
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

    [Header("Boosts")]
    [SerializeField] private SpeedBoostHandler _speedBoost;
    [SerializeField] private FlameBoostHandler _flameBoost;
    [SerializeField] private BoostConfig _speedBoostConfig;
    [SerializeField] private BoostConfig _flameBoostConfig;
    [SerializeField] private TextMeshProUGUI _speedBoostKey;
    [SerializeField] private TextMeshProUGUI _flameBoostKey;

    [Header("Input")]
    [SerializeField] private DesktopInputConfig _desktopInputConfig;
    private DesktopInput _desktopInput;

    private void Awake()
    {
        StartCoroutine(nameof(Initialize));
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public virtual IEnumerator Initialize()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        yield return null;

        _cameraHandler.Initialize(_player, _levelConfig.CameraFreePosition, _levelConfig.CameraUnfollowingOrthoSize);

        yield return null;

        _player.Initialize(_healthView, _spikesTilemap, _sceneLoader, _cameraHandler, ChooseAndGetInputSystem(), _levelConfig.KeysCount);

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

        _speedBoost.Initialize(_speedBoostConfig, _player.MovementHandler);

        yield return null;

        _flameBoost.Initialize(_flameBoostConfig, _baseTorch);

        yield return null;

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Subscribe();
            _tutorial?.transform.DOScale(7, 1).From(0).SetEase(Ease.OutBounce);
            _flameBoostKey.gameObject.SetActive(true);
            _speedBoostKey.gameObject.SetActive(true);
            _flameBoostKey.text = _flameBoostConfig.Key.ToString();
            _speedBoostKey.text = _speedBoostConfig.Key.ToString();
        }
        else
        {
            _tutorial.gameObject.SetActive(false);
        }
    }

    private IInput ChooseAndGetInputSystem()
    {
        _desktopInput = new DesktopInput(_desktopInputConfig);
        IInput input = _desktopInput;
        _mobileCanvas.gameObject.SetActive(false);

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _mobileCanvas.gameObject.SetActive(true);
            input = new MobileInput(_joystick);
        }

        return input;
    }

    private void Subscribe()
    {
        _desktopInput.SpeedBoostKeyPressed += _speedBoost.Use;
        _desktopInput.FlameBoostKeyPressed += _flameBoost.Use;
    }

    private void Unsubscribe()
    {
        _desktopInput.SpeedBoostKeyPressed -= _speedBoost.Use;
        _desktopInput.FlameBoostKeyPressed -= _flameBoost.Use;
    }
}
