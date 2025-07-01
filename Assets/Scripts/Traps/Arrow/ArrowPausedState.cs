using UnityEngine;

public class ArrowPausedState : IArrowState
{
    private float _pauseTimer;

    public void EnterState(Arrow arrow)
    {
        _pauseTimer = arrow.PauseDuration;
    }

    public void UpdateState(Arrow arrow)
    {
        _pauseTimer -= Time.deltaTime;
        
        if (_pauseTimer <= 0)
        {
            arrow.SwitchTarget();
            arrow.ChangeState(new ArrowFlyingState());
        }
    }

    public void ExitState(Arrow arrow)
    {
        arrow.Enable();
    }
}
