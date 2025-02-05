using UnityEngine;

public class AnimationHandler 
{
    private MovementHandler _movementHandler;
    private AnimationSwitchingHandler _animationSwitchingHandler;
    private OrientationHandler _orientationHandler;
    private string _orientationName;

    public AnimationHandler(MovementHandler movementHandler, AnimationSwitchingHandler animationSwitchingHandler,
                            OrientationHandler orientationHandler)
    {
        _movementHandler = movementHandler;
        _animationSwitchingHandler = animationSwitchingHandler;
        _orientationHandler = orientationHandler;
    }

    public void HandleAnimations(Vector2 velocityDirection)
    {        
        _orientationName = _orientationHandler.GetOrientationName(velocityDirection);

        if(_movementHandler.IsMoving())
        {
            _animationSwitchingHandler.ChangeAnimation($"Walk{_orientationName}");
        }
        else
        {
            _animationSwitchingHandler.ChangeAnimation($"Idle{_orientationName}");
        }
    }    
}
