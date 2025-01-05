using UnityEngine;

public class Bootstrap : MonoBehaviour
{   
    [Header("StatesComponents")]
    [SerializeField] private StateSwitch _stateSwitch;
    [SerializeField] private BootstrapMenu _bootstrapMenu;
    [SerializeField] private MainMenu _mainMenu;

    // [Header("Player")]
    // [SerializeField] private Player _player;
    // [SerializeField] private HealthConfig _playerHealthConfig;
    // [SerializeField] private HealthView _playerHealthView;

    private void Awake()
    {
        _stateSwitch.Initialize(_bootstrapMenu, _mainMenu);
        // _player.Initialize(_playerHealthConfig, _playerHealthView);
    }
}
