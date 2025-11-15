using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HandTorch : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private float _collisionCheckRadius = 0.3f;

    [Header("Components")]
    [SerializeField] private HandTorchConfig _handTorchConfig;
    [SerializeField] private FlameAnimationsConfig _flameAnimationsConfig;
    [SerializeField] private Light2D _flame;

    private FlameAnimationsHandler _flameAnimationsHandler;
    private Player _player;
    private float _angle = 0;
    private bool _isInitialized = false;

    private void OnValidate()
    {
        if (_flame == null)
            throw new ArgumentNullException(nameof(_flame), "Flame cannot be null");

        if (_handTorchConfig == null)
            throw new ArgumentNullException(nameof(_handTorchConfig), "Hand torch config cannot be null");
    }

    public void Initialize(Player player)
    {
        _player = player;

        _flameAnimationsHandler = new FlameAnimationsHandler(_flameAnimationsConfig, _flame);
        _flameAnimationsHandler.HandleActivationAnimation();

        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized)
            return;

        Move();
        _flameAnimationsHandler.HandleFlameIntensityAnimation();
    }
    
    private void Move()
    {
        _angle += Time.deltaTime;
        
        var x = Mathf.Cos(_angle * _handTorchConfig.Speed) * _handTorchConfig.Radius;
        var y = Mathf.Sin(_angle * _handTorchConfig.Speed) * _handTorchConfig.Radius;
        Vector2 desiredPosition = new Vector2(x, y) + (Vector2)_player.transform.position;
        
        if (!Physics2D.OverlapCircle(desiredPosition, _collisionCheckRadius, _wallMask))
        {
            transform.position = desiredPosition;
        }
        else
        {
            transform.position = FindValidPosition(desiredPosition);
        }
    }
    
    private Vector2 FindValidPosition(Vector2 desiredPos)
    {
        Vector2 toPlayer = (Vector2)_player.transform.position - desiredPos;
        RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, -toPlayer.normalized, _handTorchConfig.Radius, _wallMask);
        
        if (hit.collider != null)
        {
            return hit.point - toPlayer.normalized * 0.1f;
        }
        
        return desiredPos;
    }
}
