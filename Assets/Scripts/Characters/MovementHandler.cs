using System;
using UnityEngine;

public class MovementHandler
{
    private readonly MovementConfig _movementConfig;
    private readonly Rigidbody2D _rigidbody2D;
    private readonly AudioSource _audioSource;

    private float _speed;
    public float Speed => _movementConfig.Speed;

    public MovementHandler(MovementConfig movementConfig, Rigidbody2D rigidbody2D, AudioSource audioSource)
    {
        _movementConfig = movementConfig;
        _rigidbody2D = rigidbody2D;
        _audioSource = audioSource;
        _audioSource.clip = _movementConfig.AudioClip;
        SetSpeed(_movementConfig.Speed);
    }

    public void SetSpeed(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be below zero");

        _speed = value;
    }

    public bool IsMoving()
    {
        return _rigidbody2D.velocity != Vector2.zero;
    }

    public void HandleMovementWithSound(Vector2 moveDirection)
    {
        if (IsMoving() && !_audioSource.isPlaying)
            _audioSource.Play();
        else if(!IsMoving())
            _audioSource.Stop();

        _rigidbody2D.velocity = moveDirection * _speed;
    }
}
