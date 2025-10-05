using UnityEngine;

[CreateAssetMenu(fileName = "New AdvancedBouncingArrowConfig", menuName = "Configs/Traps/Arrow")]
public class AdvancedBouncingArrowConfig : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _stopDuration = 0.5f;
    [SerializeField] private AnimationCurve _movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSmoothing = 2f;
    [SerializeField] private float _maxTiltAngle = 5f;
    [SerializeField] private AnimationCurve _rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Animation Settings")]
    [SerializeField] private float _baseScale = 1f;
    [SerializeField] private float _baseAlpha = 0.8f;
    [SerializeField] private float _alphaAmplitude = 0.2f;
    [SerializeField] private float _scaleAmplitude = 0.15f;

    [Header("Other settings")]
    [SerializeField] private float _tiltFrequency = 4f;
    [SerializeField] private float _gizmosSphereRadius = 0.15f;

    public float Speed => _speed;
    public float StopDuration => _stopDuration;
    public AnimationCurve MovementCurve => _movementCurve;

    public float RotationSmoothing => _rotationSmoothing;
    public float MaxTiltAngle => _maxTiltAngle;
    public AnimationCurve RotationCurve => _rotationCurve;

    public float BaseScale => _baseScale;
    public float BaseAlpha => _baseAlpha;
    public float AlphaAmplitude => _alphaAmplitude;
    public float ScaleAmplitude => _scaleAmplitude;

    public float TiltFrequency => _tiltFrequency;
    public float GizmosSphereRadius => _gizmosSphereRadius;
}
