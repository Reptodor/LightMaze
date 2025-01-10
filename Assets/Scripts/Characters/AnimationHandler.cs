using UnityEngine;

public class AnimationHandler 
{
    private MovementHandler _movementHandler;
    private MeleeCombatHandler _meleeCombatHandler;
    private AnimationSwitchingHandler _animationSwitchingHandler;
    private OrientationHandler _orientationHandler;
    private string _orientationName;

    public AnimationHandler(MovementHandler movementHandler, MeleeCombatHandler meleeCombatHandler,
                            AnimationSwitchingHandler animationSwitchingHandler, OrientationHandler orientationHandler)
    {
        _movementHandler = movementHandler;
        _meleeCombatHandler = meleeCombatHandler;
        _animationSwitchingHandler = animationSwitchingHandler;
        _orientationHandler = orientationHandler;
    }

    public void HandleAnimations(Vector2 velocityDirection)
    {        
        if(_meleeCombatHandler.IsAttacking)
        {
            _animationSwitchingHandler.ChangeAnimation($"Attack{_orientationName}");
            return;
        }
        
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
