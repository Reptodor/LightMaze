using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamagable
{
    private bool _isInitialized;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
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

    public MovementHandler MovementHandler => _movementHandler;

    [Header("Bag")]
    [SerializeField] private BagConfig _bagConfig;
    private BagHandler _bagHandler;

    public BagHandler BagHandler => _bagHandler;

    [Header("Health")]
    [SerializeField] private HealthConfig _healthConfig;
    private Health _health;
    public Health Health => _health;

    private void OnValidate()
    {
        if(_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        if(_spriteRenderer == null)
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if(_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if(_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }

    public void Initialize(HealthView healthView, GameObject spikesTilemap,
                           SceneLoader sceneLoader, CameraHandler cameraHandler, IInput input)
    {
        InitializeMovement(_movementConfig, input);
        InitializeHealth(_healthConfig, healthView, sceneLoader, cameraHandler);
        
        _animationSwitchingHandler = new AnimationSwitchingHandler(_animator);
        _bagHandler = new BagHandler(_bagConfig, spikesTilemap);
        _animationHandler = new AnimationHandler(_movementHandler, _animationSwitchingHandler, _orientationHandler);
        
        
        _isInitialized = true;
        OnEnable();
    }

    private void InitializeMovement(MovementConfig movementConfig, IInput input)
    {
        _input = input;
        _movementHandler = new MovementHandler(movementConfig, _rigidbody2D, _audioSource);
        _orientationHandler = new OrientationHandler();
        _rotationHandler = new RotationHandler(transform);
    }

    private void InitializeHealth(HealthConfig healthConfig, HealthView healthView, SceneLoader sceneLoader, CameraHandler cameraHandler)
    {
        _health = new Health(healthConfig, healthView, _spriteRenderer, sceneLoader, cameraHandler, _hitAudioSource);
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
            _audioSource.enabled = false;
            return;
        }

        _animationHandler.HandleAnimations(_input.GetInputDirection());

        if(Input.GetKeyDown(KeyCode.P))
        {
            Health.RecieveDamage(1);
        }
    }

    private void FixedUpdate()
    {
        if(!_isInitialized)
            return;

        if(!_health.IsAlive)
        {
            _movementHandler.HandleMovement(Vector2.zero);
            return;
        }
        
        _movementHandler.HandleMovement(_input.GetInputDirection().normalized);
        _rotationHandler.HandleRotation(_input.GetInputDirection());
    }

    private void OnDied()
    {
        _animationSwitchingHandler.ChangeAnimation("Death");
    }
}
