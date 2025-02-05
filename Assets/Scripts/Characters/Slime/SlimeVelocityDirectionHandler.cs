using UnityEngine;

public class SlimeVelocityDirectionHandler
{
    private float _movementDistance;
    private Slime _slime;
    private Vector3 _currentVelocityDirection;
    private Vector2 _startPosition;
    private Vector2 _leftBorder;
    private Vector2 _rightBorder;

    public SlimeVelocityDirectionHandler(Slime slime, float movementDistance)
    {
        _slime = slime;
        _movementDistance = movementDistance;
        SetBorders();
    }

    private void SetBorders()
    {
        _startPosition = _slime.transform.position;
        _leftBorder = _startPosition - new Vector2(_movementDistance, 0);
        _rightBorder = _startPosition + new Vector2(_movementDistance, 0);
        _currentVelocityDirection = -_slime.transform.right;
    }

    public Vector3 GetVelocityDirection()
    {
        if(_slime.transform.position.x <= _leftBorder.x + 0.1f)
        {
            _currentVelocityDirection = new Vector3(1, 0, 0);
        }
        if(_slime.transform.position.x >= _rightBorder.x - 0.1f)
        {
            _currentVelocityDirection = new Vector3(-1, 0, 0);
        }

        return _currentVelocityDirection;
    }
}
