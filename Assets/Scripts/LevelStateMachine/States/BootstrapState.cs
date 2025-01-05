using UnityEngine;

public class BootstrapState : State
{
    private BootstrapMenu _bootstrapMenu;

    public BootstrapState(StateSwitch stateSwitch, BootstrapMenu bootstrapMenu) : base(stateSwitch)
    {
        _bootstrapMenu = bootstrapMenu;
    }

    public override void Enter()
    {
        base.Enter();
        _bootstrapMenu.gameObject.SetActive(true);
        Debug.Log("Entered in BootstrapState");
    }

    public override void Exit()
    {
        base.Exit();
        _bootstrapMenu.gameObject.SetActive(false);
        Debug.Log("Exite from BootstrapState");
    }
}
