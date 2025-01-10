using UnityEngine;

public class FirtsLevelBootstrap : MonoBehaviour
{
    [Header("Player components")]
    [SerializeField] private Player _player;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private HealthConfig _healthConfig;
    [SerializeField] private MeleeCombatConfig _playerMeleeCombatConfig;
    [SerializeField] private float _playerMovementSpeed;

    private void Awake()
    {
        _player.Initialize(_healthConfig, _healthView, _playerMeleeCombatConfig, _playerMovementSpeed);
    }
}
