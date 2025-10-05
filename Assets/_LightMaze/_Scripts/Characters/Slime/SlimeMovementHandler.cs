using UnityEngine;

public class SlimeMovementHandler
{
    private float _movementDistance;
    private float _speed;
    private Slime _slime;
    private Vector2 _startPosition;
    private Vector2 _leftBorder;
    private Vector2 _rightBorder;
    private Vector2 _destination;

    public SlimeMovementHandler(Slime slime, float movementDistance, float speed)
    {
        _slime = slime;
        _movementDistance = movementDistance;
        _speed = speed;
        SetBorders();
    }

    private void SetBorders()
    {
        _startPosition = _slime.transform.position;
        _leftBorder = _startPosition - new Vector2(_movementDistance, 0);
        _rightBorder = _startPosition + new Vector2(_movementDistance, 0);
        _destination = _leftBorder;
    }

    public void HandleMovement()
    {
        if(_slime.transform.position.x <= _leftBorder.x + 0.1f)
        {
            _destination = _rightBorder;
            _slime.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if(_slime.transform.position.x >= _rightBorder.x - 0.1f)
        {
            _destination = _leftBorder;
            _slime.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _slime.transform.position = Vector2.MoveTowards(_slime.transform.position, _destination, _speed * Time.deltaTime);
    }
}
