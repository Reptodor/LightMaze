using UnityEngine;

public class EnemyTurnState : State
{
    public EnemyTurnState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in EnemyTurnState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from EnemyTurnState");
    }
}
