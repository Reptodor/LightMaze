using UnityEngine;

public class MainMenuState : State
{
    private MainMenu _mainMenu;

    public MainMenuState(StateSwitch stateSwitch, MainMenu mainMenu) : base(stateSwitch)
    {
        _mainMenu = mainMenu;
    }

    public override void Enter()
    {
        base.Enter();
        _mainMenu.gameObject.SetActive(true);
        _mainMenu.Show();
        Debug.Log("Entered in MainMenuState");
    }

    public override void Exit()
    {
        base.Exit();
        _mainMenu.Hide();
        Debug.Log("Exited from MainMenuState");
    }
}
