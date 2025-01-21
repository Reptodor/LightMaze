using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private AudioSource _audioSource;
    private bool _isInitialized;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private AnimationHandler _animationHandler;
    private AnimationSwitchingHandler _animationSwitchingHandler;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private MovementHandler _movementHandler;
    private PlayerInputHandler _playerInputHandler;
    private OrientationHandler _orientationHandler;
    private RotationHandler _rotationHandler;

    [Header("Combat")]
    private MeleeCombatHandler _meleeCombatHandler;

    [Header("Bag")]
    private BagHandler _bagHandler;

    public BagHandler BagHandler => _bagHandler;

    [Header("Health")]
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

    public void Initialize(MovementConfig movementConfig, HealthConfig healthConfig, HealthView healthView, MeleeCombatConfig meleeCombatConfig, BagConfig bagConfig, GameObject spikesTilemap)
    {
        _animationSwitchingHandler = new AnimationSwitchingHandler(_animator);
        InitializeMovement(movementConfig);
        _meleeCombatHandler = new MeleeCombatHandler(meleeCombatConfig);
        _bagHandler = new BagHandler(bagConfig, spikesTilemap);
        _animationHandler = new AnimationHandler(_movementHandler, _meleeCombatHandler, _animationSwitchingHandler, _orientationHandler);
        InitializeHealth(healthConfig, healthView);

        _isInitialized = true;
        OnEnable();
    }

    private void InitializeMovement(MovementConfig movementConfig)
    {
        _playerInputHandler = new PlayerInputHandler();
        _movementHandler = new MovementHandler(movementConfig, _rigidbody2D, _audioSource);
        _orientationHandler = new OrientationHandler();
        _rotationHandler = new RotationHandler(transform);
    }

    private void InitializeHealth(HealthConfig healthConfig, HealthView healthView)
    {
        _health = new Health(healthConfig, healthView, _spriteRenderer);
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
            return;

        _animationHandler.HandleAnimations(_playerInputHandler.GetInputDirection());

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _meleeCombatHandler.Attack();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Health.RecieveDamage(1);
        }
    }

    private void FixedUpdate()
    {
        if(!_isInitialized)
            return;

        if(!_health.IsAlive || _meleeCombatHandler.IsAttacking)
        {
            _movementHandler.HandleMovement(Vector2.zero);
            return;
        }
        
        _movementHandler.HandleMovement(_playerInputHandler.GetInputDirection().normalized);
        _rotationHandler.HandleRotation(_playerInputHandler.GetInputDirection());
    }

    private void OnDied()
    {
        _animationSwitchingHandler.ChangeAnimation("Death");
    }
}
