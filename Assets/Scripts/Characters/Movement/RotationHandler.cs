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
        if(velocityDirection.x > 0)
        {   
            _transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if(velocityDirection.x < 0)
        {
            _transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
