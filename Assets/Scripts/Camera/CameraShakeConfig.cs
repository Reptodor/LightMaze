using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraShakeConfig", menuName = "Configs/Camera/Shake")]
public class CameraShakeConfig : ScriptableObject
{
    [SerializeField] private float _intensity;
    [SerializeField] private float _duration;

    public float Intensity => _intensity;
    public float Duration => _duration;

    private void OnValidate()
    {
        if (_intensity <= 0)
            throw new ArgumentOutOfRangeException(nameof(_intensity), "Intensity must be greater than null");

        if (_duration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_duration), "Duration must be greater than zero");
    }
}
