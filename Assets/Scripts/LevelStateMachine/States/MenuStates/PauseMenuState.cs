using UnityEngine;

public class PauseMenuState : State
{
    public PauseMenuState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in PauseMenuState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from PauseMenuState");
    }
}
