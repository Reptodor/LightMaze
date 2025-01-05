using System.Collections;
using UnityEngine;

public class LevelInitializationState : State
{
    private Coroutine _coroutine;

    public LevelInitializationState(StateSwitch stateSwitch) : base(stateSwitch)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered in LevelInitializationState");

        _coroutine = Coroutines.StartRoutine(InitializeLevel());
    }

    public IEnumerator InitializeLevel()
    {
        //Initialization
        yield return null;

        StateSwitch.EnterIn<PreparationState>();
        Coroutines.StopRoutine(_coroutine);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exited from LevelInitializationState");
    }
}
