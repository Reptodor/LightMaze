using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementConfig", menuName = "Configs/Characters/Movement")]
public class MovementConfig : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _speed;

    public AudioClip AudioClip => _audioClip;
    public float Speed => _speed;

    private void OnValidate()
    {
        if(_audioClip == null)
            throw new ArgumentNullException(nameof(_audioClip), "Audio clip cannot be null");

        if(_speed <= 0)
            throw new ArgumentOutOfRangeException(nameof(_speed), "Speed must be greater than zero");
    }
}
