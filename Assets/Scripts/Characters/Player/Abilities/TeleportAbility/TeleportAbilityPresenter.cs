using UnityEngine;

public class TeleportAbilityPresenter
{
    private TeleportAbilityView _view;
    private AbilityModel _model;

    private LayerMask _obstacleLayer;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;

    private float _teleportDistance = 5f;

    public TeleportAbilityPresenter(TeleportAbilityView teleportAbilityView, AbilityModel teleportAbilityModel, LayerMask obstacleLayer,
                                    Rigidbody2D rigidbody2D, float teleportDistance)
    {
        _view = teleportAbilityView;
        _model = teleportAbilityModel;
        _obstacleLayer = obstacleLayer;
        _rigidbody2D = rigidbody2D;
        _teleportDistance = teleportDistance;
    }

    public void Subscribe()
    {
        _view.AbilityRequested += OnAbilityRequested;
    }

    public void Unsubscribe()
    {
        _view.AbilityRequested -= OnAbilityRequested;
    }

    public void Update()
    {
        _model.Update(Time.deltaTime);

        if (!_model.IsAbilityActive && _model.CurrentCooldown > 0)
        {
            float cooldownProgress = _model.CurrentCooldown / _model.CooldownDuration;
            _view.UpdateCooldownVisual(cooldownProgress);
        }
    }

    public void UpdateMoveDirection(Vector2 moveDirection)
    {
        _moveDirection = moveDirection;
    }

    private void OnAbilityRequested()
    {
        _model.ActivateAbility();
        _view.ShowCooldownVisual();

        if (_model.IsAbilityActive)
        {
            _view.PlayTeleportSound();
            PerformTeleport();
        }
    }

    private void PerformTeleport()
    {
        Vector2 targetPosition = _rigidbody2D.position + _moveDirection * _teleportDistance;
        
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody2D.position, _moveDirection, _teleportDistance, _obstacleLayer);
        
        if (hit.collider != null)
        {
            targetPosition = hit.point - _moveDirection * 0.5f;
        }
        
        _rigidbody2D.position = targetPosition;
    }
}
