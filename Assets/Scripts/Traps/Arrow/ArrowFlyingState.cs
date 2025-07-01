using UnityEngine;

public class ArrowFlyingState : IArrowState
{
    public void EnterState(Arrow arrow)
    {
        arrow.UpdateRotation();
    }

    public void UpdateState(Arrow arrow)
    {
        arrow.transform.position = Vector2.MoveTowards(arrow.transform.position, arrow.CurrentTarget, arrow.Speed * Time.deltaTime);

        if (Vector2.Distance(arrow.transform.position, arrow.CurrentTarget) < 0.1f)
        {
            arrow.ChangeState(new ArrowPausedState());
        }
    }

    public void ExitState(Arrow arrow)
    {
        arrow.Disable();
    }
}
