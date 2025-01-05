using System;
using System.Collections.Generic;

public class LevelStateMachine
{
    private Dictionary<Type, ILevelState> _states;
    private ILevelState _currentState;
    private ILevelState _previousState;

    public ILevelState CurrentState => _currentState;

    public LevelStateMachine(StateSwitch stateSwitch, BootstrapMenu bootstrapMenu, MainMenu mainMenu)
    {
        _states = new Dictionary<Type, ILevelState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(stateSwitch, bootstrapMenu),
            [typeof(MainMenuState)] = new MainMenuState(stateSwitch, mainMenu),
            [typeof(SettingsMenuState)] = new SettingsMenuState(stateSwitch),
            [typeof(PauseMenuState)] = new PauseMenuState(stateSwitch),
            [typeof(WinMenuState)] = new WinMenuState(stateSwitch),
            [typeof(LoseMenuState)] = new LoseMenuState(stateSwitch),
            [typeof(LevelInitializationState)] = new LevelInitializationState(stateSwitch),
            [typeof(PreparationState)] = new PreparationState(stateSwitch),
            [typeof(PlayerTurnState)] = new PlayerTurnState(stateSwitch),
            [typeof(EnemyTurnState)] = new EnemyTurnState(stateSwitch)
        };
    }

    public bool IsBattleState()
    {
        return _currentState == _states[typeof(PreparationState)] ||
               _currentState == _states[typeof(PlayerTurnState)] ||
               _currentState == _states[typeof(EnemyTurnState)];
    }

    public void EnterIn<TState>() where TState : ILevelState
    {
        if(_states.TryGetValue(typeof(TState), out ILevelState state))
        {
            _previousState = _currentState;
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}
