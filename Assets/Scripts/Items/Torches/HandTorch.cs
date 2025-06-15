using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HandTorch : MonoBehaviour
{
    [SerializeField] private HandTorchConfig _handTorchConfig;
    [SerializeField] private Light2D _flame;
    private FlameAnimationsHandler _flameAnimationsHandler;
    private Player _player;
    private float _angle = 0;
    private bool _isInitialized = false;

    public Light2D Flame => _flame; 

    private void OnValidate()
    {
        if (_flame == null)
            _flame = GetComponentInChildren<Light2D>();

        if (_handTorchConfig == null)
            throw new ArgumentNullException(nameof(_handTorchConfig), "Hand torch config cannot be null");
    }

    public void Initialize(Player player, FlameAnimationsConfig flameAnimationsConfig)
    {
        _player = player;
        _flameAnimationsHandler = new FlameAnimationsHandler(flameAnimationsConfig, _flame);
        _flameAnimationsHandler.HandleActivationAnimation();

        _isInitialized = true;
    }

    private void Update()
    {
        if(!_isInitialized)
            return;

        Move();
        _flameAnimationsHandler.HandleFlameIntensityAnimation();
    }

    private void Move()
    {
        _angle += Time.deltaTime;

        var x = Mathf.Cos (_angle * _handTorchConfig.Speed) * _handTorchConfig.Radius;
        var y = Mathf.Sin (_angle * _handTorchConfig.Speed) * _handTorchConfig.Radius;
        transform.position = new Vector2(x, y) + (Vector2)_player.transform.position;
    }
}
