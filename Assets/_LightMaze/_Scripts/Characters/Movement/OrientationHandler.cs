using UnityEngine;

public class OrientationHandler
{
    private string _currentSide = "Front";

    public string GetOrientationName(Vector2 velocityDirection)
    {
        if(velocityDirection.x != 0)
        {
            _currentSide = "Side";
        }
        else if(velocityDirection.y > 0)
        {
            _currentSide = "Back";
        }
        else if(velocityDirection.y < 0)
        {
            _currentSide = "Front";
        }

        return _currentSide;
    }
}
