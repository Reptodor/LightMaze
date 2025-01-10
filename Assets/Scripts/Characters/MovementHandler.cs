using UnityEngine;

public class MovementHandler
{
    private readonly Rigidbody2D _rigidbody2D;
    private float _speed;

    public MovementHandler(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }

    public void SetValues(float speed)
    {
        _speed = speed;
    }

    public bool IsMoving()
    {
        if(_rigidbody2D.velocity != Vector2.zero)
        {
            return true;
        }
        return false;
    }

    public void HandleMovement(Vector2 moveDirection)
    {
        _rigidbody2D.velocity = moveDirection * _speed;
    }
}
