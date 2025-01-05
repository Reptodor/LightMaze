using UnityEngine;

public class PreparationState : State
{
    public PreparationState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in PreparationState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from PreparationState");
    }
}
