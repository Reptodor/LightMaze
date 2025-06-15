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
    private IInput _input;
    private OrientationHandler _orientationHandler;
    private RotationHandler _rotationHandler;

    [Header("Bag")]
    private BagHandler _bagHandler;

    [Header("Health")]
    [SerializeField] private HealthConfig _healthConfig;
    private Health _health;

    private bool _isInitialized;

    public MovementHandler MovementHandler => _movementHandler;
    public BagHandler BagHandler => _bagHandler;
    public Health Health => _health;

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
    }

    public void Initialize(HealthView healthView, GameObject spikesTilemap,
                           SceneLoader sceneLoader, CameraHandler cameraHandler, IInput input, int keysCount)
    {
        InitializeMovement(_movementConfig, input);

        _health = new Health(_healthConfig, healthView, _spriteRenderer, sceneLoader, cameraHandler, _hitAudioSource);
        _animationSwitchingHandler = new AnimationSwitchingHandler(_animator);
        _bagHandler = new BagHandler(keysCount, spikesTilemap);
        _animationHandler = new AnimationHandler(_movementHandler, _animationSwitchingHandler, _orientationHandler);

        _isInitialized = true;
        OnEnable();
    }

    private void InitializeMovement(MovementConfig movementConfig, IInput input)
    {
        _input = input;
        _movementHandler = new MovementHandler(movementConfig, _rigidbody2D, _runAudioSource);
        _orientationHandler = new OrientationHandler();
        _rotationHandler = new RotationHandler(transform);
    }

    private void OnEnable()
    {
        if(!_isInitialized)
            return;

        _health.Subscribe();
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Unsubscribe();
        _health.Died += OnDied;
    }

    private void Update()
    {
        if(!_isInitialized)
            return;

        if(!_health.IsAlive)
        {
            _runAudioSource.enabled = false;
            return;
        }

        _input.Update();
        _animationHandler.HandleAnimations(_input.GetMoveDirection());
    }

    private void FixedUpdate()
    {
        if(!_isInitialized)
            return;

        if(!_health.IsAlive)
        {
            _movementHandler.HandleMovementWithSound(Vector2.zero);
            return;
        }
        
        _movementHandler.HandleMovementWithSound(_input.GetMoveDirection().normalized);
        _rotationHandler.HandleRotation(_input.GetMoveDirection());
    }

    private void OnDied()
    {
        _animationSwitchingHandler.ChangeAnimation("Death");
    }
}
