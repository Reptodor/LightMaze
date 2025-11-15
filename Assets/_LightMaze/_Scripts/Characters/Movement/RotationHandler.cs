using UnityEngine;

public class RotationHandler
{
    private readonly Transform _transform;
    private readonly Vector3 _startScale;

    public RotationHandler(Transform transform)
    {
        _transform = transform;
        _startScale = _transform.localScale;
    }

    public void HandleRotation(Vector2 velocityDirection)
    {
        if (velocityDirection.x > 0)
        {
            _transform.localScale = new Vector3(-_startScale.x, _startScale.y, _startScale.z);
        }
        if (velocityDirection.x < 0)
        {
            _transform.localScale = new Vector3(_startScale.x, _startScale.y, _startScale.z);
        }
    }
}
