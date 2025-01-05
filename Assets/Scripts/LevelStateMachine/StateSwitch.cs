using UnityEngine;

public class StateSwitch : MonoBehaviour
{
    private LevelStateMachine _levelStateMachine;

    public void Initialize(BootstrapMenu bootstrapMenu, MainMenu mainMenu)
    {
        _levelStateMachine = new LevelStateMachine(this, bootstrapMenu, mainMenu);
        EnterIn<BootstrapState>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _levelStateMachine.IsBattleState())
        {
            EnterIn<PauseMenuState>();
        }
    }

    public void EnterIn<TState>() where TState : ILevelState 
    {
        _levelStateMachine.EnterIn<TState>();
    }
}
