using UnityEngine;
using System.Collections;

public class AdvancedBouncingArrow : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AdvancedBouncingArrowConfig _config;
    [SerializeField] private bool _isFlyVertical;

    [Header("Detection")]
    [SerializeField] private LayerMask _wallLayerMask = 1;
    [SerializeField] private float _detectionOffset = 0.1f;

    private Vector2 _firstWallPoint;
    private Vector2 _secondWallPoint;
    private bool _hasDetectedWalls = false;
    private bool _isMovingToSecondPoint = true;
    private bool _isStopped = false;
    private bool _isRotating = false;

    private SpriteRenderer _spriteRenderer;
    private Coroutine _movementCoroutine;
    private Coroutine _rotationCoroutine;

    private Vector2 _currentTarget;
    private Vector2 _nextTarget;
    private Quaternion _targetRotation;
    private Quaternion _startRotation;

    private void Start()
    {
        InitializeComponents();
        DetectWalls();

        if (_hasDetectedWalls)
        {
            StartAdvancedMovement();
        }
    }

    private void InitializeComponents()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _targetRotation = _isMovingToSecondPoint ? Quaternion.identity : Quaternion.Euler(0, 0, 180f);
        transform.rotation = _targetRotation;
    }

    private void DetectWalls()
    {
        Vector2 currentPosition = transform.position;
        
        RaycastHit2D firstHit = Physics2D.Raycast(currentPosition, Vector2.left, Mathf.Infinity, _wallLayerMask);
        RaycastHit2D secondHit = Physics2D.Raycast(currentPosition, Vector2.right, Mathf.Infinity, _wallLayerMask);

        if (_isFlyVertical)
        {
            firstHit = Physics2D.Raycast(currentPosition, Vector2.up, Mathf.Infinity, _wallLayerMask);
            secondHit = Physics2D.Raycast(currentPosition, Vector2.down, Mathf.Infinity, _wallLayerMask);
        }

        if (firstHit.collider != null && secondHit.collider != null)
            {
                _firstWallPoint = firstHit.point + Vector2.right * _detectionOffset;
                _secondWallPoint = secondHit.point + Vector2.left * _detectionOffset;
                _hasDetectedWalls = true;
            }
    }

    private void StartAdvancedMovement()
    {
        _currentTarget = _isMovingToSecondPoint ? _secondWallPoint : _firstWallPoint;
        _nextTarget = _isMovingToSecondPoint ? _firstWallPoint : _secondWallPoint;

        Vector2 directionToTarget = (_currentTarget - (Vector2)transform.position).normalized;
        float startAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        _targetRotation = Quaternion.Euler(0, 0, startAngle);
        transform.rotation = _targetRotation;

        _movementCoroutine = StartCoroutine(AdvancedMovementRoutine());
    }

    private IEnumerator AdvancedMovementRoutine()
    {
        while (true)
        {
            if (!_isStopped)
            {
                yield return StartCoroutine(MoveToTargetSmooth());
            }

            if (!_isRotating)
            {
                _rotationCoroutine = StartCoroutine(RotateDuringStop());
                yield return _rotationCoroutine;
            }

            _isStopped = false;
            _isMovingToSecondPoint = !_isMovingToSecondPoint;

            _currentTarget = _nextTarget;
            _nextTarget = _isMovingToSecondPoint ? _secondWallPoint : _firstWallPoint;
        }
    }

    private IEnumerator MoveToTargetSmooth()
    {
        Vector2 startPosition = transform.position;
        float distance = Vector2.Distance(startPosition, _currentTarget);
        float duration = distance / _config.Speed;
        float elapsed = 0f;

        Quaternion movementRotation = _targetRotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float time = _config.MovementCurve.Evaluate(elapsed / duration);

            transform.position = Vector2.Lerp(startPosition, _currentTarget, time);
            transform.rotation = movementRotation;

            ApplyMovementTilt(time);
            UpdateMovementEffects(time);

            yield return null;
        }

        transform.position = _currentTarget;
        transform.rotation = movementRotation;

        _isStopped = true;
    }

    private IEnumerator RotateDuringStop()
    {
        _isRotating = true;
        float elapsed = 0f;

        Vector2 directionToNextTarget = (_nextTarget - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToNextTarget.y, directionToNextTarget.x) * Mathf.Rad2Deg;

        Quaternion newTargetRotation = Quaternion.Euler(0, 0, targetAngle);

        _startRotation = transform.rotation;
        _targetRotation = newTargetRotation;

        while (elapsed < _config.StopDuration)
        {
            elapsed += Time.deltaTime;
            float time = _config.RotationCurve.Evaluate(elapsed / _config.StopDuration);

            transform.rotation = Quaternion.Slerp(_startRotation, _targetRotation, time);

            yield return null;
        }

        transform.rotation = _targetRotation;
        _isRotating = false;
    }

    private void ApplyMovementTilt(float movementProgress)
    {
        if (!_isRotating)
        {
            float tilt = Mathf.Sin(movementProgress * Mathf.PI * _config.TiltFrequency) * _config.MaxTiltAngle;

            Quaternion tiltedRotation = _targetRotation * Quaternion.Euler(0, 0, tilt);
            transform.rotation = tiltedRotation;
        }
    }

    private void UpdateMovementEffects(float progress)
    {
        float scale = _config.BaseScale + Mathf.Sin(progress * Mathf.PI) * _config.ScaleAmplitude;
        transform.localScale = Vector3.one * scale;

        if (_spriteRenderer != null)
        {
            Color color = _spriteRenderer.color;
            color.a = _config.BaseAlpha + Mathf.Sin(progress * Mathf.PI) * _config.AlphaAmplitude;
            _spriteRenderer.color = color;
        }
    }

    private void OnDestroy()
    {
        if (_movementCoroutine != null)
            StopCoroutine(_movementCoroutine);
        if (_rotationCoroutine != null)
            StopCoroutine(_rotationCoroutine);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        if (_hasDetectedWalls)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_firstWallPoint, _config.GizmosSphereRadius);
            Gizmos.DrawWireSphere(_secondWallPoint, _config.GizmosSphereRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_firstWallPoint, _secondWallPoint);
        }
    }
#endif
}