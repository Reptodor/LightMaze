using UnityEngine;

public class RotationHandler
{
    private Transform _transform;

    public RotationHandler(Transform transform)
    {
        _transform = transform;
    }

    public void HandleRotation(Vector2 velocityDirection)
    {
        if (velocityDirection.x > 0)
        {
            _transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocityDirection.x < 0)
        {
            _transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
