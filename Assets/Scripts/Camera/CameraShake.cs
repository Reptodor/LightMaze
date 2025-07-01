using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private CameraShakeConfig _cameraShakeConfig;
    private CinemachineImpulseSource _impulseSource;

    private void OnValidate()
    {
        if (_cameraShakeConfig == null)
            throw new ArgumentNullException(nameof(_cameraShakeConfig), "CameraShakeConfig cannot be null");
    }

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera()
    {
        _impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = _cameraShakeConfig.Duration;
        _impulseSource.GenerateImpulse(_cameraShakeConfig.Intensity);
    }
}
