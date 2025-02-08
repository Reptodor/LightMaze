using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FirtsLevelBootstrap : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] private Tutorial _tutorial;

    [Header("Camera")]
    [SerializeField] private CameraHandler _cameraHandler;
    [SerializeField] private CameraHandlerConfig _cameraHandlerConfig;

    [Header("Player components")]
    [SerializeField] private Player _player;
    [SerializeField] private MovementConfig _movementConfig;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private BagConfig _bagConfig;
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
    [SerializeField] private ExitHandlerConfig _exitHandlerConfig;
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

    private void Awake()
    {
        StartCoroutine(nameof(Initialize));
    }

    public virtual IEnumerator Initialize()
    {
        _sceneLoader = FindAnyObjectByType<SceneLoader>();

        yield return null;

        _cameraHandler.Initialize(_cameraHandlerConfig, _player);

        yield return null;

        IInput input = new DesktopInputHandler();
        _mobileCanvas.gameObject.SetActive(false);

        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            _mobileCanvas.gameObject.SetActive(true);
            input = new MobileInputHandler(_joystick);
        }

        _player.Initialize(_movementConfig, _healthConfig, _healthView, _bagConfig, _spikesTilemap, _sceneLoader, _cameraHandler, input);

        yield return null;

        _baseTorch.Initialize(_handTorchConfig, _player, _flameAnimationsConfig);

        yield return null;

        foreach(var baseGroundTorch in _baseGroundTorches)
        {
            baseGroundTorch.Initialize(_flameAnimationsConfig, ShakeAnimationConfig);

            yield return null;
        }

        yield return null;

        _questHandler.Initialize(_questHandlerConfig, _player.BagHandler);

        yield return null;

        _exitHandler.Initialize(_exitHandlerConfig, _sceneLoader, _questHandler);

        yield return null;

        foreach(Key key in Keys)
        {
            key.Initialize(ShakeAnimationConfig);
            
            yield return null;
        }

        _speedBoost.Initialize(_speedBoostConfig, _player.MovementHandler);

        yield return null;

        _flameBoost.Initialize(_flameBoostConfig, _baseTorch);

        yield return null;

        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
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
}
