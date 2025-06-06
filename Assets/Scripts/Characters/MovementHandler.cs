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
        SetSpeed(_movementConfig.Speed);
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public bool IsMoving()
    {
        if(_rigidbody2D.velocity != Vector2.zero)
            return true;

        _audioSource.clip = _movementConfig.AudioClip;
        _audioSource.Play();
        return false;
    }

    public void HandleMovement(Vector2 moveDirection)
    {
        _rigidbody2D.velocity = moveDirection * _speed;
    }
}
