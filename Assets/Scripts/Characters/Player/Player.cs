using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [Header("Audio")]
    [SerializeField] private AudioSource _runAudioSource;
    [SerializeField] private AudioSource _hitAudioSource;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private AnimationHandler _animationHandler;
    private AnimationSwitchingHandler _animationSwitchingHandler;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private MovementConfig _movementConfig;
    private MovementHandler _movementHandler;
    private OrientationHandler _orientationHandler;
    private RotationHandler _rotationHandler;

    [Header("Bag")]
    private BagHandler _bagHandler;

    [Header("Health")]
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private HealthView _healthView;
    private HealthPresenter _healthPresenter;

    [Header("Input")]
    [SerializeField] private InputConfig _inputConfig;
    private InputSystem _inputSystem;

    [Header("SpeedAbility")]
    [SerializeField] private SpeedAbilityConfig _speedAbilityConfig;
    [SerializeField] private SpeedAbilityView _speedAbilityView;
    private SpeedAbilityPresenter _speedAbilityPresenter;

    [Header("TeleportAbility")]
    [SerializeField] private TeleportAbilityConfig _teleportAbilityConfig;
    [SerializeField] private TeleportAbilityView _teleportAbilityView;
    private TeleportAbilityPresenter _teleportAbilityPresenter;

    [Header("CameraAbility")]
    [SerializeField] private CameraAbilityConfig _cameraAbilityConfig;
    [SerializeField] private CameraAbilityView _cameraAbilityView;
    private CameraAbilityPresenter _cameraAbilityPresenter;

    private CameraHandler _cameraHandler;
    private bool _isInitialized;

    public BagHandler BagHandler => _bagHandler;

    public event Action<int> Damaged;

    private void OnValidate()
    {
        if (_runAudioSource == null)
            throw new ArgumentNullException(nameof(_runAudioSource), "Run audio source cannot be null");

        if (_hitAudioSource == null)
            throw new ArgumentNullException(nameof(_hitAudioSource), "Hit audio source cannot be null");

        if (_animator == null)
            throw new ArgumentNullException(nameof(_animator), "Animator cannot be null");

        if (_spriteRenderer == null)
            throw new ArgumentNullException(nameof(_spriteRenderer), "SpriteRenderer cannot be null");

        if (_rigidbody2D == null)
            throw new ArgumentNullException(nameof(_rigidbody2D), "Rigidbody2D cannot be null");

        if (_movementConfig == null)
            throw new ArgumentNullException(nameof(_movementConfig), "MovementConfig cannot be null");

        if (_healthConfig == null)
            throw new ArgumentNullException(nameof(_healthConfig), "HealthConfig cannot be null");

        if (_healthView == null)
            throw new ArgumentNullException(nameof(_healthView), "HealthView cannot be null");

        if (_inputConfig == null)
            throw new ArgumentNullException(nameof(_inputConfig), "Input config cannot be null");

        if (_speedAbilityConfig == null)
            throw new ArgumentNullException(nameof(_speedAbilityConfig), "SpeedAbilityConfig cannot be null");

        if (_speedAbilityView == null)
            throw new ArgumentNullException(nameof(_speedAbilityView), "SpeedAbilityView cannot be null");

        if (_teleportAbilityConfig == null)
            throw new ArgumentNullException(nameof(_teleportAbilityConfig), "TeleportAbilityConfig cannot be null");

        if (_teleportAbilityView == null)
            throw new ArgumentNullException(nameof(_teleportAbilityView), "TeleportAbilityView cannot be null");

        if (_cameraAbilityConfig == null)
            throw new ArgumentNullException(nameof(_cameraAbilityConfig), "CameraAbilityConfig cannot be null");

        if (_cameraAbilityView == null)
            throw new ArgumentNullException(nameof(_cameraAbilityView), "CameraAbilityView cannot be null");
    }

    public void Initialize(GameObject spikesTilemap, SceneLoader sceneLoader, CameraHandler cameraHandler, LevelConfig levelConfig)
    {
        InitializeMovement(_movementConfig);

        _cameraHandler = cameraHandler;
        _animationSwitchingHandler = new AnimationSwitchingHandler(_animator);
        _bagHandler = new BagHandler(levelConfig.KeysCount, spikesTilemap);
        _animationHandler = new AnimationHandler(_movementHandler, _animationSwitchingHandler, _orientationHandler);
        _inputSystem = new InputSystem(_inputConfig);
        
        InitializeHealth(sceneLoader);
        InitializeSpeedAbility();
        InitializeDashAbility();
        InitializeCameraAbility(levelConfig.CameraFreePosition, levelConfig.CameraUnfollowingOrthoSize);

        _isInitialized = true;
        OnEnable();
    }

    private void InitializeMovement(MovementConfig movementConfig)
    {
        _movementHandler = new MovementHandler(movementConfig, _rigidbody2D, _runAudioSource);
        _orientationHandler = new OrientationHandler();
        _rotationHandler = new RotationHandler(transform);
    }

    private void InitializeHealth(SceneLoader sceneLoader)
    {
        HealthModel healthModel = new HealthModel(_healthConfig.MaxHealth);
        _healthPresenter = new HealthPresenter(healthModel, _healthView, sceneLoader, _animationSwitchingHandler, _healthConfig.DeathTime);
    }

    private void InitializeSpeedAbility()
    {
        _speedAbilityView.Initialize(_inputSystem.SpeedAbilityKey);
        AbilityModel speedAbilityModel = new AbilityModel(_speedAbilityConfig.Duration, _speedAbilityConfig.Cooldown);
        _speedAbilityPresenter = new SpeedAbilityPresenter(_speedAbilityView, speedAbilityModel, _movementHandler, _speedAbilityConfig.BoostPercent);
    }

    private void InitializeDashAbility()
    {
        _teleportAbilityView.Initialize(_inputSystem.TeleportAbilityKey);
        AbilityModel dashAbilityModel = new AbilityModel(_teleportAbilityConfig.Duration, _teleportAbilityConfig.Cooldown);
        _teleportAbilityPresenter = new TeleportAbilityPresenter(_teleportAbilityView, dashAbilityModel, _teleportAbilityConfig.ObstacleLayer, _rigidbody2D, _teleportAbilityConfig.TeleportDistance);
    }

    private void InitializeCameraAbility(Vector3 cameraFreePosition, float cameraUnfollowingOrthoSize)
    {
        _cameraAbilityView.Initialize(_inputSystem.CameraAbilityKey);
        AbilityModel cameraAbilityModel = new AbilityModel(_cameraAbilityConfig.Duration, _cameraAbilityConfig.Cooldown);
        _cameraAbilityPresenter = new CameraAbilityPresenter(_cameraAbilityView, cameraAbilityModel, _cameraAbilityConfig,
                                                             this, cameraFreePosition, _cameraHandler.Camera,
                                                             _cameraHandler.CameraMovementHandler, cameraUnfollowingOrthoSize);
    }

    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        _healthPresenter.Subscribe();
        _speedAbilityPresenter.Subscribe();
        _teleportAbilityPresenter.Subscribe();
        _cameraAbilityPresenter.Subscribe();
        Damaged += _healthPresenter.OnDamaged;
        _inputSystem.SpeedAbilityKeyPressed += _speedAbilityView.OnSpeedAbilityKeyPressed;
        _inputSystem.TeleportAbilityKeyPressed += _teleportAbilityView.OnTeleportAbilityKeyPressed;
        _inputSystem.CameraAbilityKeyPressed += _cameraAbilityView.OnCameraAbilityKeyPressed;
    }

    private void OnDisable()
    {
        _healthPresenter.Unsubscribe();
        _speedAbilityPresenter.Unsubscribe();
        _teleportAbilityPresenter.Unsubscribe();
        _cameraAbilityPresenter.Unsubscribe();
        Damaged -= _healthPresenter.OnDamaged;
        _inputSystem.SpeedAbilityKeyPressed -= _speedAbilityView.OnSpeedAbilityKeyPressed;
        _inputSystem.TeleportAbilityKeyPressed -= _teleportAbilityView.OnTeleportAbilityKeyPressed;
        _inputSystem.CameraAbilityKeyPressed -= _cameraAbilityView.OnCameraAbilityKeyPressed;
    }

    private void Update()
    {
        if (!_isInitialized)
            return;

        if (!_healthPresenter.IsAlive)
        {
            _runAudioSource.enabled = false;
            return;
        }

        _inputSystem.Update();
        _speedAbilityPresenter.Update();
        _teleportAbilityPresenter.Update();
        _teleportAbilityPresenter.UpdateMoveDirection(_inputSystem.GetMoveDirection());
        _cameraAbilityPresenter.Update();
        _animationHandler.HandleAnimations(_inputSystem.GetMoveDirection());
    }

    private void FixedUpdate()
    {
        if(!_isInitialized)
            return;

        if(!_healthPresenter.IsAlive)
        {
            _movementHandler.HandleMovementWithSound(Vector2.zero);
            return;
        }
        
        _movementHandler.HandleMovementWithSound(_inputSystem.GetMoveDirection().normalized);
        _rotationHandler.HandleRotation(_inputSystem.GetMoveDirection());
    }

    public void TakeDamage(int damage)
    {
        Damaged?.Invoke(damage);
    }
}
