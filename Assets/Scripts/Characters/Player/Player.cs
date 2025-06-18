using System;
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

    [Header("SpeedBoost")]
    [SerializeField] private BoostConfig _speedBoostConfig;
    [SerializeField] private SpeedBoostView _speedBoostView;
    private SpeedBoostPresenter _speedBoostPresenter;

    private bool _isInitialized;

    public MovementHandler MovementHandler => _movementHandler;
    public BagHandler BagHandler => _bagHandler;
    public InputSystem InputSystem => _inputSystem;

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

        if (_speedBoostConfig == null)
            throw new ArgumentNullException(nameof(_speedBoostConfig), "SpeedBoostConfig cannot be null");

        if (_speedBoostView == null)
            throw new ArgumentNullException(nameof(_speedBoostView), "SpeedBoostView cannot be null");
    }

    public void Initialize(GameObject spikesTilemap, SceneLoader sceneLoader, int keysCount)
    {
        InitializeMovement(_movementConfig);

        _animationSwitchingHandler = new AnimationSwitchingHandler(_animator);
        _bagHandler = new BagHandler(keysCount, spikesTilemap);
        _animationHandler = new AnimationHandler(_movementHandler, _animationSwitchingHandler, _orientationHandler);
        _inputSystem = new InputSystem(_inputConfig);
        
        InitializeHealth(sceneLoader);
        InitializeSpeedBoost();

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

    private void InitializeSpeedBoost()
    {
        SpeedBoostModel speedBoostModel = new SpeedBoostModel(_speedBoostConfig.Duration, _speedBoostConfig.Cooldown);
        _speedBoostPresenter = new SpeedBoostPresenter(_speedBoostView, speedBoostModel, _movementHandler, _speedBoostConfig.BoostPercent);
    }

    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        _healthPresenter.Subscribe();
        _speedBoostPresenter.Subscribe();
        Damaged += _healthPresenter.OnDamaged;
    }

    private void OnDisable()
    {
        _healthPresenter.Unsubscribe();
        _speedBoostPresenter.Unsubscribe();
        Damaged -= _healthPresenter.OnDamaged;
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
        _speedBoostPresenter.Update();
        _animationHandler.HandleAnimations(_inputSystem.GetMoveDirection());

        if (Input.GetKeyDown(KeyCode.V))
            TakeDamage(1);
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
