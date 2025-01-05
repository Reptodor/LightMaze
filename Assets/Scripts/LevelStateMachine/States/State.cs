public abstract class State : ILevelState
{
    protected readonly StateSwitch StateSwitch;

    public State(StateSwitch stateSwitch)
    {
        StateSwitch = stateSwitch;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }
}
