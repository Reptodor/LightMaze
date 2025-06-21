using System;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private AudioSource _slimeWalkAudioSource;
    [SerializeField] private int _damage;
    [SerializeField] private float _movementDistance;
    private MovementHandler _movementHandler;
    private RotationHandler _rotationHandler;
    private SlimeVelocityDirectionHandler _slimeVelocityDirectionHandler;
    private Vector3 _startPosition;
    private bool _isInitialized = false;

    private void OnValidate()
    {
        if (_rigidbody2D == null)
            throw new ArgumentNullException(nameof(_rigidbody2D), "Rigidbody2D cannot be null");

        if (_slimeWalkAudioSource == null)
            throw new ArgumentNullException(nameof(_slimeWalkAudioSource), "SlimeWalkAudioSource cannot be null");
    }

    public void Initialize(MovementConfig movementConfig, float movementDistance)
    {
        InitializeMovement(movementConfig, movementDistance);
        _startPosition = transform.position;
        _isInitialized = true;
    }

    private void InitializeMovement(MovementConfig movementConfig, float movementDistance)
    {
        if(_movementDistance == 0)
            _movementDistance = movementDistance;

        _movementHandler = new MovementHandler(movementConfig, _rigidbody2D, _slimeWalkAudioSource);
        _rotationHandler = new RotationHandler(transform);

        _slimeVelocityDirectionHandler = new SlimeVelocityDirectionHandler(this, _movementDistance);
    }

    private void FixedUpdate()
    {
        if(!_isInitialized)
            return;

        _movementHandler.HandleMovementWithSound(_slimeVelocityDirectionHandler.GetVelocityDirection());
        _rotationHandler.HandleRotation(_slimeVelocityDirectionHandler.GetVelocityDirection());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_startPosition - new Vector3(_movementDistance, 0, 0), _startPosition + new Vector3(_movementDistance, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(_damage);
        }
    }
}
