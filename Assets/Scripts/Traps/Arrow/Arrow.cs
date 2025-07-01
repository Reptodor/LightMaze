using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    private ArrowConfig _arrowConfig;
    private IArrowState _currentState;
    private bool _canDamage;

    public Vector2 CurrentTarget { get; private set; }
    public float Speed => _arrowConfig.Speed;
    public float PauseDuration => _arrowConfig.PauseDuration;

    public void Initialize(ArrowConfig arrowConfig)
    {
        _arrowConfig = arrowConfig;
        transform.position = _startPoint.position;
        CurrentTarget = _endPoint.position;

        ChangeState(new ArrowFlyingState());
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
    }

    public void ChangeState(IArrowState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState?.EnterState(this);
    }

    public void SwitchTarget()
    {
        CurrentTarget = CurrentTarget == (Vector2)_endPoint.position ? _startPoint.position : _endPoint.position;
    }

    public void Enable()
    {
        _canDamage = true;
        _spriteRenderer.enabled = true;
    }

    public void Disable()
    {
        _canDamage = false;
        _spriteRenderer.enabled = false;
    }

    public void UpdateRotation()
    {
        Vector2 direction = CurrentTarget - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagableObject = other.GetComponent<IDamagable>();

        if (damagableObject != null)
        {
            if (_canDamage)
            {
                damagableObject.TakeDamage(_arrowConfig.Damage);
            }
        }
    }
}
