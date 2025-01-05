using UnityEngine;

public class SettingsMenuState : State
{
    public SettingsMenuState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in SettingsMenuState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from SettingsMenuState");
    }
}
