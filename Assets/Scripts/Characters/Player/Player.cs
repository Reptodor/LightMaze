using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamagable
{
    private bool _isInitialized;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private AnimationHandler _animationHandler;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    private AnimationSwitchingHandler _animationSwitchingHandler;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private MovementHandler _movementHandler;
    private PlayerInputHandler _playerInputHandler;
    private OrientationHandler _orientationHandler;
    private RotationHandler _rotationHandler;

    [Header("Combat")]
    private MeleeCombatHandler _meleeCombatHandler;

    [Header("Health")]
    private Health _health;
    public Health Health => _health;

    private void OnValidate()
    {
        if(_spriteRenderer == null)
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if(_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if(_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }

    public void Initialize(HealthConfig healthConfig, HealthView healthView, MeleeCombatConfig meleeCombatConfig, float movementSpeed)
    {
        _animationSwitchingHandler = new AnimationSwitchingHandler(_animator);
        InitializeMovement(movementSpeed);
        _meleeCombatHandler = new MeleeCombatHandler(meleeCombatConfig);
        _animationHandler = new AnimationHandler(_movementHandler, _meleeCombatHandler, _animationSwitchingHandler, _orientationHandler);
        InitializeHealth(healthConfig, healthView);
        _isInitialized = true;
        OnEnable();
    }

    private void InitializeMovement(float movementSpeed)
    {
        _playerInputHandler = new PlayerInputHandler();
        _movementHandler = new MovementHandler(_rigidbody2D);
        _orientationHandler = new OrientationHandler();
        _rotationHandler = new RotationHandler(transform);
        _movementHandler.SetValues(movementSpeed);
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
