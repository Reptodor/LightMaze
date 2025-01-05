using UnityEngine;

public class WinMenuState : State
{
    public WinMenuState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in WinMenuState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from WinMenuState");
    }
}
