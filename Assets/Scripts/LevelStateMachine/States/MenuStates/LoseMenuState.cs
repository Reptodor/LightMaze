using UnityEngine;

public class LoseMenuState : State
{
    public LoseMenuState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in LoseMenuState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from LoseMenuState");
    }
}
