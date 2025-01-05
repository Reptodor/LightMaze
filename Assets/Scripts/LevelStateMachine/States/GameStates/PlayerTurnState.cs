using UnityEngine;

public class PlayerTurnState : State
{
    public PlayerTurnState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in PlayerTurnState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from PlayerTurnState");
    }
}
